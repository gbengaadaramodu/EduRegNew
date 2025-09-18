using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;

namespace EduReg.Services.Repositories
{
    public class RegistrationsRepository : IRegistrations
    {

        public readonly ApplicationDbContext _context;

        public RegistrationsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<GeneralResponse> CreateStudentRegistrationAsync(RegistrationsDto model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> CreateStudentRegistrationAsync(List<RegistrationsDto> model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> CreateStudentRegistrationAsync(byte[] model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> DropStudentRegistrationAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetAllStudentRegistrationsAync(string matricNumber)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetDepartmentRegistrationsBySemesterIdAsync(string sessionId)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetDepartmentRegistrationsBySessionIdAsync(string sessionId)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetStudentRegistrationsBySemesterIdAync(RegistrationsDto model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetStudentRegistrationsBySessionIdAync(RegistrationsDto model)
        {
            throw new NotImplementedException();
        }
    }
}
