using EduReg.Common;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EduReg.Managers
{
    public class RegistrationsManager : IRegistrations, IRegistrationsBusinessRules
    {
        private readonly IRegistrations _registrations;
        private readonly IRegistrationsBusinessRules _registrationRules;

        public RegistrationsManager(IRegistrations registrations, IRegistrationsBusinessRules registrationRules)
        {
            _registrations = registrations;
            _registrationRules = registrationRules;
        }

        // Registration Business Rules Implementation

        public Task<GeneralResponse> CreateRegistrationBusinessRuleAsync(RegistrationBusinessRulesDto model)
        {
            return _registrationRules.CreateRegistrationBusinessRuleAsync(model);
        }

        public Task<GeneralResponse> CreateRegistrationBusinessRuleAsync(List<RegistrationBusinessRulesDto> model)
        {
            return _registrationRules.CreateRegistrationBusinessRuleAsync(model);
        }

        public Task<GeneralResponse> DeleteRegistrationBusinessRuleAsync(long Id)
        {
            return _registrationRules.DeleteRegistrationBusinessRuleAsync(Id);
        }

        public Task<GeneralResponse> GetAllRegistrationBusinessRulesAsync(PagingParameters paging)
        {
            return _registrationRules.GetAllRegistrationBusinessRulesAsync(paging);
        }

        public Task<GeneralResponse> GetRegistrationBusinessRulesByDepartmentAsync(string DepartmentCode, RegistrationBusinessRulesDto model)
        {
            return _registrationRules.GetRegistrationBusinessRulesByDepartmentAsync(DepartmentCode, model);
        }

        public Task<GeneralResponse> UpdateRegistrationBusinessRuleAsync(long Id, RegistrationBusinessRulesDto model)
        {
            return _registrationRules.UpdateRegistrationBusinessRuleAsync(Id, model);
        }

        public Task<GeneralResponse> UploadRegistrationBusinessRuleAsync(byte[] model)
        {
            return _registrationRules.UploadRegistrationBusinessRuleAsync(model);
        }

        public Task<GeneralResponse> ValidateStudentRegistrationAsync(RegistrationBusinessRulesDto model)
        {
            return _registrationRules.ValidateStudentRegistrationAsync(model);
        }

        // Student Registration Implementation

        public Task<GeneralResponse> CreateStudentRegistrationAsync(RegistrationsDto model)
        {
            return _registrations.CreateStudentRegistrationAsync(model);
        }

        public Task<GeneralResponse> CreateStudentRegistrationAsync(List<RegistrationsDto> model)
        {
            return _registrations.CreateStudentRegistrationAsync(model);
        }

        public Task<GeneralResponse> CreateStudentRegistrationAsync(byte[] model)
        {
            return _registrations.CreateStudentRegistrationAsync(model);
        }

        public Task<GeneralResponse> DropStudentRegistrationAsync(long Id)
        {
            return _registrations.DropStudentRegistrationAsync(Id);
        }

        public Task<GeneralResponse> GetAllStudentRegistrationsAync(string matricNumber)
        {
            return _registrations.GetAllStudentRegistrationsAync(matricNumber);
        }

        public Task<GeneralResponse> GetDepartmentRegistrationsBySemesterIdAsync(string sessionId, PagingParameters paging)
        {
            return _registrations.GetDepartmentRegistrationsBySemesterIdAsync(sessionId, paging);
        }

        public Task<GeneralResponse> GetDepartmentRegistrationsBySessionIdAsync(string sessionId, PagingParameters paging)
        {
            return _registrations.GetDepartmentRegistrationsBySessionIdAsync(sessionId, paging);
        }

        public Task<GeneralResponse> GetStudentRegistrationsBySemesterIdAync(RegistrationsDto model)
        {
            return _registrations.GetStudentRegistrationsBySemesterIdAync(model);
        }

        public Task<GeneralResponse> GetStudentRegistrationsBySessionIdAync(RegistrationsDto model)
        {
            return _registrations.GetStudentRegistrationsBySessionIdAync(model);
        }
    }
}
