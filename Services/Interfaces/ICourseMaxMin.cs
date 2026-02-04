using EduReg.Common;
using EduReg.Models.Dto;

namespace EduReg.Services.Interfaces
{
    public interface ICourseMaxMin
    {
        Task<GeneralResponse> CreateCourseMaxMinAsync(CourseMaxMinDto dto);
        Task<GeneralResponse> GetCourseMaxMinByIdAsync(long id);
        Task<GeneralResponse> GetAllCourseMaxMinAsync(string institutionShortName);
        Task<GeneralResponse> UpdateCourseMaxMinAsync(long id, UpdateCourseMaxMinDto dto);
        Task<GeneralResponse> DeleteCourseMaxMinAsync(long id);
    }
}
