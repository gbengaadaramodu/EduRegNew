using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using static EduReg.Services.Repositories.SessionSemesterRepository;

namespace EduReg.Services.Repositories
{
    public class SessionSemesterRepository : ISessionSemester
    {
        private readonly ApplicationDbContext _context;

        public SessionSemesterRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GeneralResponse> CreateSessionSemesterAsync(SessionSemesterDto dto)
        {
            var newSemester = new SessionSemester
            {
                InstitutionShortName = dto.InstitutionShortName,
                SessionId = dto.SessionId,
                SemesterId = dto.SemesterId,
                IsActive = dto.IsActive,
                RegistrationStartDate = dto.RegistrationStartDate,
                RegistrationCloseDate = dto.RegistrationCloseDate,
                ExamStartDate = dto.ExamStartDate,
                ExamEndDate = dto.ExamEndDate,

                // Base fields
                ActiveStatus = dto.ActiveStatus,
                CreatedBy = dto.CreatedBy,
                Created = dto.Created // From CommonBaseDto
            };

            try
            {
                _context.SessionSemesters.Add(newSemester);
                await _context.SaveChangesAsync();

                return new GeneralResponse
                {
                    StatusCode = 201, // Created
                    Message = "SessionSemester created successfully",
                    Data = newSemester
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"Database Error: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<GeneralResponse> GetSessionSemesterByIdAsync(long id)
        {
            var record = await _context.SessionSemesters
          .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

            if (record == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "SessionSemester not found",
                    Data = null
                };
            }

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Success",
                Data = record
            };
        }

        public async Task<GeneralResponse> GetAllSessionSemesterAsync(string institutionShortName)
        {
            var list = await _context.SessionSemesters
                .Where(x => x.InstitutionShortName == institutionShortName && !x.IsDeleted)
                .OrderByDescending(x => x.Created)
                .ToListAsync();

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = $"Found {list.Count} records",
                Data = list
            };
        }

        public async Task<GeneralResponse> UpdateSessionSemesterAsync(long id, UpdateSessionSemesterDto dto)
        {
            // 1. Find the record using the ID from the URL/Interface
            var found = await _context.SessionSemesters
          .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

            if (found == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "SessionSemester not found",
                    Data = null
                };
            }

            found.RegistrationStartDate = dto.RegistrationStartDate;
            found.RegistrationCloseDate = dto.RegistrationCloseDate;
            found.ExamStartDate = dto.ExamStartDate;
            found.ExamEndDate = dto.ExamEndDate;

            // Update base fields from the DTO
            found.ActiveStatus = dto.ActiveStatus;
            found.CreatedBy = dto.CreatedBy;

            try
            {
                // 3. Save changes
                await _context.SaveChangesAsync();

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "SessionSemester updated successfully",
                    Data = found
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"Internal server error: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<GeneralResponse> DeleteSessionSemesterAsync(long id)
        {
            // 1. Find the record by its long ID (from CommonBase)
            var record = await _context.SessionSemesters
         .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

            if (record == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "Record not found or already deleted",
                    Data = null
                };
            }

            // 2. VALIDATION: Check if it has been "Used"
            // We check if any student has registered for courses in this session/semester
            bool isUsedInRegistration = await _context.CourseRegistrations
                .AnyAsync(cr => cr.Id == id);

            // You can add other checks here (e.g., FeePayments, Timetables)
            //bool isUsedInFees = await _context.FeePayments
            //    .AnyAsync(fp => fp.Id == id);

            //if (isUsedInRegistration || isUsedInFees)
            //{
            //    return new GeneralResponse
            //    {
            //        StatusCode = 400,
            //        Message = "Cannot delete: This semester has active student registrations or fee records associated with it."
            //    };
            //}

            // 3. If not used, proceed with Soft Delete
            record.IsDeleted = true;
            record.ActiveStatus = 0;

            try
            {
                await _context.SaveChangesAsync();

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "SessionSemester deleted successfully",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"Error deleting record: {ex.Message}",
                    Data = null
                };
            }
        }
    }

}
