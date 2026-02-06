using Azure;
using EduReg.Common;
using EduReg.Managers;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request;
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

        public async Task<GeneralResponse> DeleteDepartmentAsync(long Id)
        {
            return await _departments.DeleteDepartmentAsync(Id);
        }

        public async Task<GeneralResponse> DeleteFacultyAsync(long Id)
        {
            return await _faculties.DeleteFacultyAsync(Id);
        }

        public async Task<GeneralResponse> GetAllDepartmentsAsync(PagingParameters paging, DepartmentFilter filter)
        {
            return await _departments.GetAllDepartmentsAsync(paging, filter);
        }

        public async Task<GeneralResponse> GetAllFacultiesAsync(PagingParameters paging, FacultyFilter filter)
        {
            return await _faculties.GetAllFacultiesAsync(paging, filter);
        }

        public async Task<GeneralResponse> GetDepartmentByIdAsync(long Id)
        {
            return await _departments.GetDepartmentByIdAsync(Id);
        }

        public async Task<GeneralResponse> GetDepartmentByNameAsync(string DepartmentName)
        {
            return await _departments.GetDepartmentByNameAsync(DepartmentName);
        }

        public async Task<GeneralResponse> GetFacultyByIdAsync(long Id)
        {
            return await _faculties.GetFacultyByIdAsync(Id);
        }

        public async Task<GeneralResponse> UpdateDepartmentAsync(long Id, DepartmentsDto model)
        {
            return await _departments.UpdateDepartmentAsync(Id, model);
        }

        public async Task<GeneralResponse> UpdateFacultyAsync(long Id, FacultiesDto model)
        {
            return await _faculties.UpdateFacultyAsync(Id, model);
        }       

        public async Task<GeneralResponse> CreateProgrammeAsync(ProgrammesDto model)
        {
            return await _programmes.CreateProgrammeAsync(model);
        }

        public async Task<GeneralResponse> DeleteProgrammeAsync(long Id)
        {
            return await _programmes.DeleteProgrammeAsync(Id);
        }

        public async Task<GeneralResponse> GetAllProgrammesAsync(PagingParameters paging, ProgrammeFilter filter)
        {
            return await _programmes.GetAllProgrammesAsync(paging, filter);
        }

        public async Task<GeneralResponse> GetProgrammeByIdAsync(long Id)
        {
            return await _programmes.GetProgrammeByIdAsync(Id);
        }

        public async Task<GeneralResponse> GetProgrammeByNameAsync(string ProgrammeName)
        {
            return await _programmes.GetProgrammeByNameAsync(ProgrammeName);
        }

        public async Task<GeneralResponse> UpdateProgrammeAsync(long id, ProgrammesDto model)
        {
            return await _programmes.UpdateProgrammeAsync(id, model);
        }

        public async Task<GeneralResponse> GetFacultyByCodeAsync(string facultyCode)
        {
            return await _faculties.GetFacultyByCodeAsync(facultyCode);
        }
    }
}
