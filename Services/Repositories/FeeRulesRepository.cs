using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduReg.Services.Repositories
{
    public class FeeRulesRepository : IFeeRules
    {
        private readonly ApplicationDbContext _context;

        public FeeRulesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GeneralResponse> CreateFeeRuleAsync(FeeRuleDto model, string institutionShortName)
        {
            try
            {
                var entity = new FeeRule
                {
                    FeeItemId = model.FeeItemId,
                    InstitutionShortName = institutionShortName,
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

                return new GeneralResponse { StatusCode = 200, Message = "Fee rules created successfully.", Data = entity };

            }
            catch (Exception ex)
            {
                return new GeneralResponse {StatusCode = 401 , Message = $"Error creating fee rule: {ex.Message}"};
            }
        }

        public async Task<GeneralResponse> GetAllFeeRuleAsync(string institutionShortName,PagingParameters paging)
        {
            var query = _context.FeeRule
                .Where(x => x.InstitutionShortName == institutionShortName)
                .AsQueryable();


            var totalRecords = await query.CountAsync();

            var feeRules = await query
                .OrderBy(x => x.FeeItemId) 
                .Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize)
                .ToListAsync();

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = totalRecords == 0
                    ? "No fee rules found"
                    : "Fee rules retrieved successfully",
                Data = feeRules,
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

        public async Task<GeneralResponse> GetFeeRuleByIdAsync(int id, string institutionShortName)
        {
            var rule = await _context.FeeRule
                .Include(r => r.FeeItem)
                .FirstOrDefaultAsync(r => r.Id == id && r.InstitutionShortName == institutionShortName);

            if (rule == null)
            {
                return new GeneralResponse { StatusCode = 404, Message = $"Rule not found" };
            }
            

           return new GeneralResponse { StatusCode = 200, Message = "Fee rules retrieved successfully.", Data = rule };
        
        

        }

        public async Task<GeneralResponse> UpdateFeeRuleAsync(int id, FeeRuleDto model, string institutionShortName)
        {
            var rule = await _context.FeeRule
                .FirstOrDefaultAsync(r => r.Id == id && r.InstitutionShortName == institutionShortName);

            if (rule == null)
                return new GeneralResponse { StatusCode = 404, Message = $"Rule not found" };

            rule.FeeItemId = model.FeeItemId;
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

        public async Task<GeneralResponse> DeleteFeeRuleAsync(int id, string institutionShortName)
        {
            var rule = await _context.FeeRule
                .FirstOrDefaultAsync(r => r.Id == id && r.InstitutionShortName == institutionShortName);

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
                    model.InstitutionShortName = institutionShortName;
                    await CreateFeeRuleAsync(model, institutionShortName);
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
