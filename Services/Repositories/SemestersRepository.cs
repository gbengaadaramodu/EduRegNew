using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;
using Microsoft.AspNetCore.Http;
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
            var response = new GeneralResponse();
            try
            {
                var existingSession = _context.AcademicSessions.FirstOrDefault(x => x.SessionId == model.SessionId);
                if (existingSession == null)
                {
                    response.Data = null;
                    response.StatusCore = 404;
                    response.Message = "Session does not exist";
                    return response;
                }

                var foundSemester = _context.Semesters.FirstOrDefault(x => x.SessionId == model.SessionId && x.SemesterName == model.SemesterName);
                if (foundSemester != null)
                {
                    response.Data = null;
                    response.StatusCore = 400;
                    response.Message = "Semester already exists";
                    return response;
                }

                var semester = new Semester
                {
                    SessionId = model.SessionId,
                    SemesterId = model.SemesterId,
                    SemesterName = model.SemesterName,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                };

                await _context.Semesters.AddAsync(semester);
                await _context.SaveChangesAsync();
                
                response.StatusCore = 200;
                response.Message = "Semester created successfully";
                response.Data = semester;
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

        public async Task<GeneralResponse> DeleteSemesterAsync(int Id)
        {
            var response = new GeneralResponse();
            try
            {
                var foundSemester = _context.Semesters.FirstOrDefault(x => x.Id == Id);
                if (foundSemester != null)
                {
                    _context.Semesters.Remove(foundSemester);
                    await _context.SaveChangesAsync();
                }

                else
                {
                    response.Data = null;
                    response.StatusCore = 404;
                    response.Message = "Semester not found";
                    return response;
                }

                response.Data = null;
                response.StatusCore = 200;
                response.Message = "Semester deleted successfully";

                return response;
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

        public async Task<GeneralResponse> GetAllSemestersAsync()
        {
            var response = new GeneralResponse();
            try
            {
                var semesters = await _context.Semesters.ToListAsync();

                response.Data = semesters;
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

        public async Task<GeneralResponse> GetSemesterByIdAsync(int Id)
        {
            var response = new GeneralResponse();
            try
            {
                var semester = await _context.Semesters.FirstOrDefaultAsync(x => x.Id == Id);

                if (semester == null)
                {
                    response.Data = null;
                    response.StatusCore = 404;
                    response.Message = "Semester not found";
                    return response;
                }

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

        public async Task<GeneralResponse> UpdateSemesterAsync(int id, SemestersDto model)
        {
            try
            {
                var existingSession = await _context.AcademicSessions
                    .FirstOrDefaultAsync(x => x.SessionId == model.SessionId);
                if (existingSession == null)
                {
                    return new GeneralResponse
                    {
                        Data = null,
                        StatusCore = 404,
                        Message = "Session does not exist"
                    };
                }

                var foundSemester = await _context.Semesters
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (foundSemester == null)
                {
                    return new GeneralResponse
                    {
                        Data = null,
                        StatusCore = 404,
                        Message = "Semester not found"
                    };
                }
                var duplicateSemester = await _context.Semesters.AnyAsync(x => x.SessionId == model.SessionId
                           && x.SemesterName == model.SemesterName
                           && x.SemesterId != id);
                if (duplicateSemester)
                {
                    return new GeneralResponse
                    {
                        Data = null,
                        StatusCore = 400,
                        Message = "Another semester with the same name already exists in this session"
                    };
                }

                foundSemester.SemesterName = model.SemesterName;
                foundSemester.StartDate = model.StartDate;
                foundSemester.EndDate = model.EndDate;
                //foundSemester.SessionId = model.SessionId; // in case of reassigning semester to a new session

                await _context.SaveChangesAsync();

                return new GeneralResponse
                {
                    Data = foundSemester,
                    StatusCore = 200,
                    Message = "Semester updated successfully"
                };
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
