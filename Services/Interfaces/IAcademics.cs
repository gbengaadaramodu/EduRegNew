using EduReg.Common;
using EduReg.Models.Dto;

namespace EduReg.Services.Interfaces
{
    public interface IAcademics
    {
        Task<GeneralResponse> CreateAcademicLevel(AcademicLevelsDto model);
        Task<GeneralResponse> UpdateAcademicLevelAsync(int Id, AcademicLevelsDto model);
        Task<GeneralResponse> DeleteAcademicLevelAsync(int Id);
        Task<GeneralResponse> GetAcademicLevelByIdAsync(int Id);
        Task<GeneralResponse> GetAllAcademicLevelAsync();
    }
}
