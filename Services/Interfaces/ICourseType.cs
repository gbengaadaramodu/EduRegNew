using EduReg.Common;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request;

namespace EduReg.Services.Interfaces
{
    public interface ICourseType
    {
        
        Task<GeneralResponse> CreateCourseTypeAsync(string institutionShortName , CourseTypeDto dto);
        
        Task<GeneralResponse> GetCourseTypeByIdAsync(long id);
        
        Task<GeneralResponse> GetAllCourseTypesAsync(string institutionShortName, CourseTypeFilter filter, PagingParameters paging);
       
        Task<GeneralResponse> UpdateCourseTypeAsync(long id, UpdateCourseTypeDto dto);

        Task<GeneralResponse> DeleteCourseTypeAsync(long id);
    }
}
