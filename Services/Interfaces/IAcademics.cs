using EduReg.Common;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request;

namespace EduReg.Services.Interfaces
{
    public interface IAcademics
    {
        Task<GeneralResponse> CreateAcademicLevel(AcademicLevelsDto model);
        Task<GeneralResponse> UpdateAcademicLevelAsync(long Id, AcademicLevelsDto model);
        Task<GeneralResponse> DeleteAcademicLevelAsync(long Id);
        Task<GeneralResponse> GetAcademicLevelByIdAsync(long Id);
        Task<GeneralResponse> GetAllAcademicLevelAsync(PagingParameters paging, AcademicLevelFilter filter);
    }
}
