using EduReg.Common;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request;
using EduReg.Services.Interfaces;
using System.Threading.Tasks;

namespace EduReg.Managers
{
    public class InstitutionsManager : IInstitutions
    {
        private readonly IInstitutions _institution;

        public InstitutionsManager(IInstitutions institution)
        {
            _institution = institution;
        }

        public Task<GeneralResponse> CreateInstitutionAsync(InstitutionsDto model)
        {
            return _institution.CreateInstitutionAsync(model);
        }

        public Task<GeneralResponse> DeleteInstitutionAsync(int Id)
        {
            return _institution.DeleteInstitutionAsync(Id);
        }

        public Task<GeneralResponse> GetAllInstitutionAsync(PagingParameters paging, InstitutionFilter filter)
        {
            return _institution.GetAllInstitutionAsync(paging, filter);
        }

        public Task<GeneralResponse> GetInstitutionByIdAsync(int Id)
        {
            return _institution.GetInstitutionByIdAsync(Id);
        }

        public Task<GeneralResponse> GetInstitutionByShortNameAsync(string InstitutionShortName)
        {
            return _institution.GetInstitutionByShortNameAsync(InstitutionShortName);
        }

        public Task<GeneralResponse> UpdateInstitutionAsync(int Id, UpdateInstitutionsDto model)
        {
            return _institution.UpdateInstitutionAsync(Id, model);
        }

        public Task<GeneralResponse> UpdateInstitutionByShortNameAsync(string shortName, UpdateInstitutionsDto model)
        {
            return _institution.UpdateInstitutionByShortNameAsync(shortName, model);
        }
    }
}
