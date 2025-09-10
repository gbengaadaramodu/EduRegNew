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
        public Task<GeneralResponse> CreateAcademicSessionAsync(AcademicSessionsDto model)
        {
            var response = new GeneralResponse();
            try
            {
                var foundSession = _context.AcademicSessions.FirstOrDefault(x => x.Id == model.SessionId);
                if (foundSession != null)
                {
                    response.Data = null;
                    response.StatusCore = 400;
                    response.Message = "Session already exists";
                    return response;
                }

                var session = new AcademicSession
                {
                    SessionName = model.SessionName,
                };

                await _context.AcademicSessions.AddAsync(session);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    Data = null,
                    StatusCore = 500,
                    Message = ex.Message
                };
            }
            return response;
        }

        public async Task<GeneralResponse> DeleteAcademicSessionAsync(int Id)
        {
            var response = new GeneralResponse();
            try
            {
                var foundSession = _context.AcademicSessions.FirstOrDefault(x => x.Id == Id);
                if (foundSession != null)
                {
                    _context.AcademicSessions.Remove(foundSession);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    response.Data = null;
                    response.StatusCore = 404;
                    response.Message = "Session not found";
                    return response;
                }

                response.Data = null;
                response.StatusCore = 200;
                response.Message = "Session deleted successfully";
                return response;

            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    Data = null,
                    StatusCore = 500,
                    Message = ex.Message
                };
            }
        }

        public async Task<GeneralResponse> GetAcademicSessionByIdAsync(int Id)
        {
            var response = new GeneralResponse();
            try
            {
                var session = await _context.AcademicSessions.FirstOrDefaultAsync(x => x.SessionId == Id && x.IsDeleted == false);

                if (session == null)
                {
                    response.Data = null;
                    response.StatusCore = 404;
                    response.Message = "Session not found";
                    return response;
                }

                response.Data = session;
                response.StatusCore = 200;
                response.Message = "Successful";

            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    Data = null,
                    StatusCore = 500,
                    Message = ex.Message
                };
            }
            return response;
        }

        public async Task<GeneralResponse> GetAllAcademicSessionsAsync()
        {
            var response = new GeneralResponse();
            try
            {
                var session = await _context.AcademicSessions.Where(x => x.IsDeleted == false).ToListAsync();

                response.Data = session;
                response.StatusCore = 200;
                response.Message = "Successful";

            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    Data = null,
                    StatusCore = 500,
                    Message = ex.Message
                };
            }
            return response;
        }

        public async Task<GeneralResponse> UpdateAcademicSessionAsync(int Id, AcademicSessionsDto model)
        {
            var response = new GeneralResponse();
            try
            {
                var existingSession = _context.AcademicSessions.FirstOrDefault(x => x.Id == Id);
                if (existingSession == null)
                {
                    response.Data = null;
                    response.StatusCore = 404;
                    response.Message = "Session does not exist";
                    return response;
                }

                var duplicateSession = await _context.AcademicSessions.AnyAsync(x => x.SessionName == model.SessionName && x.SessionId != Id);
                if (duplicateSession)
                {
                    response.Data = null;
                    response.StatusCore = 400;
                    response.Message = "Another session with the same name already exists";
                    return response;
                }

                existingSession.SessionName = model.SessionName;

                _context.AcademicSessions.Update(existingSession);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    Data = null,
                    StatusCore = 500,
                    Message = ex.Message
                };
            }
            return response;
        }
    }
}
