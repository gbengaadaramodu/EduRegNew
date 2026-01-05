using EduReg.Common;
using EduReg.Models.Dto;

namespace EduReg.Services.Interfaces
{
    public interface IDepartmentCourses
    {
        Task<GeneralResponse> CreateDepartmentCourseAsync(DepartmentCoursesDto model);
        Task<GeneralResponse> CreateDepartmentCourseAsync(List<DepartmentCoursesDto> model);
        Task<GeneralResponse> UploadDepartmentCourseAsync(byte[] model); // upload excel file, csv,flat file

        Task<GeneralResponse> UpdateDepartmentCourseAsync(int Id, DepartmentCoursesDto model);

        Task<GeneralResponse> DeleteDepartmentCourseAsync(int Id);

        Task<GeneralResponse> GetDepartmentCoursesByIdAsync(int Id); 
        Task<GeneralResponse> GetDepartmentCoursesByDepartmentNameAsync(string shortname); 
        
        Task<GeneralResponse> GetAllDepartmentsByCoursesAsync(PagingParameters paging);
    }
}
