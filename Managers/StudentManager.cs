using EduReg.Models.Dto;
using EduReg.Services.Interfaces;

namespace EduReg.Managers
{
    public class StudentManager
    {
        private readonly IStudent _studentRepository;
        public StudentManager(IStudent studentRepository)
        {
            // Initialize any dependencies here if needed
            _studentRepository = studentRepository; // Assuming you have a concrete implementation of IStudent
        }
        public async Task<(StudentResponse item, string message, bool isSuccess)> StudentLogin(StudentLogin student)
        {
            return await _studentRepository.StudentLogin(student);    
        }
    }
}
