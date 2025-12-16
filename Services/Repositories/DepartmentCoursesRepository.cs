using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduReg.Services.Repositories
{
    public class DepartmentCoursesRepository : IDepartmentCourses
    {
        private readonly ApplicationDbContext _context;
        public DepartmentCoursesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GeneralResponse> CreateDepartmentCourseAsync(DepartmentCoursesDto model)
        {
            try
            {
                var entity = new DepartmentCourses
                {
                    InstitutionShortName = model.InstitutionShortName,
                    DepartmentCode = model.DepartmentCode,
                    CourseCode = model.CourseCode,
                    Title = model.Title,
                    Units = model.Units,
                    CourseType = model.CourseType,
                    Prerequisite = model.Prerequisite,
                    Description = model.Description,
                    CreatedBy = model.CreatedBy,
                    Created = model.Created,
                    ActiveStatus = model.ActiveStatus
                };

                await _context.DepartmentCourses.AddAsync(entity);
                await _context.SaveChangesAsync();

                return new GeneralResponse
                {
                    StatusCode = 201,
                    Message = "Department course created successfully.",
                    Data = entity
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<GeneralResponse> CreateDepartmentCourseAsync(List<DepartmentCoursesDto> models)
        {
            try
            {
                var entities = models.Select(model => new DepartmentCourses
                {
                    InstitutionShortName = model.InstitutionShortName,
                    DepartmentCode = model.DepartmentCode,
                    CourseCode = model.CourseCode,
                    Title = model.Title,
                    Units = model.Units,
                    CourseType = model.CourseType,
                    Prerequisite = model.Prerequisite,
                    Description = model.Description,
                    CreatedBy = model.CreatedBy,
                    Created = model.Created,
                    ActiveStatus = model.ActiveStatus
                }).ToList();

                await _context.DepartmentCourses.AddRangeAsync(entities);
                await _context.SaveChangesAsync();

                return new GeneralResponse
                {
                    StatusCode = 201,
                    Message = "Department courses created successfully.",
                    Data = entities
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<GeneralResponse> UploadDepartmentCourseAsync(byte[] model)
        {
            // TODO: Implement Excel file parsing using EPPlus or similar library
            // For now, return a placeholder response
            return new GeneralResponse
            {
                StatusCode = 501,
                Message = "Upload functionality not yet implemented. Please use individual course creation.",
                Data = null
            };
        }

        public async Task<GeneralResponse> UpdateDepartmentCourseAsync(int id, DepartmentCoursesDto model)
        {
            var course = await _context.DepartmentCourses.FindAsync(id);
            if (course == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "Course not found",
                    Data = null
                };
            }

            course.Title = model.Title;
            course.Units = model.Units;
            course.CourseType = model.CourseType;
            course.Prerequisite = model.Prerequisite;
            course.Description = model.Description;
            course.ActiveStatus = model.ActiveStatus;

            _context.DepartmentCourses.Update(course);
            await _context.SaveChangesAsync();

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Course updated successfully",
                Data = course
            };
        }

        public async Task<GeneralResponse> DeleteDepartmentCourseAsync(int id)
        {
            var course = await _context.DepartmentCourses.FindAsync(id);
            if (course == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "Course not found",
                    Data = null
                };
            }

            _context.DepartmentCourses.Remove(course);
            await _context.SaveChangesAsync();

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Course deleted successfully",
                Data = null
            };
        }

        public async Task<GeneralResponse> GetDepartmentCoursesByIdAsync(int id)
        {
            var course = await _context.DepartmentCourses.FindAsync(id);
            if (course == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "Course not found",
                    Data = null
                };
            }

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Success",
                Data = course
            };
        }

        public async Task<GeneralResponse> GetDepartmentCoursesByDepartmentNameAsync(string shortname)
        {
            var courses = await _context.DepartmentCourses
                .Where(x => x.DepartmentCode == shortname)
                .ToListAsync();

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Success",
                Data = courses
            };
        }

        public async Task<GeneralResponse> GetAllDepartmentsByCoursesAsync()
        {
            var courses = await _context.DepartmentCourses.ToListAsync();
            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Success",
                Data = courses
            };
        }
    }
}
