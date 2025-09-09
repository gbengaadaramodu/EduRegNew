using EduReg.Common;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;

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

        public Task<GeneralResponse> CreateDepartmentAsync(DepartmentsDto model)
        {
            
            throw new NotImplementedException();
        }

        public async Task<GeneralResponse> CreateFacultyAsync(FacultiesDto model)
        {
            return await _faculties.CreateFacultyAsync(model);
        }

        public Task<GeneralResponse> DeleteDepartmentAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<GeneralResponse> DeleteFacultyAsync(int Id)
        {
            return await _faculties.DeleteFacultyAsync(Id);
        }

        public Task<GeneralResponse> GetAllDepartmentsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<GeneralResponse> GetAllFacultiesAsync()
        {
            return await _faculties.GetAllFacultiesAsync();
        }

        public Task<GeneralResponse> GetDepartmentByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetDepartmentByNameAsync(string DepartmentName)
        {
            throw new NotImplementedException();
        }

        public async Task<GeneralResponse> GetFacultyByIdAsync(int Id)
        {
            return await _faculties.GetFacultyByIdAsync(Id);
        }

        public Task<GeneralResponse> UpdateDepartmentAsync(int Id, DepartmentsDto model)
        {
            throw new NotImplementedException();
        }

        public async Task<GeneralResponse> UpdateFacultyAsync(int Id, FacultiesDto model)
        {
            return await _faculties.UpdateFacultyAsync(Id, model);
        }
    }
}
