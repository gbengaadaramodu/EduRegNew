using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;

namespace EduReg.Services.Repositories
{
    public class AdmissionBatchesRepository : IAdmissionBatches
    {
        private readonly ApplicationDbContext _context;
        public AdmissionBatchesRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<GeneralResponse> CreateAdmissionBatchAsync(AdmissionBatchesDto model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> DeleteAdmissionBatchAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetAdmissionBatchByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetAllAdmissionBatchAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> UpdateAdmissionBatchAsync(int Id, AdmissionBatchesDto model)
        {
            throw new NotImplementedException();
        }
    }
}
