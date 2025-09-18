using EduReg.Common;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;

namespace EduReg.Managers
{
    public class CourseScheduleManager : ICourseSchedule
    {
        private readonly ICourseSchedule _schedule;
        public CourseScheduleManager(ICourseSchedule schedule)
        {
            _schedule = schedule;
        }
        public Task<GeneralResponse> CreateCourseScheduleAsync(CourseScheduleDto model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> CreateCourseScheduleAsync(List<CourseScheduleDto> model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> CreateCourseScheduleAsync(byte[] model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> DeleteCourseScheduleAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> DeleteManyCourseSchedulesAsync(List<CourseScheduleDto> model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> DeleteManyCourseSchedulesAsync(List<int> Id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetAllCourseSchedulesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetCourseScheduleByCourseCodeAsync(string courseCode)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetCourseScheduleByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> UpdateCourseScheduleAsync(int Id, CourseScheduleDto model)
        {
            throw new NotImplementedException();
        }
    }
}
