using EduReg.Common;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;


namespace EduReg.Managers
{
    

    public class AcademicsManager: IAdmissionBatches, IAcademicSessions, ISemesters
    {
        private readonly IAdmissionBatches _batches;
        private readonly IAcademicSessions _sessions;
        private readonly IAcademics _levels;
        private readonly ISemesters _semesters;
        public AcademicsManager(IAdmissionBatches batches, IAcademicSessions sessions, IAcademics levels, ISemesters semesters)
        {
            _batches = batches;
            _sessions = sessions;
                                     _levels = levels;  
            _semesters = semesters;
        }

        public async Task<GeneralResponse> CreateAcademicSessionAsync(AcademicSessionsDto model)
        {
            return await _sessions.CreateAcademicSessionAsync(model);
        }

        public async Task<GeneralResponse> CreateAdmissionBatchAsync(AdmissionBatchesDto model)
        {
            return await _batches.CreateAdmissionBatchAsync(model);
        }

        public  async Task<GeneralResponse> CreateAcademicLevelAsync(AcademicLevelsDto model)
        {
           return await _levels.CreateAcademicLevel(model);
        }

        public async Task<GeneralResponse> CreateSemesterAsync(SemestersDto model)
        {
            return await _semesters.CreateSemesterAsync(model);
        }

        public async Task<GeneralResponse> DeleteAcademicSessionAsync(int Id)
        {
            return await _sessions.DeleteAcademicSessionAsync(Id);
        }

        public async Task<GeneralResponse> DeleteAdmissionBatchAsync(int Id)
        {
            return await _batches.DeleteAdmissionBatchAsync(Id);
        }

        public async Task<GeneralResponse> DeleteAcademicLevelAsync(int Id)
        {
            return await _levels.DeleteAcademicLevelAsync(Id);
        }

        public async Task<GeneralResponse> DeleteSemesterAsync(int Id)
        {
            return await _semesters.DeleteSemesterAsync(Id);
        }

        public async Task<GeneralResponse> GetAcademicSessionByIdAsync(int Id)
        {
            return await _sessions.GetAcademicSessionByIdAsync(Id);
        }

        public async Task<GeneralResponse> GetAdmissionBatchByIdAsync(int Id)
        {
            return await _batches.GetAdmissionBatchByIdAsync(Id);
        }

        public async Task<GeneralResponse> GetAcademicLevelByIdAsync(int Id)
        {
          return  await _levels.GetAcademicLevelByIdAsync(Id);
        }


        public async Task<GeneralResponse> GetAllAcademicSessionsAsync()
        {
            return await _sessions.GetAllAcademicSessionsAsync();
        }

        public async Task<GeneralResponse> GetAllAdmissionBatchAsync()
        {
            return await _batches.GetAllAdmissionBatchAsync();
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

        public async Task<GeneralResponse> UpdateAcademicSessionAsync(int Id, AcademicSessionsDto model)
        {
            return await _sessions.UpdateAcademicSessionAsync(Id, model);
        }

        public async Task<GeneralResponse> UpdateAdmissionBatchAsync(int Id, AdmissionBatchesDto model)
        {
            return await _batches.UpdateAdmissionBatchAsync(Id, model);
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
