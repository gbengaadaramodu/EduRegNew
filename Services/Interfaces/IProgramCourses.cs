using EduReg.Common;
using EduReg.Models.Dto;

namespace EduReg.Services.Interfaces
{
    public interface IProgramCourses
    {
       Task<GeneralResponse> AssignCoursesToProgramsAsync(string departmentShortName, ProgramCoursesDto model); // DepartmentShortName
        Task<GeneralResponse> CreateProgramCourseAsync(ProgramCoursesDto model);
        Task<GeneralResponse> CreateProgramCourseAsync(List<ProgramCoursesDto> model);
        //Task<GeneralResponse> CreateProgramCourseAsync(byte[] model); // EXCEL, CSV, Flat file upload
        Task<GeneralResponse> UpdateProgramCourseAsync(int Id, ProgramCoursesDto model);
        Task<GeneralResponse> DeleteProgramCourseAsync(int Id);
        Task<GeneralResponse> GetProgramCoursesByIdAsync(int Id);
        Task<GeneralResponse> GetProgramCoursesByProgramNameAsync(string programName);//Shortname
        Task<GeneralResponse> GetAllProgramsByCoursesAsync(PagingParameters paging);
    }
}
