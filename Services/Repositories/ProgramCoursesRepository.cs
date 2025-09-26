using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduReg.Services.Repositories
{
    public class ProgramCoursesRepository : IProgramCourses
    {
        private readonly ApplicationDbContext _context;

        public ProgramCoursesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GeneralResponse> AssignCoursesToProgramsAsync(string departmentShortName, ProgramCoursesDto model)
        {
            try
            {
                var course = new ProgramCourses
                {
                    InstitutionShortName = model.InstitutionShortName,
                    DepartmentCode = departmentShortName,
                    ProgrammeCode = model.ProgrammeCode,
                    CourseCode = model.CourseCode,
                    ClassCode = model.ClassCode,
                    LevelName = model.LevelName,
                    Title = model.Title,
                    Units = model.Units,
                    CourseType = model.CourseType,
                    Prerequisite = model.Prerequisite,
                    Description = model.Description
                };

                await _context.ProgramCourses.AddAsync(course);
                await _context.SaveChangesAsync();

                return new GeneralResponse
                {
                    StatusCore = 201,
                    Message = "Course assigned to program successfully.",
                    Data = course
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCore = 500,
                    Message = $"Error: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<GeneralResponse> CreateProgramCourseAsync(ProgramCoursesDto model)
        {
            try
            {
                var course = MapDtoToEntity(model);

                await _context.ProgramCourses.AddAsync(course);
                await _context.SaveChangesAsync();

                return new GeneralResponse
                {
                    StatusCore = 201,
                    Message = "Program course created successfully.",
                    Data = course
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCore = 500,
                    Message = $"Error: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<GeneralResponse> CreateProgramCourseAsync(List<ProgramCoursesDto> models)
        {
            try
            {
                var entities = models.Select(MapDtoToEntity).ToList();

                await _context.ProgramCourses.AddRangeAsync(entities);
                await _context.SaveChangesAsync();

                return new GeneralResponse
                {
                    StatusCore = 201,
                    Message = "Program courses created successfully.",
                    Data = entities
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCore = 500,
                    Message = $"Error: {ex.Message}",
                    Data = null
                };
            }
        }

        

        public async Task<GeneralResponse> UpdateProgramCourseAsync(int id, ProgramCoursesDto model)
        {
            var course = await _context.ProgramCourses.FindAsync(id);
            if (course == null)
            {
                return new GeneralResponse
                {
                    StatusCore = 404,
                    Message = "Program course not found",
                    Data = null
                };
            }

            course.CourseCode = model.CourseCode;
            course.ClassCode = model.ClassCode;
            course.LevelName = model.LevelName;
            course.Title = model.Title;
            course.Units = model.Units;
            course.CourseType = model.CourseType;
            course.Prerequisite = model.Prerequisite;
            course.Description = model.Description;

            _context.ProgramCourses.Update(course);
            await _context.SaveChangesAsync();

            return new GeneralResponse
            {
                StatusCore = 200,
                Message = "Program course updated successfully",
                Data = course
            };
        }

        public async Task<GeneralResponse> DeleteProgramCourseAsync(int id)
        {
            var course = await _context.ProgramCourses.FindAsync(id);
            if (course == null)
            {
                return new GeneralResponse
                {
                    StatusCore = 404,
                    Message = "Program course not found",
                    Data = null
                };
            }

            _context.ProgramCourses.Remove(course);
            await _context.SaveChangesAsync();

            return new GeneralResponse
            {
                StatusCore = 200,
                Message = "Program course deleted successfully",
                Data = null
            };
        }

        public async Task<GeneralResponse> GetProgramCoursesByIdAsync(int id)
        {
            var course = await _context.ProgramCourses.FindAsync(id);
            if (course == null)
            {
                return new GeneralResponse
                {
                    StatusCore = 404,
                    Message = "Program course not found",
                    Data = null
                };
            }

            return new GeneralResponse
            {
                StatusCore = 200,
                Message = "Success",
                Data = course
            };
        }

        public async Task<GeneralResponse> GetProgramCoursesByProgramNameAsync(string programName)
        {
            var courses = await _context.ProgramCourses
                .Where(x => x.ProgrammeCode == programName)
                .ToListAsync();

            return new GeneralResponse
            {
                StatusCore = 200,
                Message = "Success",
                Data = courses
            };
        }

        public async Task<GeneralResponse> GetAllProgramsByCoursesAsync()
        {
            var courses = await _context.ProgramCourses.ToListAsync();

            return new GeneralResponse
            {
                StatusCore = 200,
                Message = "Success",
                Data = courses
            };
        }

        // Helper method
        private ProgramCourses MapDtoToEntity(ProgramCoursesDto model)
        {
            return new ProgramCourses
            {
                InstitutionShortName = model.InstitutionShortName,
                DepartmentCode = model.DepartmentCode,
                ProgrammeCode = model.ProgrammeCode,
                CourseCode = model.CourseCode,
                ClassCode = model.ClassCode,
                LevelName = model.LevelName,
                Title = model.Title,
                Units = model.Units,
                CourseType = model.CourseType,
                Prerequisite = model.Prerequisite,
                Description = model.Description
            };
        }
    }
}
