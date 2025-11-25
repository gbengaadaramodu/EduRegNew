using EduReg.Common;
using EduReg.Models.Dto;

namespace EduReg.Services.Interfaces
{
    public interface IFeeRules
    {
        Task<GeneralResponse> CreateFeeRuleAsync(FeeRuleDto model, string institutionShortName);
        Task<GeneralResponse> ApplyFeeRuleToSemesterScheduleAsync(List<FeeRuleDto> models, string institutionShortName);
        Task<GeneralResponse> UpdateFeeRuleAsync(int id, FeeRuleDto model, string institutionShortName);
        Task<GeneralResponse> DeleteFeeRuleAsync(int id, string institutionShortName);
        Task<GeneralResponse> GetFeeRuleByIdAsync(int id, string institutionShortName);
        Task<GeneralResponse> GetAllFeeRuleAsync(string institutionShortName);
    }
}


 