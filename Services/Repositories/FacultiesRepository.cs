using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;

namespace EduReg.Services.Repositories
{
    public class FacultiesRepository : IFaculties
    {
        private readonly ApplicationDbContext _context;
        public FacultiesRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<GeneralResponse> CreateFacultyAsync(FacultiesDto model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> DeleteFacultyAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetAllFacultiesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetFacultyByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> UpdateFacultyAsync(int Id, FacultiesDto model)
        {
            throw new NotImplementedException();
        }
    }
}
