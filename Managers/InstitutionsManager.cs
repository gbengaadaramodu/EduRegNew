using EduReg.Common;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;

namespace EduReg.Managers
{
    public class InstitutionsManager: IInstitutions
    {
        private readonly IInstitutions _manager;
        public InstitutionsManager(IInstitutions manager) 
        {
            _manager = manager;
        }
        public Task<GeneralResponse> CreateInstitutionAsync(InstitutionsDto model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> DeleteInstitutionAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetAllInstitutionAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetInstitutionByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetInstitutionByShortNameAsync(string InstitutionShortName)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> UpdateInstitutionAsync(int Id, InstitutionsDto model)
        {
            throw new NotImplementedException();
        }
    }
}
