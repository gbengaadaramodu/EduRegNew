using EduReg.Common;
using EduReg.Models.Dto;

namespace EduReg.Services.Interfaces
{
    public interface IFaculties
    {
        Task<GeneralResponse> CreateFacultyAsync(FacultiesDto model);
        Task<GeneralResponse> UpdateFacultyAsync(int Id, FacultiesDto model);
        Task<GeneralResponse> DeleteFacultyAsync(int Id);
        Task<GeneralResponse> GetFacultyByIdAsync(int Id);
        Task<GeneralResponse> GetAllFacultiesAsync();
    }
}
