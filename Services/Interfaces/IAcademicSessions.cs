using EduReg.Common;
using EduReg.Models.Dto;
using System.Threading.Tasks;

namespace EduReg.Services.Interfaces
{
    public interface IAcademicSessions
    {
        Task<GeneralResponse> CreateAcademicSessionAsync(AcademicSessionsDto model);
        Task<GeneralResponse> UpdateAcademicSessionAsync(long Id, AcademicSessionsDto model);
        Task<GeneralResponse> DeleteAcademicSessionAsync(long Id);
        Task<GeneralResponse> GetAcademicSessionByIdAsync(long Id);
        Task<GeneralResponse> GetAllAcademicSessionsAsync();
    }
}
