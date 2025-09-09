using EduReg.Common;
using EduReg.Models.Dto;

namespace EduReg.Services.Interfaces
{
    public interface IProgrammes
    {
        Task<GeneralResponse> CreateProgrammeAsync(ProgrammesDto model);
        Task<GeneralResponse> UpdateProgrammeAsync(int Id, ProgrammesDto model);
        Task<GeneralResponse> DeleteProgrammeAsync(int Id);
        Task<GeneralResponse> GetProgrammeByIdAsync(int Id);
        Task<GeneralResponse> GetProgrammeByNameAsync(string ProgrammeName);
        Task<GeneralResponse> GetAllProgrammesAsync();
    }
}
