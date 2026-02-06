using EduReg.Common;
using EduReg.Models.Dto;

namespace EduReg.Services.Interfaces
{
    public interface ICourseRegistration
    {
        Task<GeneralResponse> CreateCourseRegistrationAsync(CreateCourseRegistrationDto model);
        Task<GeneralResponse> GetCourseRegistration(CourseRegistrationRequestDto model);
        Task<GeneralResponse> GetCourseRegistrationById(long id);
    }
}
