using EduReg.Common;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request;

namespace EduReg.Services.Interfaces
{
    public interface IFeeTypes
    {
        Task<GeneralResponse> CreateFeeTypeAsync(FeeTypeDto model);
        Task<GeneralResponse> UpdateFeeTypeAsync(long id, FeeTypeDto model);
        Task<GeneralResponse> DeleteFeeTypeAsync(long id);
        Task<GeneralResponse> GetFeeTypeByIdAsync(long id);
        Task<GeneralResponse> GetAllFeeTypesAsync(PagingParameters paging, string? institutionShortName = null);
    }
}