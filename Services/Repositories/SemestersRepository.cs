using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;

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

                var foundSemester = _context.Semesters.FirstOrDefault(x => x.Id == model.Id);
                if (foundSemester != null)
                {
                    return new GeneralResponse
                    {
                        Data = null,
                        StatusCore = 400,
                        Message = "Semester already exists"
                    };
                }

                var semester = new Semesters
                {
                    SessionId = model.SessionId,
                    SemesterId = model.SemesterId,
                    SemesterName = model.SemesterName,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                };

                await _context.Semesters.AddAsync(model);
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

        public async Task<GeneralResponse> DeleteSemesterAsync(int Id)
        {
            try
            {
                var foundSemester = _context.Semesters.FirstOrDefault(x => x.Id == Id);
                if (foundSemester == null)
                {
                    return new GeneralResponse
                    {
                        Data = null,
                        StatusCore = 404,
                        Message = "Semester not found"
                    };
                }

                await _context.Semesters.RemoveAsync(foundSemester);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return new GeneralResponse
                {
                    Data = null,
                    StatusCore = 500,
                    Message = ex.Message
                };
            }
        }

        public Task<GeneralResponse> GetAllSemestersAsync()
        {
            var response = new GeneralResponse();
            try
            {
                var semester = _context.Semesters.ToList();

                response.Data = semester;
                response.StatusCore = 200;
                response.Message = "Successful";

            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    Data = null, StatusCore = 500, Message = ex.Message
                };
            }
            return response;
        }

        public Task<GeneralResponse> GetSemesterByIdAsync(int Id)
        {
            var response = new GeneralResponse();
            try
            {
                var semester = _context.Semesters.FirstOrDefault(x => x.Id == Id);

                response.Data = semester;
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

        public Task<GeneralResponse> UpdateSemesterAsync(int Id, SemestersDto model)
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

                var foundSemester = _context.Semesters.FirstOrDefault(x => x.Id == Id);
                if (foundSemester == null)
                {
                    return new GeneralResponse
                    {
                        Data = null,
                        StatusCore = 404,
                        Message = "Semester not found"
                    };
                }

                foundSemester.SemesterName = model.SemesterName;
                foundSemester.StartDate = model.StartDate;
                foundSemester.EndDate = model.EndDate;

                await _context.Semesters.UpdateAsync(foundSemester);
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
