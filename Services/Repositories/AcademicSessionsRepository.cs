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
            try
            {
                var foundSession = _context.AcademicSessions.FirstOrDefault(x => x.Id == model.Id);
                if (foundSession != null)
                {
                    return new GeneralResponse
                    {
                        Data = null,
                        StatusCore = 400,
                        Message = "Session already exists"
                    };
                }

                var session = new AcademicSession
                {
                    SessionName = model.SessionName,
                };

                await _context.AcademicSessions.AddAsync(model);
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
        }

        public Task<GeneralResponse> DeleteAcademicSessionAsync(int Id)
        {
            try
            {
                var foundSession = _context.AcademicSessions.FirstOrDefault(x => x.Id == Id);
                if (foundSession == null)
                {
                    return new GeneralResponse
                    {
                        Data = null,
                        StatusCore = 404,
                        Message = "Session not found"
                    };
                }

                await _context.AcademicSessions.RemoveAsync(foundSession);
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
        }

        public Task<GeneralResponse> GetAcademicSessionByIdAsync(int Id)
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

        public Task<GeneralResponse> GetAllAcademicSessionsAsync()
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

        public Task<GeneralResponse> UpdateAcademicSessionAsync(int Id, AcademicSessionsDto model)
        {
            try
            {
                var existingSession = _context.AcademicSessions.FirstOrDefault(x => x.Id == model.SessionId);
                if (existingSession == null)
                {
                    return new GeneralResponse
                    {
                        Data = null,
                        StatusCore = 404,
                        Message = "Session does not exist"
                    };
                }

                existingSession.SessionName = model.SessionName;

                await _context.AcademicSessions.UpdateAsync(existingSession);
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
        }
    }
}
