using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;

namespace EduReg.Services.Repositories
{

    public class DepartmentCoursesRepository : IDepartmentCourses
    {
        private readonly ApplicationDbContext _context;
        public DepartmentCoursesRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<GeneralResponse> CreateDepartmentCourseAsync(DepartmentCoursesDto model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> CreateDepartmentCourseAsync(List<DepartmentCoursesDto> model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> CreateDepartmentCourseAsync(byte[] model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> DeleteDepartmentCourseAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetAllDepartmentsByCoursesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetDepartmentCoursesByDepartmentNameAsync(string shortname)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> GetDepartmentCoursesByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> UpdateDepartmentCourseAsync(int Id, DepartmentCoursesDto model)
        {
            throw new NotImplementedException();
        }
    }
}
