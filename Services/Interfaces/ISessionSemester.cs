using EduReg.Common;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request;
using EduReg.Models.Entities;

namespace EduReg.Services.Interfaces
{
    public interface ISessionSemester
    {
        Task<GeneralResponse> CreateSessionSemesterAsync(string institutionShortName, SessionSemesterDto dto);
        Task<GeneralResponse> GetSessionSemesterByIdAsync(long id);
        Task<GeneralResponse> GetAllSessionSemesterAsync(string institutionShortName, SessionSemesterFilter filter, PagingParameters paging);
        Task<GeneralResponse> UpdateSessionSemesterAsync(long id, UpdateSessionSemesterDto dto);
        Task<GeneralResponse> DeleteSessionSemesterAsync(long id);
    }

}
