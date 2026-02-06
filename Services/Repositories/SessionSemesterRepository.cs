using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request;
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

        public async Task<GeneralResponse> CreateSessionSemesterAsync(string institutionShortName, SessionSemesterDto dto)
        {
            // 1. Business Logic Validation: Date check
            if (dto.RegistrationCloseDate <= dto.RegistrationStartDate)
            {
                return new GeneralResponse { StatusCode = 400, Message = "Registration close date must be after start date." };
            }

            if (dto.ExamEndDate <= dto.ExamStartDate)
            {
                return new GeneralResponse { StatusCode = 400, Message = "Exam end date must be after start date." };
            }

            // 2. Duplicate Check: Ensure this school doesn't already have this session/semester record
            var exists = await _context.SessionSemesters.AnyAsync(x =>
                x.InstitutionShortName == institutionShortName &&
                x.SessionId == dto.SessionId &&
                x.SemesterId == dto.SemesterId &&
                !x.IsDeleted);

            if (exists)
            {
                return new GeneralResponse { StatusCode = 409, Message = "This Session and Semester combination already exists for your institution." };
            }

            // 3. Mapping: Use 'institutionShortName' from the method parameter for security
            var newSemester = new SessionSemester
            {
                InstitutionShortName = institutionShortName,
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
                Created = DateTime.Now // Use server time, not DTO time
            };

            try
            {
                _context.SessionSemesters.Add(newSemester);
                await _context.SaveChangesAsync();

                return new GeneralResponse
                {
                    StatusCode = 201,
                    Message = "SessionSemester created successfully",
                    Data = newSemester
                };
            }
            catch (Exception ex)
            {
                // Log the full exception internally, return a cleaner message to the user
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = "An internal error occurred while saving the record.",
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

        public async Task<GeneralResponse> GetAllSessionSemesterAsync(string institutionShortName, SessionSemesterFilter filter, PagingParameters paging)
        {
            // 1. Initialize the query with Tenant Isolation and Soft Delete check
            var query = _context.SessionSemesters
                .Where(x => x.InstitutionShortName == institutionShortName && !x.IsDeleted)
                .AsNoTracking()
                .AsQueryable();

            // 2.  Filters 
            if (filter.SessionId.HasValue)
                query = query.Where(x => x.SessionId == filter.SessionId);

            if (filter.SemesterId.HasValue)
                query = query.Where(x => x.SemesterId == filter.SemesterId);

            if (filter.IsActive.HasValue)
                query = query.Where(x => x.IsActive == filter.IsActive);

            // Filter for "Currently Open" Registration
            if (filter.IsRegistrationOpen.HasValue && filter.IsRegistrationOpen.Value)
            {
                var today = DateTime.Now;
                query = query.Where(x => today >= x.RegistrationStartDate && today <= x.RegistrationCloseDate);
            }

            // 3. Apply Search (Searching by Reference or specific Title if applicable)
            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                string search = filter.Search.ToLower();
                // Assuming you might want to search by a session name or description if joined, 
                // otherwise searching numeric IDs as strings:
                query = query.Where(x => x.SessionId.ToString().Contains(search));
            }

            // 4. Get Total Count before Pagination
            var totalCount = await query.CountAsync();

            // 5. Apply Sorting and Pagination
            var items = await query
                .OrderByDescending(x => x.CreatedDate) // Uses the CreatedDate from your entity
                .Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize)
                .ToListAsync();

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = $"Retrieved {items.Count} of {totalCount} records",
                Data = new
                {
                    TotalCount = totalCount,
                    PageNumber = paging.PageNumber,
                    PageSize = paging.PageSize,
                    Items = items
                }
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
