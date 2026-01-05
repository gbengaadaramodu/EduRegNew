using EduReg.Common;
using EduReg.Models.Dto;
using System.Threading.Tasks;

namespace EduReg.Services.Interfaces
{
    public interface IAcademicSessions
    {
        Task<GeneralResponse> CreateAcademicSessionAsync(AcademicSessionsDto model);
        Task<GeneralResponse> UpdateAcademicSessionAsync(int Id, AcademicSessionsDto model);
        Task<GeneralResponse> DeleteAcademicSessionAsync(int Id);
        Task<GeneralResponse> GetAcademicSessionByIdAsync(int Id);
        Task<GeneralResponse> GetAllAcademicSessionsAsync(PagingParameters paging);
    }
}
