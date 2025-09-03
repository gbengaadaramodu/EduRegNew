using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;

namespace EduReg.Services.Repositories
{
    public class InstitutionsRepository : IInstitutions
    {
        private readonly ApplicationDbContext _context;
        public InstitutionsRepository(ApplicationDbContext context)
        {
            _context = context;
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
