using EduReg.Common;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request;
using EduReg.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EduReg.Managers
{
    public class CoursesManager : IDepartmentCourses, IProgramCourses, ICourseSchedule, ICourseMaxMin
    {
        private readonly IDepartmentCourses _departmentRepo;
        private readonly IProgramCourses _programRepo;
        private readonly ICourseSchedule _scheduleRepo;
        private readonly ICourseMaxMin _maxMin;

        public CoursesManager(
            IDepartmentCourses departmentcourses,
            IProgramCourses programcourses,
            ICourseSchedule courseScheduleRepo,
            ICourseMaxMin maxMin)
        {
            _departmentRepo = departmentcourses;
            _programRepo = programcourses;
            _scheduleRepo = courseScheduleRepo;
            _maxMin = maxMin;
        }

        // -----------------------
        // DepartmentCourses methods
        // -----------------------
        public Task<GeneralResponse> CreateDepartmentCourseAsync(DepartmentCoursesDto model)
        {
            // Possibly validate: model.CourseCode not null, Institution exists, etc.
            if (model == null)
            {
                return Task.FromResult(new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "Invalid input: model is null",
                    Data = null
                });
            }
            // Could check duplicates first, etc.

            return _departmentRepo.CreateDepartmentCourseAsync(model);
        }

        public Task<GeneralResponse> CreateDepartmentCourseAsync(List<DepartmentCoursesDto> model)
        {
            if (model == null || model.Count == 0)
            {
                return Task.FromResult(new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "Invalid input: no courses to create",
                    Data = null
                });
            }

            return _departmentRepo.CreateDepartmentCourseAsync(model);
        }

        public Task<GeneralResponse> UploadDepartmentCourseAsync(byte[] model)
        {
            if (model == null || model.Length == 0)
            {
                return Task.FromResult(new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "Invalid file upload",
                    Data = null
                });
            }

            return _departmentRepo.UploadDepartmentCourseAsync(model);
        }

        public Task<GeneralResponse> UpdateDepartmentCourseAsync(long Id, DepartmentCoursesDto model)
        {
            if (Id <= 0)
            {
                return Task.FromResult(new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "Invalid Id",
                    Data = null
                });
            }
            if (model == null)
            {
                return Task.FromResult(new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "Invalid input: model is null",
                    Data = null
                });
            }

            return _departmentRepo.UpdateDepartmentCourseAsync(Id, model);
        }

        public Task<GeneralResponse> DeleteDepartmentCourseAsync(long Id)
        {
            if (Id <= 0)
            {
                return Task.FromResult(new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "Invalid Id",
                    Data = null
                });
            }

            return _departmentRepo.DeleteDepartmentCourseAsync(Id);
        }

        public Task<GeneralResponse> GetDepartmentCoursesByIdAsync(long Id)
        {
            if (Id <= 0)
            {
                return Task.FromResult(new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "Invalid Id",
                    Data = null
                });
            }

            return _departmentRepo.GetDepartmentCoursesByIdAsync(Id);
        }

        public Task<GeneralResponse> GetDepartmentCoursesByDepartmentNameAsync(string shortname)
        {
            if (string.IsNullOrWhiteSpace(shortname))
            {
                return Task.FromResult(new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "Department shortname required",
                    Data = null
                });
            }

            return _departmentRepo.GetDepartmentCoursesByDepartmentNameAsync(shortname);
        }

        public Task<GeneralResponse> GetAllDepartmentsByCoursesAsync(PagingParameters paging, DepartmentCourseFilter filter)
        {
            return _departmentRepo.GetAllDepartmentsByCoursesAsync(paging, filter);
        }

        // -----------------------
        // ProgramCourses methods
        // -----------------------

        public Task<GeneralResponse> AssignCoursesToProgramsAsync(string departmentShortName, ProgramCoursesDto model)
        {
            if (string.IsNullOrWhiteSpace(departmentShortName))
            {
                return Task.FromResult(new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "Department short name is required",
                    Data = null
                });
            }
            if (model == null)
            {
                return Task.FromResult(new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "Invalid program course model",
                    Data = null
                });
            }

            // Possibly ensure that model.DepartmentCode == departmentShortName
            // Enforce consistency
            model.DepartmentCode = departmentShortName;

            return _programRepo.AssignCoursesToProgramsAsync(departmentShortName, model);
        }

        public Task<GeneralResponse> CreateProgramCourseAsync(ProgramCoursesDto model)
        {
            if (model == null)
            {
                return Task.FromResult(new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "Invalid program course model",
                    Data = null
                });
            }

            return _programRepo.CreateProgramCourseAsync(model);
        }

        public Task<GeneralResponse> CreateProgramCourseAsync(List<ProgramCoursesDto> model)
        {
            if (model == null || model.Count == 0)
            {
                return Task.FromResult(new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "No program courses to create",
                    Data = null
                });
            }

            return _programRepo.CreateProgramCourseAsync(model);
        }

         

        public Task<GeneralResponse> UpdateProgramCourseAsync(long Id, ProgramCoursesDto model)
        {
            if (Id <= 0)
            {
                return Task.FromResult(new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "Invalid Id",
                    Data = null
                });
            }
            if (model == null)
            {
                return Task.FromResult(new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "Invalid model",
                    Data = null
                });
            }

            return _programRepo.UpdateProgramCourseAsync(Id, model);
        }

        public Task<GeneralResponse> DeleteProgramCourseAsync(long Id)
        {
            if (Id <= 0)
            {
                return Task.FromResult(new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "Invalid Id",
                    Data = null
                });
            }

            return _programRepo.DeleteProgramCourseAsync(Id);
        }

        public Task<GeneralResponse> GetProgramCoursesByIdAsync(long Id)
        {
            if (Id <= 0)
            {
                return Task.FromResult(new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "Invalid Id",
                    Data = null
                });
            }

            return _programRepo.GetProgramCoursesByIdAsync(Id);
        }

        public Task<GeneralResponse> GetProgramCoursesByProgramNameAsync(string programName)
        {
            if (string.IsNullOrWhiteSpace(programName))
            {
                return Task.FromResult(new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "Programme code/name required",
                    Data = null
                });
            }

            return _programRepo.GetProgramCoursesByProgramNameAsync(programName);
        }

        public Task<GeneralResponse> GetAllProgramsByCoursesAsync(PagingParameters paging, ProgramCourseFilter filter)
        {
            return _programRepo.GetAllProgramsByCoursesAsync(paging, filter);
        }

        // -----------------------
        // CourseSchedule methods
        // -----------------------

        public Task<GeneralResponse> CreateCourseScheduleAsync(CourseScheduleDto model)
        {
            if (model == null)
            {
                return Task.FromResult(new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "Invalid schedule model",
                    Data = null
                });
            }

            return _scheduleRepo.CreateCourseScheduleAsync(model);
        }

        public Task<GeneralResponse> CreateCourseScheduleAsync(List<CourseScheduleDto> model)
        {
            if (model == null || model.Count == 0)
            {
                return Task.FromResult(new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "No course schedules to create",
                    Data = null
                });
            }

            return _scheduleRepo.CreateCourseScheduleAsync(model);
        }

       

        public Task<GeneralResponse> UpdateCourseScheduleAsync(long Id, CourseScheduleDto model)
        {
            if (Id <= 0)
            {
                return Task.FromResult(new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "Invalid Id",
                    Data = null
                });
            }
            if (model == null)
            {
                return Task.FromResult(new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "Invalid model",
                    Data = null
                });
            }
            return _scheduleRepo.UpdateCourseScheduleAsync(Id, model);
        }

        public Task<GeneralResponse> DeleteCourseScheduleAsync(long Id)
        {
            if (Id <= 0)
            {
                return Task.FromResult(new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "Invalid Id",
                    Data = null
                });
            }
            return _scheduleRepo.DeleteCourseScheduleAsync(Id);
        }

        public Task<GeneralResponse> DeleteManyCourseSchedulesAsync(List<CourseScheduleDto> model)
        {
            if (model == null || model.Count == 0)
            {
                return Task.FromResult(new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "No schedule DTOs provided to delete",
                    Data = null
                });
            }
            return _scheduleRepo.DeleteManyCourseSchedulesAsync(model);
        }

        public Task<GeneralResponse> DeleteManyCourseSchedulesAsync(List<long> Id)
        {
            if (Id == null || Id.Count == 0)
            {
                return Task.FromResult(new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "No IDs provided for delete many",
                    Data = null
                });
            }
            return _scheduleRepo.DeleteManyCourseSchedulesAsync(Id);
        }

        public Task<GeneralResponse> GetCourseScheduleByIdAsync(long Id)
        {
            if (Id <= 0)
            {
                return Task.FromResult(new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "Invalid Id",
                    Data = null
                });
            }
            return _scheduleRepo.GetCourseScheduleByIdAsync(Id);
        }

        public Task<GeneralResponse> GetCourseScheduleByCourseCodeAsync(string courseCode)
        {
            if (string.IsNullOrWhiteSpace(courseCode))
            {
                return Task.FromResult(new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "Course code required",
                    Data = null
                });
            }
            return _scheduleRepo.GetCourseScheduleByCourseCodeAsync(courseCode);
        }

        public Task<GeneralResponse> GetAllCourseSchedulesAsync(PagingParameters paging, CourseScheduleFilter filter)
        {
            return _scheduleRepo.GetAllCourseSchedulesAsync(paging, filter);
        }


        //************
        //CourseMaxMin
        //************
        public Task<GeneralResponse> CreateCourseMaxMinAsync(CourseMaxMinDto dto)
        {
            return _maxMin.CreateCourseMaxMinAsync(dto);
        }

        public Task<GeneralResponse> GetCourseMaxMinByIdAsync(long id)
        {

            if (id <= 0)
            {
                return Task.FromResult(new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "Invalid Id",
                    Data = null
                });
            }
            return _maxMin.GetCourseMaxMinByIdAsync(id);
        }

        public Task<GeneralResponse> GetAllCourseMaxMinAsync(string institutionShortName)
        {
            return _maxMin.GetAllCourseMaxMinAsync(institutionShortName);
        }

        public Task<GeneralResponse> UpdateCourseMaxMinAsync(long id, UpdateCourseMaxMinDto dto)
        {

            if (id <= 0)
            {
                return Task.FromResult(new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "Invalid Id",
                    Data = null
                });
            }
            return _maxMin.UpdateCourseMaxMinAsync(id, dto);
        }

        public Task<GeneralResponse> DeleteCourseMaxMinAsync(long id)
        {

            if (id<= 0)
            {
                return Task.FromResult(new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "Invalid Id",
                    Data = null
                });
            }
            return _maxMin.DeleteCourseMaxMinAsync(id);
        }
    }
}
