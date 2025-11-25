using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduReg.Services.Repositories
{
    public class RegistrationsBusinessRulesRepository : IRegistrationsBusinessRules
    {
        private readonly ApplicationDbContext _context;

        public RegistrationsBusinessRulesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GeneralResponse> ValidateStudentRegistrationAsync(RegistrationBusinessRulesDto model)
        {
            try
            {
                var ruleEntity = await _context.RegistrationsBusinessRules
                    .FirstOrDefaultAsync(rb =>
                        rb.InstitutionShortName == model.InstitutionShortName &&
                        rb.DepartmentCode == model.DepartmentCode &&
                        rb.ProgrammeCode == model.ProgrammeCode &&
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

                // Could also validate minimum or compulsory/elective metrics here if needed.
                // e.g., ensure total minimum units are met, etc.

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "Business rule found.",
                    Data = ruleEntity
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
                        rb.InstitutionShortName == model.InstitutionShortName &&
                        rb.DepartmentCode == model.DepartmentCode &&
                        rb.ProgrammeCode == model.ProgrammeCode &&
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
                    InstitutionShortName = model.InstitutionShortName,
                    DepartmentCode = model.DepartmentCode,
                    ProgrammeCode = model.ProgrammeCode,
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

                return new GeneralResponse
                {
                    StatusCode = 201,
                    Message = "Business rule created successfully.",
                    Data = entity
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
                            rb.InstitutionShortName == model.InstitutionShortName &&
                            rb.DepartmentCode == model.DepartmentCode &&
                            rb.ProgrammeCode == model.ProgrammeCode &&
                            rb.SemesterId == model.SemesterId &&
                            rb.LevelName == model.LevelName &&
                            rb.ClassCode == model.ClassCode
                        );
                    if (exists != null)
                        continue;

                    var entity = new RegistrationsBusinessRules
                    {
                        Name = model.Name,
                        Description = model.Description,
                        InstitutionShortName = model.InstitutionShortName,
                        DepartmentCode = model.DepartmentCode,
                        ProgrammeCode = model.ProgrammeCode,
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

                return new GeneralResponse
                {
                    StatusCode = 201,
                    Message = $"{added.Count} business rule(s) created successfully.",
                    Data = added
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

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = list.Any() ? "Business rules retrieved." : "No business rules found.",
                    Data = list
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

        public async Task<GeneralResponse> GetAllRegistrationBusinessRulesAsync()
        {
            try
            {
                var list = await _context.RegistrationsBusinessRules.ToListAsync();
                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = list.Any() ? "Business rules retrieved successfully." : "No business rules found.",
                    Data = list
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

        public async Task<GeneralResponse> UpdateRegistrationBusinessRuleAsync(int Id, RegistrationBusinessRulesDto model)
        {
            try
            {
                var entity = await _context.RegistrationsBusinessRules.FindAsync(Id);
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
                entity.InstitutionShortName = model.InstitutionShortName;
                entity.DepartmentCode = model.DepartmentCode;
                entity.ProgrammeCode = model.ProgrammeCode;
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

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "Business rule updated successfully.",
                    Data = entity
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

        public async Task<GeneralResponse> DeleteRegistrationBusinessRuleAsync(int Id)
        {
            try
            {
                var entity = await _context.RegistrationsBusinessRules.FindAsync(Id);
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
