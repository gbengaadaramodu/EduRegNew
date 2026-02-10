using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduReg.Services.Repositories
{
    public class ProgramCoursesRepository : IProgramCourses
    {
        private readonly ApplicationDbContext _context;
        private readonly RequestContext _requestContext;

        public ProgramCoursesRepository(ApplicationDbContext context, RequestContext requestContext)
        {
            _context = context;
            _requestContext = requestContext;
            _requestContext.InstitutionShortName = requestContext.InstitutionShortName.ToUpper();
        }

        public async Task<GeneralResponse> AssignCoursesToProgramsAsync(string departmentShortName, ProgramCoursesDto model)
        {
            try
            {
                model.InstitutionShortName = _requestContext.InstitutionShortName;

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
                    StatusCode = 201,
                    Message = "Course assigned to program successfully.",
                    Data = course
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
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
                    StatusCode = 201,
                    Message = "Program course created successfully.",
                    Data = course
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
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
                    StatusCode = 201,
                    Message = "Program courses created successfully.",
                    Data = entities
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"Error: {ex.Message}",
                    Data = null
                };
            }
        }

        

        public async Task<GeneralResponse> UpdateProgramCourseAsync(long id, ProgramCoursesDto model)
        {
            var course = await _context.ProgramCourses.FindAsync(id);
            if (course == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
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
            course.ActiveStatus = model.ActiveStatus;

            _context.ProgramCourses.Update(course);
            await _context.SaveChangesAsync();

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Program course updated successfully",
                Data = course
            };
        }

        public async Task<GeneralResponse> DeleteProgramCourseAsync(long id)
        {
            var course = await _context.ProgramCourses.FindAsync(id);
            if (course == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "Program course not found",
                    Data = null
                };
            }

            _context.ProgramCourses.Remove(course);
            await _context.SaveChangesAsync();

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Program course deleted successfully",
                Data = null
            };
        }

        public async Task<GeneralResponse> GetProgramCoursesByIdAsync(long id)
        {
            var course = await _context.ProgramCourses.FindAsync(id);
            if (course == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "Program course not found",
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

        public async Task<GeneralResponse> GetProgramCoursesByProgramNameAsync(string programName)
        {
            var courses = await _context.ProgramCourses
                .Where(x => x.ProgrammeCode == programName)
                .ToListAsync();

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Success",
                Data = courses
            };
        }

        public async Task<GeneralResponse> GetAllProgramsByCoursesAsync(PagingParameters paging,ProgramCourseFilter filter)
        {
            var query = _context.ProgramCourses.AsQueryable();

           
            // Filters
            

            if (!string.IsNullOrWhiteSpace(filter?.InstitutionShortName))
                query = query.Where(x => x.InstitutionShortName == filter.InstitutionShortName);

            if (!string.IsNullOrWhiteSpace(filter?.DepartmentCode))
                query = query.Where(x => x.DepartmentCode == filter.DepartmentCode);

            if (!string.IsNullOrWhiteSpace(filter?.ProgrammeCode))
                query = query.Where(x => x.ProgrammeCode == filter.ProgrammeCode);

            if (!string.IsNullOrWhiteSpace(filter?.CourseCode))
                query = query.Where(x => x.CourseCode == filter.CourseCode);

            if (!string.IsNullOrWhiteSpace(filter?.ClassCode))
                query = query.Where(x => x.ClassCode == filter.ClassCode);

            if (!string.IsNullOrWhiteSpace(filter?.LevelName))
                query = query.Where(x => x.LevelName == filter.LevelName);

            if (!string.IsNullOrWhiteSpace(filter?.CourseType))
                query = query.Where(x => x.CourseType == filter.CourseType);

            if (filter?.MinUnits.HasValue == true)
                query = query.Where(x => x.Units >= filter.MinUnits.Value);

            if (filter?.MaxUnits.HasValue == true)
                query = query.Where(x => x.Units <= filter.MaxUnits.Value);

            // Generic Search
            if (!string.IsNullOrWhiteSpace(filter?.Search))
            {
                query = query.Where(x =>
                    (x.Title != null && x.Title.Contains(filter.Search)) ||
                    (x.CourseCode != null && x.CourseCode.Contains(filter.Search)) ||
                    (x.ProgrammeCode != null && x.ProgrammeCode.Contains(filter.Search)) ||
                    (x.DepartmentCode != null && x.DepartmentCode.Contains(filter.Search)) ||
                    (x.LevelName != null && x.LevelName.Contains(filter.Search)) ||
                    (x.ClassCode != null && x.ClassCode.Contains(filter.Search))
                );
            }

            // Pagination
            var totalRecords = await query.CountAsync();

            var courses = await query
                .OrderBy(x => x.Title)
                .Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize)
                .ToListAsync();

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = totalRecords == 0
                    ? "No program courses found"
                    : "Program courses retrieved successfully",
                Data = courses,
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
                Description = model.Description,
                ActiveStatus = model.ActiveStatus
            };
        }

       
    }
}
