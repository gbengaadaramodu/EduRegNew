using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduReg.Services.Repositories
{
    public class AcademicSessionsRepository : IAcademicSessions
    {
        private readonly ApplicationDbContext _context;
        public AcademicSessionsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
     
        public async Task<GeneralResponse> CreateAcademicSessionAsync(CreateAcademicSessionDto model)
        {
            // Validate dates
            if (model.EndDate <= model.StartDate)
                return new GeneralResponse { StatusCode = 400, Message = "End date must be after start date" };

            // Check for duplicate session
            var exists = await _context.AcademicSessions
                .AnyAsync(x => x.SessionName == model.SessionName
                            && x.BatchShortName == model.BatchShortName
                            && !x.IsDeleted);

            if (exists)
                return new GeneralResponse { StatusCode = 400, Message = "Session already exists for this batch." };

            // Map DTO → Entity
            var entity = new AcademicSession
            {
                SessionName = model.SessionName,
                BatchShortName = model.BatchShortName,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                ActiveStatus = model.ActiveStatus
            };

            await _context.AcademicSessions.AddAsync(entity);
            await _context.SaveChangesAsync();

            // Map Entity → Response DTO
            return new GeneralResponse
            {
                StatusCode = 201,
                Message = "Academic session created successfully",
                Data = new AcademicSessionResponseDto
                {
                    SessionId = entity.SessionId,
                    SessionName = entity.SessionName,
                    SemesterName = entity.SemesterName,
                    BatchShortName = entity.BatchShortName,
                    StartDate = entity.StartDate,
                    EndDate = entity.EndDate
                    
                   
                }
            };


        }

        // --- UPDATE ---
        public async Task<GeneralResponse> UpdateAcademicSessionAsync(long Id, UpdateAcademicSessionDto model)
        {
            var session = await _context.AcademicSessions.FindAsync(Id);
            if (session == null)
                return new GeneralResponse { StatusCode = 404, Message = "Academic session not found" };

            // Partial update only
            if (!string.IsNullOrWhiteSpace(model.SessionName))
                session.SessionName = model.SessionName;

            if (!string.IsNullOrWhiteSpace(model.BatchShortName))
                session.BatchShortName = model.BatchShortName;

            if (model.StartDate.HasValue)
                session.StartDate = model.StartDate.Value;

            if (model.EndDate.HasValue)
                session.EndDate = model.EndDate.Value;

            // Validate dates
            if (session.EndDate <= session.StartDate)
                return new GeneralResponse { StatusCode = 400, Message = "End date must be after start date" };

            // Check for duplicate session (excluding current)
            var exists = await _context.AcademicSessions
                .AnyAsync(s => s.SessionId != Id
                            && s.SessionName == session.SessionName
                            && s.BatchShortName == session.BatchShortName
                            && !s.IsDeleted);

            if (exists)
                return new GeneralResponse { StatusCode = 400, Message = "Another session with the same name already exists for this batch." };

            _context.AcademicSessions.Update(session);
            await _context.SaveChangesAsync();

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Academic session updated successfully",
                Data = new AcademicSessionResponseDto
                {
                    SessionId = session.SessionId,
                    SessionName = session.SessionName,
                    SemesterName = session.SemesterName,
                    BatchShortName = session.BatchShortName,
                    StartDate = session.StartDate,
                    EndDate = session.EndDate

                }
            };
        }

        public async Task<GeneralResponse> DeleteAcademicSessionAsync(long Id)
        {
            var session = await _context.AcademicSessions.FindAsync(Id);

            if (session == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "Academic session not found",
                    Data = null
                };
            }

            _context.AcademicSessions.Remove(session);
            await _context.SaveChangesAsync();

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Academic session deleted successfully",
                Data = null
            };
        }

        public async Task<GeneralResponse> GetAcademicSessionByIdAsync(long Id)
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

            var session = await _context.AcademicSessions.FindAsync(Id);
            if (session == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "Academic session not found",
                    Data = null
                };
            }

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Academic session retrieved successfully",
                Data = session
            };
        }

        public async Task<GeneralResponse> GetAllAcademicSessionsAsync(PagingParameters paging)
        {
            var query = _context.AcademicSessions
            .Where(x => !x.IsDeleted)
            .AsQueryable();

            var totalRecords = await query.CountAsync();

            var sessions = await query
                .OrderByDescending(x => x.Created)
                .Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize)
                .ToListAsync();

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = totalRecords == 0
              ? "No academic sessions found"
              : "Academic sessions retrieved successfully",
                Data = sessions,
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


       
    }
}
