using EduReg.Common;
using EduReg.Managers;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduReg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {

         private readonly CoursesManager _manager;

        public CoursesController(CoursesManager manager)
        {
            _manager = manager;
        }


        public Task<IActionResult> AssignCoursesToProgramsAsync(string departmentShortName, ProgramCoursesDto model)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> CreateDepartmentCourseAsync(DepartmentCoursesDto model)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route ("CreateDepartmentCourses")]
        public async Task<IActionResult> CreateDepartmentCourseAsync(List<DepartmentCoursesDto> model)
        {
            try
            {
                var response = await _manager.CreateDepartmentCourseAsync(model);

                return StatusCode(response.StatusCore, response);
            }
            catch (Exception ex)
            {
                 
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("UploadDepartmentCourses")]
        public Task<IActionResult> CreateDepartmentCourseAsync(byte[] model)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> CreateProgramCourseAsync(ProgramCoursesDto model)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> CreateProgramCourseAsync(List<ProgramCoursesDto> model)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> CreateProgramCourseAsync(byte[] model)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> DeleteDepartmentCourseAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> DeleteProgramCourseAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> GetAllDepartmentsByCoursesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> GetAllProgramsByCoursesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> GetDepartmentCoursesByDepartmentNameAsync(string shortname)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> GetDepartmentCoursesByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> GetProgramCoursesByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> GetProgramCoursesByProgramNameAsync(string programName)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> UpdateDepartmentCourseAsync(int Id, DepartmentCoursesDto model)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> UpdateProgramCourseAsync(int Id, ProgramCoursesDto model)
        {
            throw new NotImplementedException();
        }
    }
}
