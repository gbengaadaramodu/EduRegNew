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
            return await _semesterRepository.CreateSemesterAsync(model);
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
            return await _semesterRepository.DeleteSemesterAsync(Id);
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
            return await _semesterRepository.GetAllSemestersAsync();
        }

        public Task<GeneralResponse> GetSemesterByIdAsync(int Id)
        {
            return await _semesterRepository.GetSemesterByIdAsync(Id);
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
            return await _semesterRepository.UpdateSemesterAsync(Id, model);
        }
    }
}
