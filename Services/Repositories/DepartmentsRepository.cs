using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;

namespace EduReg.Services.Repositories
{
    public class DepartmentsRepository : IDepartments
    {
        private readonly ApplicationDbContext _context;

        public DepartmentsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<GeneralResponse> CreateDepartmentAsync(DepartmentsDto model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> DeleteDepartmentAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetAllDepartmentsAsync()
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

        public Task<GeneralResponse> UpdateDepartmentAsync(int Id, DepartmentsDto model)
        {
            throw new NotImplementedException();
        }
    }
}
