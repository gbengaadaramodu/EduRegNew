using EduReg.Common;
using EduReg.Interfaces;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request; // Added for the robust filter
using EduReg.Services.Interfaces;

namespace EduReg.Managers
{
   
    public class StudentManager : IStudent, ICourseRegistration, IStudentRecords
    {
        private readonly IStudent _studentRepository;
        private readonly ICourseRegistration _courseRegistrationRepository;
        private readonly IStudentRecords _studentRecordsRepository;

        public StudentManager(IStudent studentRepository, ICourseRegistration courseRegistrationRepository, IStudentRecords studentRecordsRepository)
        {
            _studentRepository = studentRepository;
            _courseRegistrationRepository = courseRegistrationRepository;
            _studentRecordsRepository = studentRecordsRepository;
        }

    

        public async Task<(StudentResponse item, string message, bool isSuccess)> StudentLogin(StudentLogin student)
        {
            return await _studentRepository.StudentLogin(student);
        }

     
        public async Task<GeneralResponse> GetAllStudentRecords(PagingParameters paging, StudentRecordsFilter filter)
        {
            return await _studentRecordsRepository.GetAllStudentRecords(paging, filter);
        }

        public async Task<GeneralResponse> GetStudentRecordsById(string id)
        {
            return await _studentRecordsRepository.GetStudentRecordsById(id);
        }

        public async Task<GeneralResponse> UpdateStudentRecords(string id, UpdateStudentRecordsDto model)
        {
            return await _studentRecordsRepository.UpdateStudentRecords(id, model);
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

        public string GenerateMatricNumber(string programmeCode, int sessionId)
        {
            return _studentRecordsRepository.GenerateMatricNumber(programmeCode, sessionId);
        }
    }
}