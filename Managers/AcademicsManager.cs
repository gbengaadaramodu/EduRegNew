using EduReg.Common;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;
using EduReg.Services.Repositories;


namespace EduReg.Managers
{
    

    public class AcademicsManager: IAdmissionBatches, IAcademicSessions, ISemesters
    {
        private readonly IAdmissionBatches _batches;
        private readonly IAcademicSessions _sessions;
        private readonly IAcademicLevels _levels;
        private readonly ISemesters _semesters;
        public AcademicsManager(IAdmissionBatches batches, IAcademicSessions sessions, IAcademicLevels levels, ISemesters semesters)
        {
            _batches = batches;
            _sessions = sessions;
            _levels = levels;
            _semesters = semesters;
        }

        public  Task<GeneralResponse> CreateAcademicSessionAsync(AcademicSessionsDto model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> CreateAdmissionBatchAsync(AdmissionBatchesDto model)
        {
            throw new NotImplementedException();
        }

        public  async Task<GeneralResponse> CreateAcademicLevelAsync(AcademicLevelsDto model)
        {
           return await _levels.CreateAcademicLevel(model);
        }

        public async Task<GeneralResponse> CreateSemesterAsync(SemestersDto model)
        {
            return await _semesters.CreateSemesterAsync(model);
        }

        public Task<GeneralResponse> DeleteAcademicSessionAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> DeleteAdmissionBatchAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<GeneralResponse> DeleteAcademicLevelAsync(int Id)
        {
            return await _levels.DeleteAcademicLevelAsync(Id);
        }

        public async Task<GeneralResponse> DeleteSemesterAsync(int Id)
        {
            return await _semesters.DeleteSemesterAsync(Id);
        }

        public Task<GeneralResponse> GetAcademicSessionByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetAdmissionBatchByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<GeneralResponse> GetAcademicLevelByIdAsync(int Id)
        {
          return  await _levels.GetAcademicLevelByIdAsync(Id);
        }


        public Task<GeneralResponse> GetAllAcademicSessionsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetAllAdmissionBatchAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<GeneralResponse> GetAllAcademicLevelsAsync()
        {
            return await _levels.GetAllAcademicLevelAsync();
        }

        public async Task<GeneralResponse> GetAllSemestersAsync()
        {
            return await _semesters.GetAllSemestersAsync();
        }

        public async Task<GeneralResponse> GetSemesterByIdAsync(int Id)
        {
            return await _semesters.GetSemesterByIdAsync(Id);
        }

        public Task<GeneralResponse> UpdateAcademicSessionAsync(int Id, AcademicSessionsDto model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> UpdateAdmissionBatchAsync(int Id, AdmissionBatchesDto model)
        {
            throw new NotImplementedException();
        }

        public async Task<GeneralResponse> UpdateAcademicLevelAsync(int Id, AcademicLevelsDto model)
        {
        return await _levels.UpdateAcademicLevelAsync(Id, model);
        }

        public async Task<GeneralResponse> UpdateSemesterAsync(int Id, SemestersDto model)
        {
            return await _semesters.UpdateSemesterAsync(Id, model);
        }

        
    }
}
