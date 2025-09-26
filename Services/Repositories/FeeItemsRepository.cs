using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;

namespace EduReg.Services.Repositories
{
    public class FeeItemsRepository : IFeeItems
    {
        private readonly ApplicationDbContext _context;
        public FeeItemsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<GeneralResponse> AddFeeItemToSemesterSchedule(List<FeeItemDto> model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> CreateFeeItemAsync(FeeItemDto model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> DeleteFeeItemAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetAllFeeItemsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetFeeItemByAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> UpdateFeeItemAsync(int Id, FeeItemDto model)
        {
            throw new NotImplementedException();
        }
    }
}
