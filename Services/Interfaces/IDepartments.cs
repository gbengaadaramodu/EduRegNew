using EduReg.Common;
using EduReg.Models.Dto;

namespace EduReg.Services.Interfaces
{
    public interface IDepartments
    {
        Task<GeneralResponse> CreateDepartmentAsync(DepartmentsDto model);
        Task<GeneralResponse> UpdateDepartmentAsync(int Id, DepartmentsDto model);
        Task<GeneralResponse> DeleteDepartmentAsync(int Id);
        Task<GeneralResponse> GetDepartmentByIdAsync(int Id);
        Task<GeneralResponse> GetDepartmentByNameAsync(string DepartmentName);
        Task<GeneralResponse> GetAllDepartmentsAsync();
    }
}
