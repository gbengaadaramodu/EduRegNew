using EduReg.Common;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;

namespace EduReg.Managers
{
    public class ProgrammesManager
    {
        private readonly IProgrammes _programmesRepository;

        public ProgrammesManager(IProgrammes programmesRepository)
        {
            _programmesRepository = programmesRepository;
        }

        public async Task<GeneralResponse> CreateProgrammeAsync(ProgrammesDto model)
        {
            return await _programmesRepository.CreateProgrammeAsync(model);
        }

        public async Task<GeneralResponse> DeleteProgrammeAsync(int Id)
        {
            return await _programmesRepository.DeleteProgrammeAsync(Id);
        }

        public async Task<GeneralResponse> GetAllProgrammesAsync()
        {
            return await _programmesRepository.GetAllProgrammesAsync();
        }

        public async Task<GeneralResponse> GetProgrammeByIdAsync(int Id)
        {
            return await _programmesRepository.GetProgrammeByIdAsync(Id);
        }

        public async Task<GeneralResponse> GetProgrammeByNameAsync(string ProgrammeName)
        {
            return await _programmesRepository.GetProgrammeByNameAsync(ProgrammeName);
        }

        public async Task<GeneralResponse> UpdateProgrammeAsync(int id, ProgrammesDto model)
        {
            return await _programmesRepository.UpdateProgrammeAsync(id, model);
        }
    }
}
