using EduReg.Services.Interfaces;
using EduReg.Common;
using EduReg.Models.Dto;

namespace EduReg.Managers
{
    public class SemesterManager
    {
        private readonly ISemesters _semesterRepository;

        public SemesterManager(ISemesters semesterRepository)
        {
            _semesterRepository = semesterRepository;
        }

        public async Task<GeneralResponse> GetAllSemesters()
        {
            return await _semesterRepository.GetAllSemestersAsync();
        }

        public async Task<GeneralResponse> GetSemesterById(int Id)
        {
            return await _semesterRepository.GetSemesterByIdAsync(Id);
        }

        public async Task<GeneralResponse> CreateSemester(SemestersDto model)
        {
            return await _semesterRepository.CreateSemesterAsync(model);
        }

        public async Task<GeneralResponse> UpdateSemester(int Id, SemestersDto model)
        {
            return await _semesterRepository.UpdateSemesterAsync(Id, model);
        }

        public async Task<GeneralResponse> DeleteSemester(int Id)
        {
            return await _semesterRepository.DeleteSemesterAsync(Id);
        }
    }
}
