using EduReg.Common;
using EduReg.Models.Dto;

namespace EduReg.Services.Interfaces
{
    public interface IFeeRules
    {

        Task<GeneralResponse> CreateFeeRuleAsync(FeeRuleDto model);
        Task<GeneralResponse> ApplyFeeRuleToSemesterSchedule(List<FeeRuleDto> model);
        Task<GeneralResponse> UpdateFeeRuleAsync(int Id, FeeRuleDto model);
        Task<GeneralResponse> DeleteFeeRuleAsync(int id);
        Task<GeneralResponse> GetFeeRuleByAsync(int id);
        Task<GeneralResponse> GetAllFeeRuleAsync();

    }
}
