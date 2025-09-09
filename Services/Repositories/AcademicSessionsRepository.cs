using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;

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
                if (foundSession == null)
                {
                    response.Data = null;
                    response.StatusCore = 404;
                    response.Message = "Session not found";
                }

                _context.AcademicSessions.Remove(foundSession);
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

        public async Task<GeneralResponse> GetAcademicSessionByIdAsync(int Id)
        {
            var response = new GeneralResponse();
            try
            {
                var session = _context.AcademicSessions.FirstOrDefault(x => x.Id == Id);

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
                var session = _context.AcademicSessions.Where(x => x.IsDeleted == false).ToList();

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
