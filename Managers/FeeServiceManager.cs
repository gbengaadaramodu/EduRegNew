using EduReg.Common;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;

namespace EduReg.Managers
{
    public class FeeServiceManager: IFeeItems, IFeeRules
    {
        private readonly IFeeItems _feeItems;
        private readonly IFeeRules _feeRules;
        public FeeServiceManager(IFeeItems feeItems, IFeeRules feeRules)
        {
             _feeItems = feeItems;
            _feeRules = feeRules;
        }

        public async Task<GeneralResponse> AddFeeItemToSemesterScheduleAsync(List<FeeItemDto> model)
        {
            var result = await _feeItems.AddFeeItemToSemesterScheduleAsync(model);
            return result;
        }

        public Task<GeneralResponse> ApplyFeeRuleToSemesterScheduleAsync(List<FeeRuleDto> models, string institutionShortName)
        {
            var result =  _feeRules.ApplyFeeRuleToSemesterScheduleAsync(models, institutionShortName);
            return result;
        }

        public async Task<GeneralResponse> CreateFeeItemAsync(FeeItemDto model)
        {
            var result = await _feeItems.CreateFeeItemAsync(model);
            return result;
        }

        public async Task<GeneralResponse> CreateFeeRuleAsync(FeeRuleDto model, string institutionShortName)
        {
            var result = await  _feeRules.CreateFeeRuleAsync(model, institutionShortName);
            return result;
        }

        public async Task<GeneralResponse> DeleteFeeItemAsync(long id)
        {
            var result = await _feeItems.DeleteFeeItemAsync(id);
            return result;
        }

        public async Task<GeneralResponse> DeleteFeeRuleAsync(long id, string institutionShortName)
        {
            var result = await  _feeRules.DeleteFeeRuleAsync(id, institutionShortName);
            return result;
        }

        public async Task<GeneralResponse> GetAllFeeItemsAsync(PagingParameters paging)
        {
            var result = await _feeItems.GetAllFeeItemsAsync(paging);

           return result;
        }

        public async Task<GeneralResponse> GetAllFeeRuleAsync(string institutionShortName, PagingParameters paging)
        {
            var result = await _feeRules.GetAllFeeRuleAsync(institutionShortName, paging);
            return result;
        }

        public async Task<GeneralResponse> GetFeeItemByIdAsync(long id)
        {
            var result = await _feeItems.GetFeeItemByIdAsync(id);

            return result;
        }

        public async Task<GeneralResponse> GetFeeRuleByIdAsync(long id, string institutionShortName)
        {
            var result = await  _feeRules.GetFeeRuleByIdAsync(id, institutionShortName);
            return result;
        }

        public async Task<GeneralResponse> UpdateFeeItemAsync(long Id, FeeItemDto model)
        {
            var result = await _feeItems.UpdateFeeItemAsync(Id,model);

            return result;
        }

        public async Task<GeneralResponse> UpdateFeeRuleAsync(long id, FeeRuleDto model, string institutionShortName)
        {
           var result = await  _feeRules.UpdateFeeRuleAsync(id, model, institutionShortName);
            return result;
        }
    }
}
