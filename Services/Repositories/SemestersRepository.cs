using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduReg.Services.Repositories
{
    public class SemestersRepository : ISemesters
    {
        private readonly ApplicationDbContext _context;
        public SemestersRepository(ApplicationDbContext context)
        {
            _context = context;
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

            var entity = new Semesters
            {
                InstitutionShortName = model.InstitutionShortName,
                SessionId = model.SessionId,
                SemesterName = model.SemesterName,
                SemesterId = model.SemesterId,
                StartDate = model.StartDate,
                EndDate = model.EndDate
            };

            await _context.Semesters.AddAsync(entity);
            await _context.SaveChangesAsync();

            return new GeneralResponse
            {
                StatusCode = 201,
                Message = "Semester created successfully",
                Data = entity
            };
        }

        public async Task<GeneralResponse> DeleteSemesterAsync(long Id)
        {
            var semester = await _context.Semesters.FindAsync(Id);

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

                // Apply optional filters
                if (filter != null)
                {
                    if (!string.IsNullOrWhiteSpace(filter.InstitutionShortName))
                        query = query.Where(x => x.InstitutionShortName == filter.InstitutionShortName);

                    if (filter.SessionId.HasValue)
                        query = query.Where(x => x.SessionId == filter.SessionId.Value);

                    if (filter.SemesterId.HasValue)
                        query = query.Where(x => x.SemesterId == filter.SemesterId.Value);

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

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = totalRecords == 0
                        ? "No semesters found"
                        : "Semesters retrieved successfully",
                    Data = semesters,
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

            var semester = await _context.Semesters.FindAsync(Id);
            if (semester == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "Semester not found",
                    Data = null
                };
            }

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Semester retrieved successfully",
                Data = semester
            };
        }

        public async Task<GeneralResponse> UpdateSemesterAsync(long Id, SemestersDto model)
        {
            var semester = await _context.Semesters.FindAsync(Id);

            if (semester == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "Semester not found",
                    Data = null
                };
            }

            semester.InstitutionShortName = model.InstitutionShortName;
            semester.SessionId = model.SessionId;
            semester.SemesterName = model.SemesterName;
            semester.SemesterId = model.SemesterId;
            semester.StartDate = model.StartDate;
            semester.EndDate = model.EndDate;

            _context.Semesters.Update(semester);
            await _context.SaveChangesAsync();

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Semester updated successfully",
                Data = semester
            };
        }
    }
}
