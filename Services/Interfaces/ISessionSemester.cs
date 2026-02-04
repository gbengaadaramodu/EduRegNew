using EduReg.Common;
using EduReg.Models.Dto;
using EduReg.Models.Entities;

namespace EduReg.Services.Interfaces
{
    public interface ISessionSemester
    {
        Task<GeneralResponse> CreateSessionSemesterAsync(SessionSemesterDto dto);
        Task<GeneralResponse> GetSessionSemesterByIdAsync(long id);
        Task<GeneralResponse> GetAllSessionSemesterAsync(string institutionShortName);
        Task<GeneralResponse> UpdateSessionSemesterAsync(long id, UpdateSessionSemesterDto dto);
        Task<GeneralResponse> DeleteSessionSemesterAsync(long id);
    }

}
