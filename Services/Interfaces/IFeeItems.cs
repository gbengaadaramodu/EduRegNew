using EduReg.Common;
using EduReg.Models.Dto;

namespace EduReg.Services.Interfaces
{
    public interface IFeeItems
    {
        Task<GeneralResponse> CreateFeeItemAsync(FeeItemDto model);
        Task<GeneralResponse> AddFeeItemToSemesterScheduleAsync(List<FeeItemDto> model);
        Task<GeneralResponse> UpdateFeeItemAsync(int Id, FeeItemDto model); 
        Task<GeneralResponse> DeleteFeeItemAsync(int Id);
        Task<GeneralResponse> GetFeeItemByIdAsync(int Id);
        Task<GeneralResponse> GetAllFeeItemsAsync(PagingParameters paging);
    }
}
