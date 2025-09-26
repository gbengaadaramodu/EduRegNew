using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;

namespace EduReg.Services.Repositories
{
    public class FeeRulesRepository : IFeeRules
    {
        private readonly ApplicationDbContext _context;

        public FeeRulesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<GeneralResponse> ApplyFeeRuleToSemesterSchedule(List<FeeRuleDto> model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> CreateFeeRuleAsync(FeeRuleDto model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> DeleteFeeRuleAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetAllFeeRuleAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetFeeRuleByAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> UpdateFeeRuleAsync(int Id, FeeRuleDto model)
        {
            throw new NotImplementedException();
        }
    }
}
