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

        public Task<GeneralResponse> CreateFacultyAsync(FacultiesDto model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> DeleteDepartmentAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> DeleteFacultyAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetAllDepartmentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetAllFacultiesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetDepartmentByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetDepartmentByNameAsync(string DepartmentName)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetFacultyByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> UpdateDepartmentAsync(int Id, DepartmentsDto model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> UpdateFacultyAsync(int Id, FacultiesDto model)
        {
            throw new NotImplementedException();
        }
    }
}
