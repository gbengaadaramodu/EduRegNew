using EduReg.Common;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request;

namespace EduReg.Services.Interfaces
{
    public interface IStudentRecords
    {
        Task<GeneralResponse> GetAllStudentRecords(PagingParameters paging, StudentRecordsFilter filter);
        Task<GeneralResponse> GetStudentRecordsById(string id);
        Task<GeneralResponse> UpdateStudentRecords(string id, UpdateStudentRecordsDto model);

        string GenerateMatricNumber(string programmeCode, int sessionId);
    }
}