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

        public Task<GeneralResponse> CreateFacultyAsync(FacultiesDto model)
        {
            throw new NotImplementedException();
        }

        public async Task<GeneralResponse> DeleteDepartmentAsync(int Id)
        {
            return await _departments.DeleteDepartmentAsync(Id);
        }

        public Task<GeneralResponse> DeleteFacultyAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<GeneralResponse> GetAllDepartmentsAsync()
        {
            return await _departments.GetAllDepartmentsAsync();
        }

        public Task<GeneralResponse> GetAllFacultiesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<GeneralResponse> GetDepartmentByIdAsync(int Id)
        {
            return await _departments.GetDepartmentByIdAsync(Id);
        }

        public async Task<GeneralResponse> GetDepartmentByNameAsync(string DepartmentName)
        {
            return await _departments.GetDepartmentByNameAsync(DepartmentName);
        }

        public Task<GeneralResponse> GetFacultyByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<GeneralResponse> UpdateDepartmentAsync(int Id, DepartmentsDto model)
        {
            return await _departments.UpdateDepartmentAsync(Id, model);
        }

        public Task<GeneralResponse> UpdateFacultyAsync(int Id, FacultiesDto model)
        {
            throw new NotImplementedException();
        }
    }
}
