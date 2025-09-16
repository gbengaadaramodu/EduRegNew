using EduReg.Common;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;

namespace EduReg.Services.Repositories
{
    public class ProgramCoursesRepository : IProgramCourses
    {
        public Task<GeneralResponse> AssignCoursesToProgramsAsync(string departmentShortName, ProgramCoursesDto model)
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

        public Task<GeneralResponse> DeleteProgramCourseAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetAllProgramsByCoursesAsync()
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

        public Task<GeneralResponse> UpdateProgramCourseAsync(int Id, ProgramCoursesDto model)
        {
            throw new NotImplementedException();
        }
    }
}
