using EduReg.Common;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request;
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

        public async Task<GeneralResponse> DeleteAcademicSessionAsync(long Id)
        {
            return await _sessions.DeleteAcademicSessionAsync(Id);
        }

        public async Task<GeneralResponse> DeleteAdmissionBatchAsync(long Id)
        {
            return await _batches.DeleteAdmissionBatchAsync(Id);
        }

        public async Task<GeneralResponse> DeleteAcademicLevelAsync(long Id)
        {
            return await _levels.DeleteAcademicLevelAsync(Id);
        }

        public async Task<GeneralResponse> DeleteSemesterAsync(long Id)
        {
            return await _semesters.DeleteSemesterAsync(Id);
        }

        public async Task<GeneralResponse> GetAcademicSessionByIdAsync(long Id)
        {
            return await _sessions.GetAcademicSessionByIdAsync(Id);
        }

        public async Task<GeneralResponse> GetAdmissionBatchByIdAsync(long Id)
        {
            return await _batches.GetAdmissionBatchByIdAsync(Id);
        }

        public async Task<GeneralResponse> GetAcademicLevelByIdAsync(long Id)
        {
          return  await _levels.GetAcademicLevelByIdAsync(Id);
        }


        public async Task<GeneralResponse> GetAllAcademicSessionsAsync(PagingParameters paging, AcademicSessionFilter filter)
        {
            return await _sessions.GetAllAcademicSessionsAsync(paging, filter);
        }

        public async Task<GeneralResponse> GetAllAdmissionBatchAsync(PagingParameters paging)
        {
            return await _batches.GetAllAdmissionBatchAsync(paging);
        }

        public async Task<GeneralResponse> GetAllAcademicLevelsAsync(PagingParameters paging, AcademicLevelFilter filter)
        {
            return await _levels.GetAllAcademicLevelAsync(paging, filter);
        }

        public async Task<GeneralResponse> GetAllSemestersAsync(PagingParameters paging, SemesterFilter filter)
        {
            return await _semesters.GetAllSemestersAsync(paging, filter);
        }

        public async Task<GeneralResponse> GetSemesterByIdAsync(long Id)
        {
            return await _semesters.GetSemesterByIdAsync(Id);
        }

        public async Task<GeneralResponse> UpdateAcademicSessionAsync(long Id, AcademicSessionsDto model)
        {
            return await _sessions.UpdateAcademicSessionAsync(Id, model);
        }


        public async Task<GeneralResponse> UpdateAdmissionBatchAsync(long Id, UpdateAdmissionBatchesDto model)
        {
            return await _batches.UpdateAdmissionBatchAsync(Id, model);
        }

        public async Task<GeneralResponse> UpdateAdmissionBatchByShortNameAsync(string shortName, UpdateAdmissionBatchesDto model)
        {
            return await _batches.UpdateAdmissionBatchByShortNameAsync(shortName, model);
        }

        public async Task<GeneralResponse> UpdateAcademicLevelAsync(long Id, AcademicLevelsDto model)
        {
        return await _levels.UpdateAcademicLevelAsync(Id, model);
        }

        public async Task<GeneralResponse> UpdateSemesterAsync(long Id, SemestersDto model)
        {
            return await _semesters.UpdateSemesterAsync(Id, model);
        }

        
    }
}
