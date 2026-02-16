using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks.Dataflow;
using System.Web.WebPages;
using AutoMapper;

namespace EduReg.Services.Repositories
{
    public class SemestersRepository : ISemesters
    {
        private readonly ApplicationDbContext _context;
        private readonly RequestContext _requestContext;
        private readonly IMapper _mapper;
        public SemestersRepository(ApplicationDbContext context, RequestContext requestContext, IMapper mapper)
        {
            _context = context;
            _requestContext = requestContext;
            _requestContext.InstitutionShortName = requestContext.InstitutionShortName.ToUpper();
            _mapper = mapper;
        }
        public async Task<GeneralResponse> CreateSemesterAsync(SemestersDto model)
        {
            if (model == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "Invalid semester data",
                    Data = null
                };
            }

            var sessionExists = await _context.AcademicSessions.AnyAsync(session => session.Id == model.SessionId);
            if (!sessionExists)
            {
                return new GeneralResponse
                {
                    StatusCode = 400,
                    Message = $"Invalid session Id: {model.SessionId}",
                    Data = null
                };
            }

            var batchExists = await _context.AdmissionBatches.AnyAsync(batch => batch.BatchShortName == model.BatchShortName && batch.InstitutionShortName == _requestContext.InstitutionShortName);
            if (!batchExists)
            {
                return new GeneralResponse
                {
                    StatusCode = 400,
                    Message = $"Invalid BatchShortName: {model.BatchShortName}",
                    Data = null
                };
            }
            var validateDates = ValidateDates(model.StartDate, model.EndDate, model.RegistrationStartDate, model.RegistrationEndDate, model.RegistrationCloseDate);
            if(validateDates.StatusCode != 200)
            {
                return validateDates;
            }

            model.InstitutionShortName = _requestContext.InstitutionShortName;
            var entity = new Semesters
            {
                InstitutionShortName = model.InstitutionShortName,
                SessionId = model.SessionId,
                SemesterName = model.SemesterName,
                //SemesterId = model.SemesterId,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                ActiveStatus = model.ActiveStatus,
                BatchShortName = model.BatchShortName,
                RegistrationStartDate = model.RegistrationStartDate,
                RegistrationEndDate = model.RegistrationEndDate,
                RegistrationCloseDate = model.RegistrationCloseDate,
            };

            await _context.Semesters.AddAsync(entity);
            await _context.SaveChangesAsync();

            var dto = _mapper.Map<SemestersDto>(entity);

            return new GeneralResponse
            {
                StatusCode = 201,
                Message = "Semester created successfully",
                Data = dto
            };
        }

        public async Task<GeneralResponse> DeleteSemesterAsync(long Id)
        {
            var semester = await _context.Semesters.FirstOrDefaultAsync(r => r.Id == Id && r.InstitutionShortName == _requestContext.InstitutionShortName);

            if (semester == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "Semester not found",
                    Data = null
                };
            }

            _context.Semesters.Remove(semester);
            await _context.SaveChangesAsync();

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Semester deleted successfully",
                Data = null
            };
        }

        public async Task<GeneralResponse> GetAllSemestersAsync(PagingParameters paging, SemesterFilter? filter)
        {
            try
            {
                var query = _context.Semesters.AsQueryable();
                filter.InstitutionShortName = _requestContext.InstitutionShortName;
                // Apply optional filters
                if (filter != null)
                {
                    if (!string.IsNullOrWhiteSpace(filter.InstitutionShortName))
                        query = query.Where(x => x.InstitutionShortName == filter.InstitutionShortName);

                    if (filter.SessionId.HasValue)
                        query = query.Where(x => x.SessionId == filter.SessionId.Value);

                    if (filter.SemesterId.HasValue)
                        query = query.Where(x => x.Id == filter.SemesterId.Value);

                    if (!string.IsNullOrWhiteSpace(filter.SemesterName))
                        query = query.Where(x => x.SemesterName.Contains(filter.SemesterName));

                    if (filter.StartDateFrom.HasValue)
                        query = query.Where(x => x.StartDate >= filter.StartDateFrom.Value);

                    if (filter.StartDateTo.HasValue)
                        query = query.Where(x => x.StartDate <= filter.StartDateTo.Value);

                    if (!string.IsNullOrWhiteSpace(filter.Search))
                    {
                        query = query.Where(x =>
                            (x.SemesterName != null && x.SemesterName.Contains(filter.Search)) ||
                            (x.InstitutionShortName != null && x.InstitutionShortName.Contains(filter.Search))
                        );
                    }
                }

                // Count total records after filtering
                var totalRecords = await query.CountAsync();

                // Apply pagination
                var semesters = await query
                    .OrderBy(x => x.SemesterName)
                    .Skip((paging.PageNumber - 1) * paging.PageSize)
                    .Take(paging.PageSize)
                    .ToListAsync();

                var semesterDtos = _mapper.Map<List<SemestersDto>>(semesters);

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = totalRecords == 0
                        ? "No semesters found"
                        : "Semesters retrieved successfully",
                    Data = semesterDtos,
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


        public async Task<GeneralResponse> GetSemesterByIdAsync(long Id)
        {
            if (Id <= 0)
            {
                return new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "Invalid ID",
                    Data = null
                };
            }

            var semester = await _context.Semesters.FirstOrDefaultAsync(r => r.Id == Id && r.InstitutionShortName == _requestContext.InstitutionShortName);
            if (semester == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "Semester not found",
                    Data = null
                };
            }

            var semesterDto = _mapper.Map<SemestersDto>(semester);
            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Semester retrieved successfully",
                Data = semesterDto
            };
        }

        public async Task<GeneralResponse> UpdateSemesterAsync(long Id, SemestersDto model)
        {
            var semester = await _context.Semesters.FirstOrDefaultAsync(r => r.Id == Id && r.InstitutionShortName == _requestContext.InstitutionShortName);

            if (semester == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "Semester not found",
                    Data = null
                };
            }

            var sessionExists = await _context.AcademicSessions.AnyAsync(session => session.Id == model.SessionId);
            if (!sessionExists)
            {
                return new GeneralResponse
                {
                    StatusCode = 400,
                    Message = $"Invalid session Id: {model.SessionId}",
                    Data = null
                };
            }

            var batchExists = await _context.AdmissionBatches.AnyAsync(batch => batch.BatchShortName == model.BatchShortName && batch.InstitutionShortName == _requestContext.InstitutionShortName);
            if (!batchExists)
            {
                return new GeneralResponse
                {
                    StatusCode = 400,
                    Message = $"Invalid BatchShortName: {model.BatchShortName}",
                    Data = null
                };
            }

            var validateDates = ValidateDates(model.StartDate, model.EndDate, model.RegistrationStartDate, model.RegistrationEndDate, model.RegistrationCloseDate);
            if (validateDates.StatusCode != 200)
            {
                return validateDates;
            }

            // semester.InstitutionShortName = model.InstitutionShortName;
            semester.SessionId = model.SessionId;
            semester.SemesterName = model.SemesterName;
            //semester.SemesterId = model.SemesterId;
            semester.StartDate = model.StartDate;
            semester.EndDate = model.EndDate;
            semester.ActiveStatus = model.ActiveStatus;
            semester.BatchShortName = model.BatchShortName;
            semester.RegistrationStartDate = model.RegistrationStartDate;
            semester.RegistrationCloseDate = model.RegistrationCloseDate;
            semester.RegistrationEndDate = model.RegistrationEndDate;

            _context.Semesters.Update(semester);
            await _context.SaveChangesAsync();

            var dto = _mapper.Map<SemestersDto>(semester);

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Semester updated successfully",
                Data = dto
            };
        }

        private static GeneralResponse ValidateDates(
                                        DateTime startDate,
                                        DateTime endDate,
                                        DateTime? registrationStartDate,
                                        DateTime? registrationEndDate,
                                        DateTime? registrationCloseDate)
        {
            // 1. Extreme ends
            if (startDate > endDate)
            {
                return new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "StartDate must be less than or equal to EndDate.",
                    Data = null
                };
            }

            // 2. Registration start < registration end
            if (registrationStartDate.HasValue && registrationEndDate.HasValue &&
                registrationStartDate.Value >= registrationEndDate.Value)
            {
                return new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "RegistrationStartDate must be less than RegistrationEndDate.",
                    Data = null
                };
            }

            // 3. Registration close > registration end
            if (registrationCloseDate.HasValue && registrationEndDate.HasValue &&
                registrationCloseDate.Value <= registrationEndDate.Value)
            {
                return new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "RegistrationCloseDate must be greater than RegistrationEndDate.",
                    Data = null
                };
            }

            // 4. Registration dates must be within Start / End
            if (registrationStartDate.HasValue &&
                (registrationStartDate < startDate || registrationStartDate > endDate))
            {
                return new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "RegistrationStartDate must be within StartDate and EndDate.",
                    Data = null
                };
            }

            if (registrationEndDate.HasValue &&
                (registrationEndDate < startDate || registrationEndDate > endDate))
            {
                return new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "RegistrationEndDate must be within StartDate and EndDate.",
                    Data = null
                };
            }

            if (registrationCloseDate.HasValue &&
                (registrationCloseDate < startDate || registrationCloseDate > endDate))
            {

                return new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "RegistrationCloseDate must be within StartDate and EndDate.",
                    Data = null
                };
            }

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Validation successful",
                Data = null
            };
        }
    }
}
