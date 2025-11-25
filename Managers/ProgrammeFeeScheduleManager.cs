using EduReg.Common;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;

namespace EduReg.Managers
{
    public class ProgrammeFeeScheduleManager : IProgrammeFeeSchedule
    {
        private readonly IProgrammeFeeSchedule _schedule;
        public ProgrammeFeeScheduleManager(IProgrammeFeeSchedule schedule)
        {
            _schedule = schedule;
        }
        public async Task<GeneralResponse> CreateProgrammeFeeScheduleAsync(ProgrammeFeeScheduleDto model)
        {
            var result = await _schedule.CreateProgrammeFeeScheduleAsync(model);
            return result;
        }

        public async Task<GeneralResponse> DeleteProgrammeFeeScheduleAsync(int id, string institutionShortName)
        {
            var result = await _schedule.DeleteProgrammeFeeScheduleAsync(id, institutionShortName);
            return result;
        }

        public async Task<GeneralResponse> GenerateProgrammeFeeSchedulesAsync(string institutionShortName, AcademicContextDto model)
        {
            var result = await _schedule.GenerateProgrammeFeeSchedulesAsync(institutionShortName, model);
            return result;
        }

        public async Task<GeneralResponse> GetAllProgrammeFeeSchedulesAsync(string institutionShortName)
        {
            var result = await _schedule.GetAllProgrammeFeeSchedulesAsync(institutionShortName);
            return result;
        }

        public async Task<GeneralResponse> GetProgrammeFeeScheduleByIdAsync(int id, string institutionShortName)
        {
            var result = await _schedule.GetProgrammeFeeScheduleByIdAsync(id, institutionShortName);
            return result;
        }

        public async Task<GeneralResponse> GetProgrammeFeeSchedulesByFeeItemAsync(string institutionShortName, int feeItemId)
        {
            var result = await _schedule.GetProgrammeFeeSchedulesByFeeItemAsync(institutionShortName, feeItemId);
            return result;
        }

        public async Task<GeneralResponse> GetProgrammeFeeSchedulesByProgrammeAsync(string institutionShortName, string programmeCode)
        {
            var result = await _schedule.GetProgrammeFeeSchedulesByProgrammeAsync(institutionShortName, programmeCode);
            return result;
        }

        public async Task<GeneralResponse> UpdateProgrammeFeeScheduleAsync(int id, ProgrammeFeeScheduleDto model)
        {
            var result = await _schedule.UpdateProgrammeFeeScheduleAsync(id, model);
            return result;
        }
    }
}
