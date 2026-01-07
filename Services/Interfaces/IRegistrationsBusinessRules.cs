using EduReg.Common;
using EduReg.Models.Dto;

namespace EduReg.Services.Interfaces
{
    public interface IRegistrationsBusinessRules
    {
        Task<GeneralResponse> ValidateStudentRegistrationAsync(RegistrationBusinessRulesDto model);
      
        Task<GeneralResponse> CreateRegistrationBusinessRuleAsync(RegistrationBusinessRulesDto model);
        Task<GeneralResponse> CreateRegistrationBusinessRuleAsync(List<RegistrationBusinessRulesDto> model);

        Task<GeneralResponse> UploadRegistrationBusinessRuleAsync(byte[] model); // EXCEL, CSV, Flat file upload - Admin

        Task<GeneralResponse> GetRegistrationBusinessRulesByDepartmentAsync(string DepartmentCode, RegistrationBusinessRulesDto model);
        Task<GeneralResponse> GetAllRegistrationBusinessRulesAsync(PagingParameters paging);
   
        Task<GeneralResponse> UpdateRegistrationBusinessRuleAsync(long Id, RegistrationBusinessRulesDto model);
        Task<GeneralResponse> DeleteRegistrationBusinessRuleAsync(long Id);

    }
}
