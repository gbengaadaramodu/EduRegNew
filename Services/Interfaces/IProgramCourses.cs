using EduReg.Common;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request;

namespace EduReg.Services.Interfaces
{
    public interface IProgramCourses
    {
       Task<GeneralResponse> AssignCoursesToProgramsAsync(string departmentShortName, ProgramCoursesDto model); // DepartmentShortName
        Task<GeneralResponse> CreateProgramCourseAsync(ProgramCoursesDto model);
        Task<GeneralResponse> CreateProgramCourseAsync(List<ProgramCoursesDto> model);
        //Task<GeneralResponse> CreateProgramCourseAsync(byte[] model); // EXCEL, CSV, Flat file upload
        Task<GeneralResponse> UpdateProgramCourseAsync(long Id, ProgramCoursesDto model);
        Task<GeneralResponse> DeleteProgramCourseAsync(long Id);
        Task<GeneralResponse> GetProgramCoursesByIdAsync(long Id);
        Task<GeneralResponse> GetProgramCoursesByProgramNameAsync(string programName);//Shortname
        Task<GeneralResponse> GetAllProgramsByCoursesAsync(PagingParameters paging, ProgramCourseFilter filter);
    }
}
