using EduReg.Common;
using EduReg.Models.Dto;

namespace EduReg.Services.Interfaces
{
    public interface ISemesters
    {
        Task<GeneralResponse> CreateSemesterAsync(SemestersDto model);
        Task<GeneralResponse> UpdateSemesterAsync(int Id, SemestersDto model);
        Task<GeneralResponse> DeleteSemesterAsync(int Id);
        Task<GeneralResponse> GetSemesterByIdAsync(int Id);
        Task<GeneralResponse> GetAllSemestersAsync();
    }
}
