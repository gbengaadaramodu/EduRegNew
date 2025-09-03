using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;

namespace EduReg.Services.Repositories
{
    public class SemestersRepository : ISemesters
    {
        private readonly ApplicationDbContext _context;
        public SemestersRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<GeneralResponse> CreateSemesterAsync(SemestersDto model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> DeleteSemesterAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetAllSemestersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetSemesterByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> UpdateSemesterAsync(int Id, SemestersDto model)
        {
            throw new NotImplementedException();
        }
    }
}
