using EduReg.Common;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request;

namespace EduReg.Services.Interfaces
{
    public interface IDepartmentCourses
    {
        Task<GeneralResponse> CreateDepartmentCourseAsync(DepartmentCoursesDto model);
        Task<GeneralResponse> CreateDepartmentCourseAsync(List<DepartmentCoursesDto> model);
        Task<GeneralResponse> UploadDepartmentCourseAsync(byte[] model); // upload excel file, csv,flat file
        Task<GeneralResponse> UploadDepartmentCourseAsync(IFormFile fileUploaded);

        Task<GeneralResponse> UpdateDepartmentCourseAsync(long Id, DepartmentCoursesDto model);

        Task<GeneralResponse> DeleteDepartmentCourseAsync(long Id);

        Task<GeneralResponse> GetDepartmentCoursesByIdAsync(long Id); 
        Task<GeneralResponse> GetDepartmentCoursesByDepartmentNameAsync(string shortname);

        Task<GeneralResponse> GetAllDepartmentsByCoursesAsync(PagingParameters paging, DepartmentCourseFilter filter);
    }
}
