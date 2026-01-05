using EduReg.Common;
using EduReg.Models.Dto;

namespace EduReg.Services.Interfaces
{
    public interface IInstitutions
    {
       
        Task<GeneralResponse> CreateInstitutionAsync(InstitutionsDto model);
        Task<GeneralResponse> UpdateInstitutionAsync(int Id, InstitutionsDto model);
        Task<GeneralResponse> DeleteInstitutionAsync(int Id);
        Task<GeneralResponse> GetInstitutionByIdAsync(int Id);
        Task<GeneralResponse> GetInstitutionByShortNameAsync(string  InstitutionShortName);
        Task<GeneralResponse> GetAllInstitutionAsync(PagingParameters paging);
    }
}
