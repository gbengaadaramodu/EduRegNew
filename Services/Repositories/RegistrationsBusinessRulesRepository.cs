using EduReg.Common;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;

namespace EduReg.Services.Repositories
{
    public class RegistrationsBusinessRulesRepository : IRegistrationsBusinessRules
    {
        public Task<GeneralResponse> CreateRegistrationBusinessRuleAsync(RegistrationBusinessRulesDto model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> CreateRegistrationBusinessRuleAsync(List<RegistrationBusinessRulesDto> model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> DeleteRegistrationBusinessRuleAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetAllRegistrationBusinessRulesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetRegistrationBusinessRulesByDepartmentAsync(string DepartmentCode, BusinessRulesDto model)
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
