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
        public async Task<GeneralResponse> CreateAcademicSessionAsync(AcademicSessionsDto model)
        {
            if (model == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "Invalid academic session data",
                    Data = null
                };
            }

            var entity = new AcademicSession
            {
                BatchShortName = model.BatchShortName,
                SessionName = model.SessionName,
                IsDeleted = model.IsDeleted
            };

            await _context.AcademicSessions.AddAsync(entity);
            await _context.SaveChangesAsync();

            return new GeneralResponse
            {
                StatusCode = 201,
                Message = "Academic session created successfully",
                Data = entity
            };
        }

        public async Task<GeneralResponse> DeleteAcademicSessionAsync(int Id)
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

        public async Task<GeneralResponse> GetAcademicSessionByIdAsync(int Id)
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


            

        public async Task<GeneralResponse> UpdateAcademicSessionAsync(int Id, AcademicSessionsDto model)
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

            session.BatchShortName = model.BatchShortName;
            session.SessionName = model.SessionName;
            session.IsDeleted = model.IsDeleted;

            _context.AcademicSessions.Update(session);
            await _context.SaveChangesAsync();

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Academic session updated successfully",
                Data = session
            };
        }
    }
}
