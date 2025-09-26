using EduReg.Common;
using EduReg.Models.Dto;

namespace EduReg.Services.Interfaces
{
    public interface IFeeItems
    {
        Task<GeneralResponse> CreateFeeItemAsync(FeeItemDto model);
        Task<GeneralResponse> AddFeeItemToSemesterSchedule(List<FeeItemDto> model);
        Task<GeneralResponse> UpdateFeeItemAsync(int Id, FeeItemDto model); 
        Task<GeneralResponse> DeleteFeeItemAsync(int id);
        Task<GeneralResponse> GetFeeItemByAsync(int id);
        Task<GeneralResponse> GetAllFeeItemsAsync();
    }
}
