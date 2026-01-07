using EduReg.Common;
using EduReg.Controllers;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduReg.Services.Repositories
{
    public class FacultiesRepository : IFaculties
    {
        private readonly ApplicationDbContext _context;
        public FacultiesRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<GeneralResponse> CreateFacultyAsync(FacultiesDto model)
        {
            var response = new GeneralResponse();
            try
            {
                var faculties = new Faculties
                {
                    FacultyName = model.FacultyName,
                    FacultyCode = model.FacultyCode,
                    Description = model.Description,
                    InstitutionShortName = model.InstitutionShortName
                };
                _context.Faculties.Add(faculties);
                await _context.SaveChangesAsync();

                response.StatusCode = 200;
                response.Message = "New Faculty created successfully";
                response.Data = faculties;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = "An error occurred, Try again later";
                response.Data = null;
            }

            return response;
        }

        public async Task<GeneralResponse> UpdateFacultyAsync(long Id, FacultiesDto model)
        {
            try
            {
                var faculty = await _context.Faculties.FindAsync(Id);
                if (faculty == null)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 404,
                        Message = "Faculty not found",
                        Data = null
                    };
                }

                faculty.FacultyName = model.FacultyName;
                faculty.FacultyCode = model.FacultyCode;
                faculty.Description = model.Description;
                faculty.InstitutionShortName = model.InstitutionShortName;

                await _context.SaveChangesAsync();

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "Faculty updated successfully",
                    Data = faculty
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while updating faculty",
                    Data = ex.Message 
                };
            }
        }

        public async Task<GeneralResponse> DeleteFacultyAsync(long Id)
        {
            try
            {
                var Faculty = await _context.Faculties.FindAsync(Id);
                if (Faculty == null)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 404,
                        Message = "Faculty not found",
                        Data = null
                    };

                }
                _context.Faculties.Remove(Faculty);
                await _context.SaveChangesAsync();
                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "Faculty deleted successfully",
                    Data = Faculty
                };

            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while deleting faculty",
                    Data = ex.Message
                };
            }



        }


        public async Task<GeneralResponse> GetAllFacultiesAsync(PagingParameters paging)
        {
            try
            {
                var query = _context.Faculties
                    .AsNoTracking();

                var totalCount = await query.CountAsync();

                if (totalCount == 0)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 404,
                        Message = "No faculties found.",
                        Data = null
                    };
                }

                var pagedList = await query
                    .Skip((paging.PageNumber - 1) * paging.PageSize)
                    .Take(paging.PageSize)
                    .ToListAsync();

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "Faculties retrieved successfully.",
                    Data = pagedList
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
        public async Task<GeneralResponse> GetFacultyByIdAsync(long Id)
        {
            try
            {
                var faculty = await _context.Faculties.FindAsync(Id);
                if (faculty == null)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 404,
                        Message = "Faculty not found",
                        Data = null
                    };
                }
                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = $"Faculty with ID {Id} retrieved successfully",
                    Data = faculty
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while retrieving faculty id",
                    Data = ex.Message
                };
            }
        }

        public async Task<GeneralResponse> GetFacultyByCodeAsync(string facultyCode)
        {
            try
            {
                if (string.IsNullOrEmpty(facultyCode))
                {
                    return new GeneralResponse
                    {
                        StatusCode = 400,
                        Message = "Invalid faculty code",
                        Data = null
                    };
                }

                var faculty = await _context.Faculties.FirstOrDefaultAsync(f => f.FacultyCode == facultyCode);
                if (faculty == null)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 404,
                        Message = "Faculty not found",
                        Data = null
                    };
                }
                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = $"Faculty with code {facultyCode} retrieved successfully",
                    Data = faculty
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while retrieving faculty by code",
                    Data = ex.Message
                };
            }
        }
    }
}
