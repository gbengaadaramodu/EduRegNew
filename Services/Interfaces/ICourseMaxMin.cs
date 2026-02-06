using EduReg.Common;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request;

namespace EduReg.Services.Interfaces
{
    public interface ICourseMaxMin
    {
        Task<GeneralResponse> CreateCourseMaxMinAsync(string institutionShortName,CourseMaxMinDto dto);
        Task<GeneralResponse> GetCourseMaxMinByIdAsync(long id);
        Task<GeneralResponse> GetAllCourseMaxMinAsync(string institutionShortName, CourseMaxMinFilter filter, PagingParameters paging);
        Task<GeneralResponse> UpdateCourseMaxMinAsync(long id, UpdateCourseMaxMinDto dto);
        Task<GeneralResponse> DeleteCourseMaxMinAsync(long id);
    }
}
