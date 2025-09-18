using EduReg.Common;
using EduReg.Models.Dto;

namespace EduReg.Services.Interfaces
{
    public interface ICourseSchedule
    {
        Task<GeneralResponse> CreateCourseScheduleAsync(CourseScheduleDto model); // Single
        Task<GeneralResponse> CreateCourseScheduleAsync(List<CourseScheduleDto> model); // Bulk

        Task<GeneralResponse> CreateCourseScheduleAsync(byte[] model); // EXCEL, CSV, Flat file upload

        Task<GeneralResponse> UpdateCourseScheduleAsync(int Id, CourseScheduleDto model);

        Task<GeneralResponse> DeleteCourseScheduleAsync(int Id);
        Task<GeneralResponse> DeleteManyCourseSchedulesAsync(List<CourseScheduleDto> model);
        Task<GeneralResponse> DeleteManyCourseSchedulesAsync(List<int> Id);

        Task<GeneralResponse> GetCourseScheduleByIdAsync(int Id);
        Task<GeneralResponse> GetCourseScheduleByCourseCodeAsync(string courseCode);
        Task<GeneralResponse> GetAllCourseSchedulesAsync();

    }
}
