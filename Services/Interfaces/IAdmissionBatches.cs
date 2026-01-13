using EduReg.Common;
using EduReg.Models.Dto;

namespace EduReg.Services.Interfaces
{
    public interface IAdmissionBatches
    {
        Task<GeneralResponse> CreateAdmissionBatchAsync(AdmissionBatchesDto model);
        Task<GeneralResponse> UpdateAdmissionBatchAsync(long Id, UpdateAdmissionBatchesDto model);
        Task<GeneralResponse> UpdateAdmissionBatchByShortNameAsync(string shortName, UpdateAdmissionBatchesDto model);
        Task<GeneralResponse> DeleteAdmissionBatchAsync(long Id);
        Task<GeneralResponse> GetAdmissionBatchByIdAsync(long Id);
        Task<GeneralResponse> GetAllAdmissionBatchAsync(PagingParameters paging);


    }
}
