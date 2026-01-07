using EduReg.Common;
using EduReg.Models.Dto;

namespace EduReg.Services.Interfaces
{
    public interface IFeeItems
    {
        Task<GeneralResponse> CreateFeeItemAsync(FeeItemDto model);
        Task<GeneralResponse> AddFeeItemToSemesterScheduleAsync(List<FeeItemDto> model);
        Task<GeneralResponse> UpdateFeeItemAsync(long Id, FeeItemDto model); 
        Task<GeneralResponse> DeleteFeeItemAsync(long Id);
        Task<GeneralResponse> GetFeeItemByIdAsync(long Id);
        Task<GeneralResponse> GetAllFeeItemsAsync(PagingParameters paging);

    }
}
