using EduReg.Common;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request;
using System.Threading.Tasks;

namespace EduReg.Services.Interfaces
{
    public interface IAcademicSessions
    {
        Task<GeneralResponse> CreateAcademicSessionAsync(CreateAcademicSessionDto model);
        Task<GeneralResponse> UpdateAcademicSessionAsync(long Id, UpdateAcademicSessionDto model);
        Task<GeneralResponse> DeleteAcademicSessionAsync(long Id);
        Task<GeneralResponse> GetAcademicSessionByIdAsync(long Id);
        Task<GeneralResponse> GetAllAcademicSessionsAsync(PagingParameters paging,AcademicSessionFilter filter);

    }
}
