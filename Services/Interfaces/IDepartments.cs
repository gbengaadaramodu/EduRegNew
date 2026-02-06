using EduReg.Common;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request;

namespace EduReg.Services.Interfaces
{
    public interface IDepartments
    {
        Task<GeneralResponse> CreateDepartmentAsync(DepartmentsDto model);
        Task<GeneralResponse> UpdateDepartmentAsync(long Id, DepartmentsDto model);
        Task<GeneralResponse> DeleteDepartmentAsync(long Id);
        Task<GeneralResponse> GetDepartmentByIdAsync(long Id);
        Task<GeneralResponse> GetDepartmentByNameAsync(string DepartmentName);
        Task<GeneralResponse> GetAllDepartmentsAsync(PagingParameters paging, DepartmentFilter filter);
    }
}
