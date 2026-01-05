using EduReg.Common;
using EduReg.Models.Dto;

namespace EduReg.Services.Interfaces
{
    public interface IFaculties
    {
        Task<GeneralResponse> CreateFacultyAsync(FacultiesDto model);
        Task<GeneralResponse> UpdateFacultyAsync(long Id, FacultiesDto model);
        Task<GeneralResponse> DeleteFacultyAsync(long Id);
        Task<GeneralResponse> GetFacultyByIdAsync(long Id);
        Task<GeneralResponse> GetFacultyByCodeAsync(string facultyCode);
        Task<GeneralResponse> GetAllFacultiesAsync();
    }
}
