using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace EduReg.Services.Repositories
{
    public class RegistrationsBusinessRulesRepository : IRegistrationsBusinessRules
    {
        private readonly ApplicationDbContext _context;
        private readonly RequestContext _requestContext;
        private readonly IMapper _mapper;

        public RegistrationsBusinessRulesRepository(ApplicationDbContext context, RequestContext requestContext, IMapper mapper)
        {
            _context = context;
            _requestContext = requestContext;
            _mapper = mapper;
        }

        public async Task<GeneralResponse> ValidateStudentRegistrationAsync(RegistrationBusinessRulesDto model)
        {
            try
            {
                var ruleEntity = await _context.RegistrationsBusinessRules
                    .FirstOrDefaultAsync(rb =>
                        rb.InstitutionShortName == _requestContext.InstitutionShortName &&
                        rb.DepartmentCode == model.DepartmentCode &&
                        rb.ProgrammeCode == model.ProgrammeCode &&
                        rb.SemesterName == model.SemesterName &&
                        rb.SemesterId == model.SemesterId &&
                        rb.LevelName == model.LevelName &&
                        rb.ClassCode == model.ClassCode
                    );

                if (ruleEntity == null)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 404,
                        Message = "Business rule not defined for the given session/semester/class/level."
                    };
                }

                var ruleDto = _mapper.Map<RegistrationBusinessRulesDto>(ruleEntity);

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "Business rule found.",
                    Data = ruleDto
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"Internal Server Error: {ex.Message}"
                };
            }
        }

        public async Task<GeneralResponse> CreateRegistrationBusinessRuleAsync(RegistrationBusinessRulesDto model)
        {
            try
            {
                // Check if a rule already exists for this combination
                var exists = await _context.RegistrationsBusinessRules
                    .FirstOrDefaultAsync(rb =>
                        rb.InstitutionShortName == _requestContext.InstitutionShortName &&
                        rb.DepartmentCode == model.DepartmentCode &&
                        rb.ProgrammeCode == model.ProgrammeCode &&
                        rb.SemesterName == model.SemesterName &&
                        rb.SemesterId == model.SemesterId &&
                        rb.LevelName == model.LevelName &&
                        rb.ClassCode == model.ClassCode
                    );

                if (exists != null)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 400,
                        Message = "A business rule already exists for this session/semester/level/class."
                    };
                }

                var entity = new RegistrationsBusinessRules
                {
                    Name = model.Name,
                    Description = model.Description,
                    InstitutionShortName = _requestContext.InstitutionShortName,
                    DepartmentCode = model.DepartmentCode,
                    ProgrammeCode = model.ProgrammeCode,
                    SemesterName = model.SemesterName,
                    SemesterId = model.SemesterId,
                    LevelName = model.LevelName,
                    ClassCode = model.ClassCode,
                    TotalCompulsoryUnits = model.TotalCompulsoryUnits,
                    TotalElectiveUnits = model.TotalElectiveUnits,
                    TotalMinimumCreditUnits = model.TotalMinimumCreditUnits,
                    TotalMaximumCreditUnits = model.TotalMaximumCreditUnits,
                    Remarks = model.Remarks,
                    Created = model.Created,
                    CreatedBy = model.CreatedBy,
                    ActiveStatus = model.ActiveStatus
                };

                _context.RegistrationsBusinessRules.Add(entity);
                await _context.SaveChangesAsync();

                var createdDto = _mapper.Map<RegistrationBusinessRulesDto>(entity);

                return new GeneralResponse
                {
                    StatusCode = 201,
                    Message = "Business rule created successfully.",
                    Data = createdDto
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"Internal Server Error: {ex.Message}"
                };
            }
        }

        public async Task<GeneralResponse> CreateRegistrationBusinessRuleAsync(List<RegistrationBusinessRulesDto> models)
        {
            try
            {
                var added = new List<RegistrationsBusinessRules>();

                foreach (var model in models)
                {
                    var exists = await _context.RegistrationsBusinessRules
                        .FirstOrDefaultAsync(rb =>
                            rb.InstitutionShortName == _requestContext.InstitutionShortName &&
                            rb.DepartmentCode == model.DepartmentCode &&
                            rb.ProgrammeCode == model.ProgrammeCode &&
                            rb.SemesterId == model.SemesterId &&
                            rb.SemesterName == model.SemesterName &&
                            rb.LevelName == model.LevelName &&
                            rb.ClassCode == model.ClassCode
                        );
                    if (exists != null)
                        continue;

                    var entity = new RegistrationsBusinessRules
                    {
                        Name = model.Name,
                        Description = model.Description,
                        InstitutionShortName = _requestContext.InstitutionShortName,
                        DepartmentCode = model.DepartmentCode,
                        ProgrammeCode = model.ProgrammeCode,
                        SemesterId = model.SemesterId,
                        SemesterName = model.SemesterName,
                        LevelName = model.LevelName,
                        ClassCode = model.ClassCode,
                        TotalCompulsoryUnits = model.TotalCompulsoryUnits,
                        TotalElectiveUnits = model.TotalElectiveUnits,
                        TotalMinimumCreditUnits = model.TotalMinimumCreditUnits,
                        TotalMaximumCreditUnits = model.TotalMaximumCreditUnits,
                        Remarks = model.Remarks,
                        Created = model.Created,
                        CreatedBy = model.CreatedBy,
                        ActiveStatus = model.ActiveStatus
                    };
                    added.Add(entity);
                }

                if (!added.Any())
                {
                    return new GeneralResponse
                    {
                        StatusCode = 400,
                        Message = "No new business rules added (all duplicates or none provided)."
                    };
                }

                _context.RegistrationsBusinessRules.AddRange(added);
                await _context.SaveChangesAsync();

                var addedDtos = _mapper.Map<List<RegistrationBusinessRulesDto>>(added);
                return new GeneralResponse
                {
                    StatusCode = 201,
                    Message = $"{added.Count} business rule(s) created successfully.",
                    Data = addedDtos
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"Internal Server Error: {ex.Message}"
                };
            }
        }

        public async Task<GeneralResponse> UploadRegistrationBusinessRuleAsync(byte[] model)
        {
            try
            {
                // TODO: parse file into DTOs
                List<RegistrationBusinessRulesDto> parsed = ParseBusinessRulesFromBytes(model);
                if (parsed == null || !parsed.Any())
                {
                    return new GeneralResponse
                    {
                        StatusCode = 400,
                        Message = "No valid business rules found in the uploaded file."
                    };
                }

                return await CreateRegistrationBusinessRuleAsync(parsed);
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"Internal Server Error: {ex.Message}"
                };
            }
        }

        public async Task<GeneralResponse> GetRegistrationBusinessRulesByDepartmentAsync(string DepartmentCode, RegistrationBusinessRulesDto model)
        {
            try
            {
               

                 var query = _context.RegistrationsBusinessRules
                    .Where(rb => rb.DepartmentCode == DepartmentCode);

                 query = query.Where(rb => rb.InstitutionShortName == _requestContext.InstitutionShortName);

                // Filter optional things if provided
                if (!string.IsNullOrEmpty(model.InstitutionShortName))
                    query = query.Where(rb => rb.InstitutionShortName == model.InstitutionShortName);
                if (!string.IsNullOrEmpty(model.ProgrammeCode))
                    query = query.Where(rb => rb.ProgrammeCode == model.ProgrammeCode);
                if (!string.IsNullOrEmpty(model.LevelName))
                    query = query.Where(rb => rb.LevelName == model.LevelName);
                if (!string.IsNullOrEmpty(model.ClassCode))
                    query = query.Where(rb => rb.ClassCode == model.ClassCode);

                var list = await query.ToListAsync();

                var dtos = _mapper.Map<List<RegistrationBusinessRulesDto>>(list);

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = list.Any() ? "Business rules retrieved." : "No business rules found.",
                    Data = dtos
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"Internal Server Error: {ex.Message}"
                };
            }
        }

        public async Task<GeneralResponse> GetAllRegistrationBusinessRulesAsync(RegistrationBusinessRuleFilter filter,PagingParameters paging)
        {
            try
            {
                var query = _context.RegistrationsBusinessRules
                    .AsNoTracking()
                    .AsQueryable();

                     query = query.Where(x =>
                        x.InstitutionShortName == _requestContext.InstitutionShortName);
                // APPLY FILTERS

                if (!string.IsNullOrWhiteSpace(filter?.DepartmentCode))
                {
                    query = query.Where(x =>
                        x.DepartmentCode == filter.DepartmentCode);
                }

                if (!string.IsNullOrWhiteSpace(filter?.ProgrammeCode))
                {
                    query = query.Where(x =>
                        x.ProgrammeCode == filter.ProgrammeCode);
                }

                if (filter?.SemesterId != null)
                {
                    query = query.Where(x =>
                        x.SemesterId == filter.SemesterId);
                }

                if (!string.IsNullOrWhiteSpace(filter?.LevelName))
                {
                    query = query.Where(x =>
                        x.LevelName == filter.LevelName);
                }

                if (!string.IsNullOrWhiteSpace(filter?.ClassCode))
                {
                    query = query.Where(x =>
                        x.ClassCode == filter.ClassCode);
                }

                // Units range
                if (filter?.MinTotalUnits != null)
                {
                    query = query.Where(x =>
                        x.TotalMinimumCreditUnits >= filter.MinTotalUnits);
                }

                if (filter?.MaxTotalUnits != null)
                {
                    query = query.Where(x =>
                        x.TotalMaximumCreditUnits <= filter.MaxTotalUnits);
                }

                // Generic search 
                if (!string.IsNullOrWhiteSpace(filter?.Search))
                {
                    query = query.Where(x =>
                        (x.Name != null && x.Name.Contains(filter.Search)) ||
                        (x.Description != null && x.Description.Contains(filter.Search)) ||
                        (x.Remarks != null && x.Remarks.Contains(filter.Search))
                    );
                }

                // PAGINATION

                var totalRecords = await query.CountAsync();

                var rules = await query
                    .OrderBy(x => x.Name)
                    .Skip((paging.PageNumber - 1) * paging.PageSize)
                    .Take(paging.PageSize)
                    .ToListAsync();

                var rulesDto = _mapper.Map<List<RegistrationBusinessRulesDto>>(rules);

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = totalRecords == 0
                        ? "No business rules found."
                        : "Business rules retrieved successfully.",
                    Data = rulesDto,
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
                    Message = $"Internal Server Error: {ex.Message}",
                    Data = null
                };
            }
        }


        public async Task<GeneralResponse> UpdateRegistrationBusinessRuleAsync(long id, RegistrationBusinessRulesDto model)

        {
            try
            {
                
                var entity = await _context.RegistrationsBusinessRules.FirstOrDefaultAsync(x =>x.Id == id && x.InstitutionShortName == _requestContext.InstitutionShortName);
                if (entity == null)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 404,
                        Message = "Business rule not found."
                    };
                }

                // Update values
                entity.Name = model.Name;
                entity.Description = model.Description;
                entity.InstitutionShortName =_requestContext.InstitutionShortName;
                entity.DepartmentCode = model.DepartmentCode;
                entity.ProgrammeCode = model.ProgrammeCode;
                entity.SemesterName = model.SemesterName;
                entity.SemesterId = model.SemesterId;
                entity.LevelName = model.LevelName;
                entity.ClassCode = model.ClassCode;
                entity.TotalCompulsoryUnits = model.TotalCompulsoryUnits;
                entity.TotalElectiveUnits = model.TotalElectiveUnits;
                entity.TotalMinimumCreditUnits = model.TotalMinimumCreditUnits;
                entity.TotalMaximumCreditUnits = model.TotalMaximumCreditUnits;
                entity.Remarks = model.Remarks;
                entity.ActiveStatus = model.ActiveStatus;

                _context.RegistrationsBusinessRules.Update(entity);
                await _context.SaveChangesAsync();

                var updatedDto = _mapper.Map<RegistrationBusinessRulesDto>(entity);

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "Business rule updated successfully.",
                    Data = updatedDto
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"Internal Server Error: {ex.Message}"
                };
            }
        }

        public async Task<GeneralResponse> DeleteRegistrationBusinessRuleAsync(long Id)
        {
            try
            {
                var entity = await _context.RegistrationsBusinessRules.FirstOrDefaultAsync(x => x.Id == Id && x.InstitutionShortName == _requestContext.InstitutionShortName);
                if (entity == null)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 404,
                        Message = "Business rule not found."
                    };
                }

                _context.RegistrationsBusinessRules.Remove(entity);
                await _context.SaveChangesAsync();

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "Business rule deleted successfully."
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"Internal Server Error: {ex.Message}"
                };
            }
        }

        // Helper to parse byte[] file to DTOs
        private List<RegistrationBusinessRulesDto> ParseBusinessRulesFromBytes(byte[] data)
        {
            // TODO: implement file parsing (CSV, Excel etc.)
            return new List<RegistrationBusinessRulesDto>();
        }
    }
}
