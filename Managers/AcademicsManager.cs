using EduReg.Common;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;

namespace EduReg.Managers
{
    

    public class AcademicsManager: IAdmissionBatches, IAcademicSessions, ISemesters
    {
        private readonly IAdmissionBatches _batches;
        private readonly IAcademicSessions _sessions;
        private readonly ISemesters _semesters;
        public AcademicsManager(IAdmissionBatches batches, IAcademicSessions sessions, ISemesters semesters)
        {
            _batches = batches;
            _sessions = sessions;
            _semesters = semesters;
        }

        public Task<GeneralResponse> CreateAcademicSessionAsync(AcademicSessionsDto model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> CreateAdmissionBatchAsync(AdmissionBatchesDto model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> CreateSemesterAsync(SemestersDto model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> DeleteAcademicSessionAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> DeleteAdmissionBatchAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> DeleteSemesterAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetAcademicSessionByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetAdmissionBatchByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetAllAcademicSessionsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetAllAdmissionBatchAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetAllSemestersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetSemesterByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> UpdateAcademicSessionAsync(int Id, AcademicSessionsDto model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> UpdateAdmissionBatchAsync(int Id, AdmissionBatchesDto model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> UpdateSemesterAsync(int Id, SemestersDto model)
        {
            throw new NotImplementedException();
        }
    }
}
