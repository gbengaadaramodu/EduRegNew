using EduReg.Common;
using EduReg.Models.Dto;

namespace EduReg.Services.Interfaces
{
    public interface IAdmissionBatches
    {
        Task<GeneralResponse> CreateAdmissionBatchAsync(AdmissionBatchesDto model);
        Task<GeneralResponse> UpdateAdmissionBatchAsync(int Id, AdmissionBatchesDto model);
        Task<GeneralResponse> DeleteAdmissionBatchAsync(int Id);
        Task<GeneralResponse> GetAdmissionBatchByIdAsync(int Id);
        Task<GeneralResponse> GetAllAdmissionBatchAsync(PagingParameters paging);

    }
}
