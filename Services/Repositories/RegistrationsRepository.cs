using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduReg.Services.Repositories
{
    public class RegistrationsRepository : IRegistrations
    {
        private readonly ApplicationDbContext _context;
        private readonly IRegistrationsBusinessRules _rulesRepo;

        public RegistrationsRepository(ApplicationDbContext context, IRegistrationsBusinessRules rulesRepo)
        {
            _context = context;
            _rulesRepo = rulesRepo;
        }

        /// <summary>
        /// Create a single student registration, but check business rules (e.g., unit limit) before adding.
        /// Assumes model includes session, semester, matricNumber, classCode etc.
        /// </summary>
        public async Task<GeneralResponse> CreateStudentRegistrationAsync(RegistrationsDto model)
        {
            try
            {
                // 1. Validate business rules for this student in this context
                var ruleDto = new RegistrationBusinessRulesDto
                {
                    InstitutionShortName = model.DepartmentId, // or however you map institution
                    DepartmentCode = model.DepartmentId,
                    ProgrammeCode = model.DepartmentId, // adjust if programme is separate
                    SemesterId = int.TryParse(model.SemesterId, out var sem) ? sem : 0,
                    LevelName = model.LevelName,
                    ClassCode = model.ClassCode
                };

                var validationResponse = await _rulesRepo.ValidateStudentRegistrationAsync(ruleDto);
                if (validationResponse.StatusCode != 200)
                {
                    return validationResponse;
                }

                // 2. Compute already registered units for this student in this session/semester/class
                var existingUnits = await _context.Registrations
                    .Where(r => r.MatricNumber == model.MatricNumber
                                && r.SessionId == model.SessionId
                                && r.SemesterId == model.SemesterId
                                && r.ClassCode == model.ClassCode)
                    .SumAsync(r => r.Units);

                // 3. Check if adding this new registration would exceed max units
                // Extract the matched business rule entity from validationResponse.Data
                if (!(validationResponse.Data is RegistrationsBusinessRules ruleEntity))
                {
                    return new GeneralResponse
                    {
                        StatusCode = 500,
                        Message = "Business rule configuration error."
                    };
                }

                // total units after adding
                var newTotal = existingUnits + model.Units;
                if (newTotal > ruleEntity.TotalMaximumCreditUnits)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 400,
                        Message = $"Unit limit exceeded. You already have {existingUnits} units registered; adding {model.Units} would exceed maximum {ruleEntity.TotalMaximumCreditUnits} units."
                    };
                }

                // 4. Create registration
                var registration = new Registrations
                {
                    MatricNumber = model.MatricNumber,
                    SessionId = model.SessionId,
                    DepartmentId = model.DepartmentId,
                    SemesterId = model.SemesterId,
                    ClassCode = model.ClassCode,
                    CourseCode = model.CourseCode,
                    CourseTitle = model.CourseTitle,
                    LevelName = model.LevelName,
                    Units = model.Units,
                    RegisteredOn = model.RegisteredOn == default(DateTime) ? DateTime.UtcNow : model.RegisteredOn,
                    CourseFee = model.CourseFee,
                    IsApproved = model.IsApproved,
                    IsPaid = model.IsPaid,
                    IsCompleted = model.IsCompleted,
                    RawScore = model.RawScore,
                    Grade = model.Grade,
                    ApprovedBy = model.ApprovedBy,
                    ApprovedDate = model.ApprovedDate,
                    LMSId = model.LMSId,
                    IsEnrolledOnLMS = model.IsEnrolledOnLMS,
                    Created = model.Created,
                    CreatedBy = model.CreatedBy,
                    ActiveStatus = model.ActiveStatus
                };

                _context.Registrations.Add(registration);
                await _context.SaveChangesAsync();

                return new GeneralResponse
                {
                    StatusCode = 201,
                    Message = "Registration created successfully.",
                    Data = registration
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

        public async Task<GeneralResponse> CreateStudentRegistrationAsync(List<RegistrationsDto> models)
        {
            try
            {
                var responses = new List<Registrations>();
                foreach (var model in models)
                {
                    var singleResp = await CreateStudentRegistrationAsync(model);
                    if (singleResp.StatusCode != 201)
                    {
                        // Option: you could abort full batch or collect errors. Here, aborting and returning error.
                        return singleResp;
                    }
                    if (singleResp.Data is Registrations r)
                    {
                        responses.Add(r);
                    }
                }

                return new GeneralResponse
                {
                    StatusCode = 201,
                    Message = $"{responses.Count} registrations created successfully.",
                    Data = responses
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

        public async Task<GeneralResponse> CreateStudentRegistrationAsync(byte[] model)
        {
            try
            {
                // TODO: parse file (e.g. csv, excel) into List<RegistrationsDto>
                List<RegistrationsDto> parsed = ParseRegistrationsFromBytes(model);
                if (parsed == null || !parsed.Any())
                {
                    return new GeneralResponse
                    {
                        StatusCode = 400,
                        Message = "No valid registration entries found in file."
                    };
                }

                return await CreateStudentRegistrationAsync(parsed);
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

        public async Task<GeneralResponse> DropStudentRegistrationAsync(int Id)
        {
            try
            {
                var reg = await _context.Registrations.FindAsync(Id);
                if (reg == null)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 404,
                        Message = "Registration not found."
                    };
                }

                _context.Registrations.Remove(reg);
                await _context.SaveChangesAsync();

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "Registration dropped successfully."
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

        public async Task<GeneralResponse> GetAllStudentRegistrationsAync(string matricNumber)
        {
            try
            {
                var list = await _context.Registrations
                    .Where(r => r.MatricNumber == matricNumber)
                    .ToListAsync();

                if (!list.Any())
                {
                    return new GeneralResponse
                    {
                        StatusCode = 404,
                        Message = "No registrations found for this student."
                    };
                }

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "Registrations retrieved successfully.",
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

        public async Task<GeneralResponse> GetDepartmentRegistrationsBySessionIdAsync(string sessionId)
        {
            try
            {
                var list = await _context.Registrations
                    .Where(r => r.SessionId == sessionId)
                    .ToListAsync();

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = list.Any() ? "Department registrations retrieved successfully." : "No registrations found for this session.",
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

        public async Task<GeneralResponse> GetDepartmentRegistrationsBySemesterIdAsync(string semesterId)
        {
            try
            {
                var list = await _context.Registrations
                    .Where(r => r.SemesterId == semesterId)
                    .ToListAsync();

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = list.Any() ? "Department registrations retrieved successfully." : "No registrations found for this semester.",
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

        public async Task<GeneralResponse> GetStudentRegistrationsBySessionIdAync(RegistrationsDto model)
        {
            try
            {
                var list = await _context.Registrations
                    .Where(r => r.MatricNumber == model.MatricNumber && r.SessionId == model.SessionId)
                    .ToListAsync();

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = list.Any() ? "Registrations retrieved successfully." : "No registrations found for this student in that session.",
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

        public async Task<GeneralResponse> GetStudentRegistrationsBySemesterIdAync(RegistrationsDto model)
        {
            try
            {
                var list = await _context.Registrations
                    .Where(r => r.MatricNumber == model.MatricNumber && r.SemesterId == model.SemesterId)
                    .ToListAsync();

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = list.Any() ? "Registrations retrieved successfully." : "No registrations found for this student in that semester.",
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

        // Helper to parse uploaded file to DTOs
        private List<RegistrationsDto> ParseRegistrationsFromBytes(byte[] data)
        {
            // TODO: implement parsing logic from CSV/Excel, mapping to RegistrationsDto
            return new List<RegistrationsDto>();
        }
    }


}