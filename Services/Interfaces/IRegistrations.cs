using EduReg.Common;
using EduReg.Models.Dto;

namespace EduReg.Services.Interfaces
{
    public interface IRegistrations
    {
        Task<GeneralResponse> GetDepartmentRegistrationsBySessionIdAsync(string sessionId); // All Students
        Task<GeneralResponse> GetDepartmentRegistrationsBySemesterIdAsync(string sessionId); // All Students
        Task<GeneralResponse> GetAllStudentRegistrationsAync(string matricNumber); //Individual Student
        Task<GeneralResponse> GetStudentRegistrationsBySessionIdAync(RegistrationsDto model); //Individual Student
        Task<GeneralResponse> GetStudentRegistrationsBySemesterIdAync(RegistrationsDto model); //Individual Student

        Task<GeneralResponse> CreateStudentRegistrationAsync(RegistrationsDto model); //Get list of ProgramCoursesDto
        Task<GeneralResponse> CreateStudentRegistrationAsync(List<RegistrationsDto> model); //Get list of ProgramCoursesDto
        Task<GeneralResponse> CreateStudentRegistrationAsync(byte[] model); // EXCEL, CSV, Flat file upload - Admin

        Task<GeneralResponse> DropStudentRegistrationAsync(long Id); // Add and Drop Courses
        

    }

     

}
