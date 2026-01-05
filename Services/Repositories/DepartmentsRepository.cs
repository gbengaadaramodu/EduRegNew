using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduReg.Services.Repositories
{
    public class DepartmentsRepository : IDepartments
    {
        private readonly ApplicationDbContext _context;

        public DepartmentsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<GeneralResponse> CreateDepartmentAsync(DepartmentsDto model)
        {
            try
            {
                var existingDepartment = await _context.Departments
                 .FirstOrDefaultAsync(d => d.DepartmentCode == model.DepartmentCode);

                if (existingDepartment != null)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 400,
                        Message = "Department with this code already exists",
                        Data = null
                    };
                }
                var department = new Departments
                {
                    FacultyCode = model.FacultyCode,
                    DepartmentName = model.DepartmentName,
                    DepartmentCode = model.DepartmentCode,
                    Description = model.Description,
                   // Programme = model.Programme,
                    Duration = model.Duration,
                    NumberofSemesters = model.NumberofSemesters,
                    MaximumNumberofSemesters = model.MaximumNumberofSemesters
                };

               await _context.Departments.AddAsync(department);
                await _context.SaveChangesAsync();

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "Department Created Successfully",
                    Data = department
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = ex.Message,
                    Data = null
                };

            }
        }

        public async Task<GeneralResponse> DeleteDepartmentAsync(long Id)
        {
            try
            {
                var department = await _context.Departments.FindAsync(Id);

                if (department == null)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 404,
                        Message = "Department not found",
                        Data = null
                    };
                }

                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "Deleted successfully",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<GeneralResponse> GetAllDepartmentsAsync()
        {
            var departments = await _context.Departments.ToListAsync();

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Success",
                Data = departments
            };
        }

        public async Task<GeneralResponse> GetDepartmentByIdAsync(long Id)
        {
            var department = await _context.Departments.FindAsync(Id);

            if (department == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "Department not found",
                    Data = null
                };
            }

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Success",
                Data = department
            };
        }

        public async Task<GeneralResponse> GetDepartmentByNameAsync(string DepartmentName)
        {
            var department = await _context.Departments
                .FirstOrDefaultAsync(d => d.DepartmentName.ToLower().Contains(DepartmentName.ToLower()));

            if (department == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "Department not found",
                    Data = null
                };
            }

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Success",
                Data = department
            };
        }


        public async Task<GeneralResponse> UpdateDepartmentAsync(long Id, DepartmentsDto model)
        {
            try
            {
                var department = await _context.Departments.FindAsync(Id);

                if (department == null)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 404,
                        Message = "Department not found",
                        Data = null
                    };
                }

                department.FacultyCode = model.FacultyCode;
                department.DepartmentName = model.DepartmentName;
                department.Description = model.Description;
               // department.Programme = model.Programme;
                department.Duration = model.Duration;
                department.NumberofSemesters = model.NumberofSemesters;
                department.MaximumNumberofSemesters = model.MaximumNumberofSemesters;

                await _context.SaveChangesAsync();

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "Updated successfully",
                    Data = department
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = ex.Message,
                    Data = null
                };
            }
        }
    }
    
}
