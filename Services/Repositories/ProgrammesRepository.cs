using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduReg.Services.Repositories
{
    public class ProgrammesRepository : IProgrammes
    {
        private readonly ApplicationDbContext _context;
        public ProgrammesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GeneralResponse> CreateProgrammeAsync(ProgrammesDto model)
        {
            try
            {

                if (await _context.Programmes.AnyAsync(p => p.ProgrammeCode == model.ProgrammeCode))
                {
                    return new GeneralResponse
                    {
                        StatusCode = 403,
                        Message = "A Programme with this Programme Code already exists"
                    };
                }

                var programme = new Programmes
                {
                    DepartmentCode = model.DepartmentCode,
                    ProgrammeCode = model.ProgrammeCode,
                    ProgrammeName = model.ProgrammeName,
                    Description = model.Description,
                    Duration = model.Duration,
                    NumberOfSemesters = model.NumberOfSemesters,
                    MaximumNumberOfSemesters = model.MaximumNumberOfSemesters
                };

                await _context.Programmes.AddAsync(programme);
                await _context.SaveChangesAsync();

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "Programme created successfully",
                    Data = programme
                };

            }

            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"An error occurred while creating the Programme: {ex.Message}"                    
                };
            }

        }

        public async Task<GeneralResponse> DeleteProgrammeAsync(long Id)
        {
            try
            {
                var programme = await _context.Programmes.FindAsync(Id);
                if (programme == null)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 404,
                        Message = "Programme not found"
                    };
                }

                _context.Programmes.Remove(programme);
                await _context.SaveChangesAsync();

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "Programme deleted successfully",
                    Data = programme
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"An error occurred while deleting the Programme: {ex.Message}"
                };
            }
        }

        public async Task<GeneralResponse> GetAllProgrammesAsync()
        {
            try
            {
                var programmes = await _context.Programmes.ToListAsync();

                var programmesDto = programmes.Select(p => new ProgrammesDto
                {
                    DepartmentCode = p.DepartmentCode,
                    ProgrammeCode = p.ProgrammeCode,
                    ProgrammeName = p.ProgrammeName,
                    Description = p.Description,
                    Duration = p.Duration,
                    NumberOfSemesters = p.NumberOfSemesters,
                    MaximumNumberOfSemesters = p.MaximumNumberOfSemesters
                }).ToList();

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "Programmes retrieved successfully",
                    Data = programmesDto
                };
            }

            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"An error occurred while retrieving Programmes: {ex.Message}"
                };
            }
        }

        public async Task<GeneralResponse> GetProgrammeByIdAsync(long Id)
        {
            try
            {
                var programme = await _context.Programmes.FindAsync(Id);

                if (programme == null)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 404,
                        Message = "Programme not found"
                    };
                }

                var programmeDto = new ProgrammesDto
                {
                    DepartmentCode = programme.DepartmentCode,
                    ProgrammeCode = programme.ProgrammeCode,
                    ProgrammeName = programme.ProgrammeName,
                    Description = programme.Description,
                    Duration = programme.Duration,
                    NumberOfSemesters = programme.NumberOfSemesters,
                    MaximumNumberOfSemesters = programme.MaximumNumberOfSemesters
                };

                return new GeneralResponse 
                {
                    StatusCode = 200,
                    Message = "Programme retrieved successfully",
                    Data = programmeDto
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"An error occurred while retrieving the Programme: {ex.Message}"
                };
            }
        }

        public async Task<GeneralResponse> GetProgrammeByNameAsync(string ProgrammeName)
        {
            try
            {
                var programme = await _context.Programmes.FirstOrDefaultAsync(p => p.ProgrammeName.ToLower().Contains(ProgrammeName.ToLower()));

                if (programme == null)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 404,
                        Message = "Programme not found"
                    };
                }

                var programmeDto = new ProgrammesDto
                {
                    DepartmentCode = programme.DepartmentCode,
                    ProgrammeCode = programme.ProgrammeCode,
                    ProgrammeName = programme.ProgrammeName,
                    Description = programme.Description,
                    Duration = programme.Duration,
                    NumberOfSemesters = programme.NumberOfSemesters,
                    MaximumNumberOfSemesters = programme.MaximumNumberOfSemesters
                };

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "Programme retrieved successfully",
                    Data = programmeDto
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"An error occurred while retrieving the Programme: {ex.Message}"
                };
            }
        }

        public async Task<GeneralResponse> UpdateProgrammeAsync(long Id, ProgrammesDto model)
        {
            try
            {
                var programme = await _context.Programmes.FindAsync(Id);

                if (programme == null)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 404,
                        Message = "Programme not found"
                    };
                }

                programme.DepartmentCode = model.DepartmentCode;
                programme.ProgrammeCode = model.ProgrammeCode;
                programme.ProgrammeName = model.ProgrammeName;
                programme.Description = model.Description;
                programme.Duration = model.Duration;
                programme.NumberOfSemesters = model.NumberOfSemesters;
                programme.MaximumNumberOfSemesters = model.MaximumNumberOfSemesters;

                _context.Programmes.Update(programme);
                await _context.SaveChangesAsync();

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "Programme updated successfully",
                    Data = programme
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"An error occurred while updating the Programme: {ex.Message}"
                };
            }
        }
    }
}
