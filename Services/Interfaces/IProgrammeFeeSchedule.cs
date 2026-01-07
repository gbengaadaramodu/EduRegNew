 

using EduReg.Common;
using EduReg.Models.Dto;

namespace EduReg.Services.Interfaces
{
    public interface IProgrammeFeeSchedule
    {
        Task<GeneralResponse> GenerateProgrammeFeeSchedulesAsync(string institutionShortName, AcademicContextDto model);        
        Task<GeneralResponse> CreateProgrammeFeeScheduleAsync(ProgrammeFeeScheduleDto model);
        Task<GeneralResponse> UpdateProgrammeFeeScheduleAsync(long id, ProgrammeFeeScheduleDto model);
        Task<GeneralResponse> DeleteProgrammeFeeScheduleAsync(long id, string institutionShortName);
        Task<GeneralResponse> GetProgrammeFeeScheduleByIdAsync(long id, string institutionShortName);
        Task<GeneralResponse> GetAllProgrammeFeeSchedulesAsync(string institutionShortName);
        Task<GeneralResponse> GetProgrammeFeeSchedulesByProgrammeAsync(string institutionShortName, string programmeCode);
        Task<GeneralResponse> GetProgrammeFeeSchedulesByFeeItemAsync(string institutionShortName, int feeItemId);

    }
}
