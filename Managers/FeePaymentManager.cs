using EduReg.Common;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;

namespace EduReg.Managers
{
    public class FeePaymentManager : IFeeItems, IFeeRules
    {
        public Task<GeneralResponse> AddFeeItemToSemesterSchedule(List<FeeItemDto> model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> ApplyFeeRuleToSemesterSchedule(List<FeeRuleDto> model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> CreateFeeItemAsync(FeeItemDto model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> CreateFeeRuleAsync(FeeRuleDto model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> DeleteFeeItemAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> DeleteFeeRuleAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetAllFeeItemsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetAllFeeRuleAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetFeeItemByAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetFeeRuleByAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> UpdateFeeItemAsync(int Id, FeeItemDto model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> UpdateFeeRuleAsync(int Id, FeeRuleDto model)
        {
            throw new NotImplementedException();
        }
    }
}
