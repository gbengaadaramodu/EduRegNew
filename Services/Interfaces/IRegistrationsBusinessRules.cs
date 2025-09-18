﻿using EduReg.Common;
using EduReg.Models.Dto;

namespace EduReg.Services.Interfaces
{
    public interface IRegistrationsBusinessRules
    {
        Task<GeneralResponse> ValidateStudentRegistrationAsync(RegistrationBusinessRulesDto model);

        Task<GeneralResponse> CreateRegistrationBusinessRuleAsync(RegistrationBusinessRulesDto model);
        Task<GeneralResponse> CreateRegistrationBusinessRuleAsync(List<RegistrationBusinessRulesDto> model);

        Task<GeneralResponse> UploadRegistrationBusinessRuleAsync(byte[] model); // EXCEL, CSV, Flat file upload - Admin

        Task<GeneralResponse> GetRegistrationBusinessRulesByDepartmentAsync(string DepartmentCode, BusinessRulesDto model);
        Task<GeneralResponse> GetAllRegistrationBusinessRulesAsync();
   
        Task<GeneralResponse> UpdateRegistrationBusinessRuleAsync(int Id, RegistrationBusinessRulesDto model);
        Task<GeneralResponse> DeleteRegistrationBusinessRuleAsync(int Id);

    }
}
