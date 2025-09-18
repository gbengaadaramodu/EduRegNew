using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;

namespace EduReg.Services.Repositories
{
    public class CourseScheduleRepository : ICourseSchedule
    {
        private readonly ApplicationDbContext _context;

        public CourseScheduleRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<GeneralResponse> CreateCourseScheduleAsync(CourseScheduleDto model)
        {
           
            throw new NotImplementedException();
        }

        public async Task<GeneralResponse> CreateCourseScheduleAsync(List<CourseScheduleDto> model)
        {
            // _context.CourseSchedules.AddRange(model);
            //  await _context.SaveChangesAsync();
            //retrun new GeneralResponse(StatusCodes = 200, Message = "Course schedules created successfully", Data = response);
            
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
