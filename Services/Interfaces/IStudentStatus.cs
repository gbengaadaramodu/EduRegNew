using EduReg.Common;
using EduReg.Models.Dto;

namespace EduReg.Services.Interfaces
{
    public interface IStudentStatus
    {
        Task<GeneralResponse> CreateStudentStatusAsync(StudentStatusDto model);
        Task<GeneralResponse> GetStudentStatusByIdAsync(long id);
        Task<GeneralResponse> GetAllStudentStatusAsync(PagingParameters paging, string? institutionShortName = null);
        Task<GeneralResponse> UpdateStudentStatusAsync(long id, StudentStatusDto model);
        Task<GeneralResponse> DeleteStudentStatusAsync(long id);
    }
}