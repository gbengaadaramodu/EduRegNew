using AutoMapper;
using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduReg.Services.Repositories
{
    public class FeeRulesRepository : IFeeRules
    {
        private readonly ApplicationDbContext _context;
        private readonly RequestContext _requestContext;
        private readonly IMapper _mapper;

        public FeeRulesRepository(ApplicationDbContext context, RequestContext requestContext, IMapper mapper)
        {
            _context = context;
            _requestContext = requestContext;
            _mapper = mapper;
        }

        public async Task<GeneralResponse> CreateFeeRuleAsync(FeeRuleDto model, string institutionShortName)
        {
            try
            {
                var entity = new FeeRule
                {
                    Id = model.Id,
                    InstitutionShortName = _requestContext.InstitutionShortName,
                    ProgrammeCode = model.ProgrammeCode,
                    DepartmentCode = model.DepartmentCode,
                    LevelName = model.LevelName,
                    ClassCode = model.ClassCode,
                    SessionId = model.SessionId,
                    SemesterId = model.SemesterId,
                    Amount = model.Amount,
                    IsRecurring = model.IsRecurring,
                    EffectiveFrom = model.EffectiveFrom,
                    EffectiveTo = model.EffectiveTo,
                    RecurrenceType = model.RecurrenceType,
                    ApplicabilityScope = model.ApplicabilityScope,                    
                    Created = DateTime.UtcNow
                };

                
                _context.FeeRule.Add(entity);
                await _context.SaveChangesAsync();

                var Dto = _mapper.Map<FeeRuleDto>(entity);

                return new GeneralResponse { StatusCode = 200, Message = "Fee rules created successfully.", Data = Dto };

            }
            catch (Exception ex)
            {
                return new GeneralResponse {StatusCode = 401 , Message = $"Error creating fee rule: {ex.Message}"};
            }
        }

        public async Task<GeneralResponse> GetAllFeeRuleAsync(string institutionShortName, PagingParameters paging,FeeRuleFilter? filter)
        {
            try
            {
                var query = _context.FeeRule
                    .AsQueryable();


                // Mandatory InstitutionShortName filter
                query = query.Where(x => x.InstitutionShortName == _requestContext.InstitutionShortName);

                // Optional filters from the filter object
                if (filter != null)
                {
                    if (filter.Id.HasValue)
                        query = query.Where(x => x.Id == filter.Id.Value);

                    if (!string.IsNullOrWhiteSpace(filter.ProgrammeCode))
                        query = query.Where(x => x.ProgrammeCode == filter.ProgrammeCode);

                    if (!string.IsNullOrWhiteSpace(filter.DepartmentCode))
                        query = query.Where(x => x.DepartmentCode == filter.DepartmentCode);

                    if (!string.IsNullOrWhiteSpace(filter.LevelName))
                        query = query.Where(x => x.LevelName == filter.LevelName);

                    if (!string.IsNullOrWhiteSpace(filter.ClassCode))
                        query = query.Where(x => x.ClassCode == filter.ClassCode);

                    if (!string.IsNullOrWhiteSpace(filter.SessionId))
                        query = query.Where(x => x.SessionId == filter.SessionId);

                    if (!string.IsNullOrWhiteSpace(filter.SemesterId))
                        query = query.Where(x => x.SemesterId == filter.SemesterId);

                    if (filter.RecurrenceType.HasValue)
                        query = query.Where(x => x.RecurrenceType == filter.RecurrenceType);

                    if (filter.ApplicabilityScope.HasValue)
                        query = query.Where(x => x.ApplicabilityScope == filter.ApplicabilityScope);

                    if (filter.IsRecurring.HasValue)
                        query = query.Where(x => x.IsRecurring == filter.IsRecurring);

                    // Effective date range
                    if (filter.EffectiveFrom.HasValue)
                        query = query.Where(x =>
                            x.EffectiveFrom == null || x.EffectiveFrom >= filter.EffectiveFrom.Value);

                    if (filter.EffectiveTo.HasValue)
                        query = query.Where(x =>
                            x.EffectiveTo == null || x.EffectiveTo <= filter.EffectiveTo.Value);

                    // Generic search (excluding institution since it's already filtered)
                    if (!string.IsNullOrWhiteSpace(filter.Search))
                        query = query.Where(x =>
                            (x.ProgrammeCode != null && x.ProgrammeCode.Contains(filter.Search)) ||
                            (x.DepartmentCode != null && x.DepartmentCode.Contains(filter.Search)) ||
                            (x.LevelName != null && x.LevelName.Contains(filter.Search)) ||
                            (x.ClassCode != null && x.ClassCode.Contains(filter.Search))
                        );
                }

                // Pagination
                var totalRecords = await query.CountAsync();

                var feeRules = await query
                    .OrderBy(x => x.Id)
                    .Skip((paging.PageNumber - 1) * paging.PageSize)
                    .Take(paging.PageSize)
                    .ToListAsync();

                var feeRuleDtos = _mapper.Map<List<FeeRuleDto>>(feeRules);

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = totalRecords == 0
                        ? "No fee rules found"
                        : "Fee rules retrieved successfully",
                    Data = feeRuleDtos,
                    Meta = new
                    {
                        paging.PageNumber,
                        paging.PageSize,
                        TotalRecords = totalRecords,
                        TotalPages = totalRecords == 0
                            ? 0
                            : (int)Math.Ceiling(totalRecords / (double)paging.PageSize)
                    }
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"Error retrieving fee rules: {ex.Message}",
                    Data = null
                };
            }
        }


        public async Task<GeneralResponse> GetFeeRuleByIdAsync( long id, string institutionShortName)
        {
            var rule = await _context.FeeRule
                .Include(r => r.FeeItem)
                .FirstOrDefaultAsync(r => r.Id == id && r.InstitutionShortName == _requestContext.InstitutionShortName);

            if (rule == null)
            {
                return new GeneralResponse { StatusCode = 404, Message = $"Rule not found" };
            }
            
            var ruleDto = _mapper.Map<FeeRuleDto>(rule);

            return new GeneralResponse { StatusCode = 200, Message = "Fee rules retrieved successfully.", Data = ruleDto };

        }

        public async Task<GeneralResponse> UpdateFeeRuleAsync(long id, FeeRuleDto model, string institutionShortName)
        {
            var rule = await _context.FeeRule
                .FirstOrDefaultAsync(r => r.Id == id && r.InstitutionShortName == _requestContext.InstitutionShortName);

            if (rule == null)
                return new GeneralResponse { StatusCode = 404, Message = $"Rule not found" };

            rule.Id = model.Id;
            rule.Amount = model.Amount;
            rule.IsRecurring = model.IsRecurring;
            rule.ProgrammeCode = model.ProgrammeCode;
            rule.DepartmentCode = model.DepartmentCode;
            rule.LevelName = model.LevelName;
            rule.ClassCode = model.ClassCode;
            rule.EffectiveFrom = model.EffectiveFrom;
            rule.EffectiveTo = model.EffectiveTo;
            rule.ApplicabilityScope = model.ApplicabilityScope;
            rule.RecurrenceType = model.RecurrenceType;
            rule.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return new GeneralResponse { StatusCode = 200, Message = "Fee rules updated successfully." };

        }

        public async Task<GeneralResponse> DeleteFeeRuleAsync(long id, string institutionShortName)
        {
            var rule = await _context.FeeRule
                .FirstOrDefaultAsync(r => r.Id == id && r.InstitutionShortName == _requestContext.InstitutionShortName);

            if (rule == null)
                return new GeneralResponse { StatusCode = 404, Message = $"Rule not found" };

            _context.FeeRule.Remove(rule);
            await _context.SaveChangesAsync();

            return new GeneralResponse { StatusCode = 200, Message = "Fee rules deleted successfully." };

        }

        public async Task<GeneralResponse> ApplyFeeRuleToSemesterScheduleAsync(List<FeeRuleDto> models, string institutionShortName)
        {
            try
            {
                foreach (var model in models)
                {
                    model.InstitutionShortName = _requestContext.InstitutionShortName;
                    await CreateFeeRuleAsync(model, _requestContext.InstitutionShortName);
                }

                return new GeneralResponse { StatusCode = 200, Message = "Fee rules applied successfully." };

            }
            catch (Exception ex)
            {
                return new GeneralResponse { StatusCode = 401, Message = $"Error applying rule" };
            }
        }
    }
}
