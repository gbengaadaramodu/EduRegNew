using EduReg.Common;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;
using EduReg.Services.Repositories;

namespace EduReg.Managers
{

    public class SchoolManager: IFaculties, IDepartments
    {
        private  readonly IFaculties _faculties; 
        private  readonly IDepartments _departments;
        public SchoolManager(IFaculties faculties, IDepartments departments)
        {
            _faculties = faculties;
            _departments = departments;
        }

        public async Task<GeneralResponse> CreateDepartmentAsync(DepartmentsDto model)
        {

            return await _departments.CreateDepartmentAsync(model);
        }

        public async Task<GeneralResponse> CreateFacultyAsync(FacultiesDto model)
        {
            return await _faculties.CreateFacultyAsync(model);
        }

        public async Task<GeneralResponse> DeleteDepartmentAsync(int Id)
        {
            return await _departments.DeleteDepartmentAsync(Id);
        }

        public async Task<GeneralResponse> DeleteFacultyAsync(int Id)
        {
            return await _faculties.DeleteFacultyAsync(Id);
        }

        public async Task<GeneralResponse> GetAllDepartmentsAsync()
        {
            return await _departments.GetAllDepartmentsAsync();
        }

        public async Task<GeneralResponse> GetAllFacultiesAsync()
        {
            return await _faculties.GetAllFacultiesAsync();
        }

        public async Task<GeneralResponse> GetDepartmentByIdAsync(int Id)
        {
            return await _departments.GetDepartmentByIdAsync(Id);
        }

        public async Task<GeneralResponse> GetDepartmentByNameAsync(string DepartmentName)
        {
            return await _departments.GetDepartmentByNameAsync(DepartmentName);
        }

        public async Task<GeneralResponse> GetFacultyByIdAsync(int Id)
        {
            return await _faculties.GetFacultyByIdAsync(Id);
        }

        public async Task<GeneralResponse> UpdateDepartmentAsync(int Id, DepartmentsDto model)
        {
            return await _departments.UpdateDepartmentAsync(Id, model);
        }

        public async Task<GeneralResponse> UpdateFacultyAsync(int Id, FacultiesDto model)
        {
            return await _faculties.UpdateFacultyAsync(Id, model);
        }
    }
}
