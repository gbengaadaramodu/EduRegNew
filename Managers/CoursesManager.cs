using EduReg.Common;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;

namespace EduReg.Managers
{
    public class CoursesManager : IDepartmentCourses, IProgramCourses
    {
        private readonly IDepartmentCourses _departmentcourses;
        private readonly IProgramCourses _programcourses;

        public CoursesManager(IDepartmentCourses departmentcourses, IProgramCourses programcourses)
        {
            _departmentcourses = departmentcourses;
            _programcourses = programcourses;

        }

        public Task<GeneralResponse> AssignCoursesToProgramsAsync(string departmentShortName, ProgramCoursesDto model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> CreateDepartmentCourseAsync(DepartmentCoursesDto model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> CreateDepartmentCourseAsync(List<DepartmentCoursesDto> model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> CreateDepartmentCourseAsync(byte[] model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> CreateProgramCourseAsync(ProgramCoursesDto model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> CreateProgramCourseAsync(List<ProgramCoursesDto> model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> CreateProgramCourseAsync(byte[] model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> DeleteDepartmentCourseAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> DeleteProgramCourseAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetAllDepartmentsByCoursesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetAllProgramsByCoursesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetDepartmentCoursesByDepartmentNameAsync(string shortname)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetDepartmentCoursesByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetProgramCoursesByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetProgramCoursesByProgramNameAsync(string programName)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> UpdateDepartmentCourseAsync(int Id, DepartmentCoursesDto model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> UpdateProgramCourseAsync(int Id, ProgramCoursesDto model)
        {
            throw new NotImplementedException();
        }
    }
}
