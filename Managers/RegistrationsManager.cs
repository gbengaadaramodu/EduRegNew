using EduReg.Common;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;

namespace EduReg.Managers
{
    public class RegistrationsManager: IRegistrations, IRegistrationsBusinessRules
    {
        public readonly IRegistrations _registrations;
        public readonly IRegistrationsBusinessRules _registrationRules;
        public RegistrationsManager(IRegistrations registrations, IRegistrationsBusinessRules registrationRules)
        {
           _registrations = registrations;
              _registrationRules = registrationRules;
        }

        public Task<GeneralResponse> CreateRegistrationBusinessRuleAsync(RegistrationBusinessRulesDto model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> CreateRegistrationBusinessRuleAsync(List<RegistrationBusinessRulesDto> model)
        {
            throw new NotImplementedException();
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

        public Task<GeneralResponse> DeleteRegistrationBusinessRuleAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> DropStudentRegistrationAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetAllRegistrationBusinessRulesAsync()
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

        public Task<GeneralResponse> GetRegistrationBusinessRulesByDepartmentAsync(string DepartmentCode, BusinessRulesDto model)
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

        public Task<GeneralResponse> UpdateRegistrationBusinessRuleAsync(int Id, RegistrationBusinessRulesDto model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> UploadRegistrationBusinessRuleAsync(byte[] model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> ValidateStudentRegistrationAsync(RegistrationBusinessRulesDto model)
        {
            throw new NotImplementedException();
        }
    }
}
