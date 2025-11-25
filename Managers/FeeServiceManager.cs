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

        public async Task<GeneralResponse> DeleteFeeItemAsync(int id)
        {
            var result = await _feeItems.DeleteFeeItemAsync(id);
            return result;
        }

        public async Task<GeneralResponse> DeleteFeeRuleAsync(int id, string institutionShortName)
        {
            var result = await  _feeRules.DeleteFeeRuleAsync(id, institutionShortName);
            return result;
        }

        public async Task<GeneralResponse> GetAllFeeItemsAsync()
        {
            var result = await _feeItems.GetAllFeeItemsAsync();

           return result;
        }

        public async Task<GeneralResponse> GetAllFeeRuleAsync(string institutionShortName)
        {
            var result = await _feeRules.GetAllFeeRuleAsync(institutionShortName);
            return result;
        }

        public async Task<GeneralResponse> GetFeeItemByIdAsync(int id)
        {
            var result = await _feeItems.GetFeeItemByIdAsync(id);

            return result;
        }

        public async Task<GeneralResponse> GetFeeRuleByIdAsync(int id, string institutionShortName)
        {
            var result = await  _feeRules.GetFeeRuleByIdAsync(id, institutionShortName);
            return result;
        }

        public async Task<GeneralResponse> UpdateFeeItemAsync(int Id, FeeItemDto model)
        {
            var result = await _feeItems.UpdateFeeItemAsync(Id,model);

            return result;
        }

        public async Task<GeneralResponse> UpdateFeeRuleAsync(int id, FeeRuleDto model, string institutionShortName)
        {
           var result = await  _feeRules.UpdateFeeRuleAsync(id, model, institutionShortName);
            return result;
        }
    }
}
