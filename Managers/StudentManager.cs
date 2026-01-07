using EduReg.Common;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;

namespace EduReg.Managers
{
    public class StudentManager
    {
        private readonly IStudent _studentRepository;
        private readonly ICourseRegistration _courseRegistrationRepository;
        public StudentManager(IStudent studentRepository, ICourseRegistration courseRegistrationRepository)
        {
            // Initialize any dependencies here if needed
            _studentRepository = studentRepository; // Assuming you have a concrete implementation of IStudent
            _courseRegistrationRepository = courseRegistrationRepository;
        }
        public async Task<(StudentResponse item, string message, bool isSuccess)> StudentLogin(StudentLogin student)
        {
            return await _studentRepository.StudentLogin(student);    
        }

        public async Task<GeneralResponse> CreateCourseRegistrationAsync(CreateCourseRegistrationDto model)
        {
            return await _courseRegistrationRepository.CreateCourseRegistrationAsync(model);
        }

        public async Task<GeneralResponse> GetCourseRegistration(CourseRegistrationRequestDto model)
        {
            return await _courseRegistrationRepository.GetCourseRegistration(model);
        }

        public async Task<GeneralResponse> GetCourseRegistrationById(long id)
        {
            return await _courseRegistrationRepository.GetCourseRegistrationById(id);
        }
    }
}
