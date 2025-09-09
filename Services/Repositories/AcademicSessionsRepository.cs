using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;

namespace EduReg.Services.Repositories
{
    public class AcademicSessionsRepository : IAcademicSessions
    {
        private readonly ApplicationDbContext _context;
        public AcademicSessionsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<GeneralResponse> CreateAcademicSessionAsync(AcademicSessionsDto model)
        {
            

            return Task.FromResult(new GeneralResponse { StatusCore = 200, Message = "Academic session created successfully" , Data = model});
            
           // return new GeneralResponse { StatusCore = 404, Message = "Not Found"};

        }

        public Task<GeneralResponse> DeleteAcademicSessionAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetAcademicSessionByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetAllAcademicSessionsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> UpdateAcademicSessionAsync(int Id, AcademicSessionsDto model)
        {
            throw new NotImplementedException();
        }
    }
}
