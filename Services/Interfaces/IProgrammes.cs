using EduReg.Common;
using EduReg.Models.Dto;

namespace EduReg.Services.Interfaces
{
    public interface IProgrammes
    {
        Task<GeneralResponse> CreateProgrammeAsync(ProgrammesDto model);
        Task<GeneralResponse> UpdateProgrammeAsync(long Id, ProgrammesDto model);
        Task<GeneralResponse> DeleteProgrammeAsync(long Id);
        Task<GeneralResponse> GetProgrammeByIdAsync(long Id);
        Task<GeneralResponse> GetProgrammeByNameAsync(string ProgrammeName);
        Task<GeneralResponse> GetAllProgrammesAsync(PagingParameters paging);
    }
}
