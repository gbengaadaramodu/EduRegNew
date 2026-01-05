using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EduReg.Common;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;

namespace EduReg.Managers
{
    public class CoursesManager : IDepartmentCourses, IProgramCourses, ICourseSchedule
    {
        private readonly IDepartmentCourses _departmentRepo;
        private readonly IProgramCourses _programRepo;
        private readonly ICourseSchedule _scheduleRepo;

        public CoursesManager(
            IDepartmentCourses departmentcourses,
            IProgramCourses programcourses,
            ICourseSchedule courseScheduleRepo)
        {
            _departmentRepo = departmentcourses;
            _programRepo = programcourses;
            _scheduleRepo = courseScheduleRepo;
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

        public Task<GeneralResponse> UpdateDepartmentCourseAsync(int Id, DepartmentCoursesDto model)
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

        public Task<GeneralResponse> DeleteDepartmentCourseAsync(int Id)
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

        public Task<GeneralResponse> GetDepartmentCoursesByIdAsync(int Id)
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

        public Task<GeneralResponse> GetAllDepartmentsByCoursesAsync()
        {
            return _departmentRepo.GetAllDepartmentsByCoursesAsync();
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

         

        public Task<GeneralResponse> UpdateProgramCourseAsync(int Id, ProgramCoursesDto model)
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

        public Task<GeneralResponse> DeleteProgramCourseAsync(int Id)
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

        public Task<GeneralResponse> GetProgramCoursesByIdAsync(int Id)
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

        public Task<GeneralResponse> GetAllProgramsByCoursesAsync()
        {
            return _programRepo.GetAllProgramsByCoursesAsync();
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

        public Task<GeneralResponse> GetAllCourseSchedulesAsync()
        {
            return _scheduleRepo.GetAllCourseSchedulesAsync();
        }
    }
}
