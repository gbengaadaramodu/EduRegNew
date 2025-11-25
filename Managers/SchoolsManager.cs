using Azure;
using EduReg.Common;
using EduReg.Managers;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;
using EduReg.Services.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduReg.Managers
{

    public class SchoolsManager: IFaculties, IDepartments, IProgrammes
    {
        private  readonly IFaculties _faculties; 
        private  readonly IDepartments _departments;
        private readonly IProgrammes _programmes;
        public SchoolsManager(IFaculties faculties, IDepartments departments, IProgrammes programmes)
        {
            _faculties = faculties;
            _departments = departments;
            _programmes = programmes;
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

        public async Task<GeneralResponse> CreateProgrammeAsync(ProgrammesDto model)
        {
            return await _programmes.CreateProgrammeAsync(model);
        }

        public async Task<GeneralResponse> DeleteProgrammeAsync(int Id)
        {
            return await _programmes.DeleteProgrammeAsync(Id);
        }

        public async Task<GeneralResponse> GetAllProgrammesAsync()
        {
            return await _programmes.GetAllProgrammesAsync();
        }

        public async Task<GeneralResponse> GetProgrammeByIdAsync(int Id)
        {
            return await _programmes.GetProgrammeByIdAsync(Id);
        }

        public async Task<GeneralResponse> GetProgrammeByNameAsync(string ProgrammeName)
        {
            return await _programmes.GetProgrammeByNameAsync(ProgrammeName);
        }

        public async Task<GeneralResponse> UpdateProgrammeAsync(int id, ProgrammesDto model)
        {
            return await _programmes.UpdateProgrammeAsync(id, model);
        }

        public Task<GeneralResponse> GetFacultyByCodeAsync(string facultyCode)
        {
            throw new NotImplementedException();
        }
    }
}
