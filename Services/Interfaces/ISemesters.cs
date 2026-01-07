using EduReg.Common;
using EduReg.Models.Dto;

namespace EduReg.Services.Interfaces
{
    public interface ISemesters
    {
        Task<GeneralResponse> CreateSemesterAsync(SemestersDto model);
        Task<GeneralResponse> UpdateSemesterAsync(long Id, SemestersDto model);
        Task<GeneralResponse> DeleteSemesterAsync(long Id);
        Task<GeneralResponse> GetSemesterByIdAsync(long Id);
        Task<GeneralResponse> GetAllSemestersAsync(PagingParameters paging);
    }
}
