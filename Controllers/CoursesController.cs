

using Azure;
using EduReg.Common;
using EduReg.Managers;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EduReg.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly CoursesManager _coursesManager;

        public CourseController(CoursesManager coursesManager)
        {
            _coursesManager = coursesManager;
        }

        // =========================
        // Department Courses
        // =========================

        [HttpPost]
        [Route("CreateDepartmentCourse")]
        public async Task<IActionResult> CreateDepartmentCourse([FromBody] DepartmentCoursesDto model)
        {
            var response = await _coursesManager.CreateDepartmentCourseAsync(model);
           return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("CreateDepartmentCoursesFromList")]
        public async Task<IActionResult> CreateDepartmentCoursesFromList([FromBody] List<DepartmentCoursesDto> model)
        {
            var response = await _coursesManager.CreateDepartmentCourseAsync(model);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("UploadDepartmentalCourses")]
        public async Task<IActionResult> UploadDepartmentCoursesFromFile(string base64filestring)

        {
            byte[] file = Convert.FromBase64String(base64filestring);
            var response = await _coursesManager.UploadDepartmentCourseAsync(file);
            return StatusCode(response.StatusCode, response);
        }


        [HttpPut]
        [Route("UpdateDepartmentCourse/{id}")]
        public async Task<IActionResult> UpdateDepartmentCourse(int id, [FromBody] DepartmentCoursesDto model)
        {
            var response = await _coursesManager.UpdateDepartmentCourseAsync(id, model);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete]
        [Route("DeleteDepartmentCourse/{id}")]
        public async Task<IActionResult> DeleteDepartmentCourse(int id)
        {
            var response = await _coursesManager.DeleteDepartmentCourseAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("GetDepartmentCourseById/{id}")]
        public async Task<IActionResult> GetDepartmentCourseById(int id)
        {
            var response = await _coursesManager.GetDepartmentCoursesByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("GetDepartmentCoursesByDepartmentName/{shortname}")]
        public async Task<IActionResult> GetDepartmentCoursesByDepartmentName(string shortname)
        {
            var response = await _coursesManager.GetDepartmentCoursesByDepartmentNameAsync(shortname);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("GetAllDepartmentsByCourses")]
        public async Task<IActionResult> GetAllDepartmentsByCourses()
        {
            var response = await _coursesManager.GetAllDepartmentsByCoursesAsync();
            return StatusCode(response.StatusCode, response);
        }

        // =========================
        // Program Courses
        // =========================

        [HttpPost]
        [Route("CreateProgramCourse")]
        public async Task<IActionResult> CreateProgramCourse([FromBody] ProgramCoursesDto model)
        {
            var response = await _coursesManager.CreateProgramCourseAsync(model);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("CreateProgramCoursesFromList")]
        public async Task<IActionResult> CreateProgramCourses([FromBody] List<ProgramCoursesDto> model)
        {
            var response = await _coursesManager.CreateProgramCourseAsync(model);
            return StatusCode(response.StatusCode, response);
        }

        

        [HttpPut]
        [Route("UpdateProgramCourse/{id}")]
        public async Task<IActionResult> UpdateProgramCourse(int id, [FromBody] ProgramCoursesDto model)
        {
            var response = await _coursesManager.UpdateProgramCourseAsync(id, model);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete]
        [Route("DeleteProgramCourse/{id}")]
        public async Task<IActionResult> DeleteProgramCourse(int id)
        {
            var response = await _coursesManager.DeleteProgramCourseAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("GetProgramCourseById/{id}")]
        public async Task<IActionResult> GetProgramCourseById(int id)
        {
            var response = await _coursesManager.GetProgramCoursesByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("GetProgramCoursesByProgramName/{programName}")]
        public async Task<IActionResult> GetProgramCoursesByProgramName(string programName)
        {
            var response = await _coursesManager.GetProgramCoursesByProgramNameAsync(programName);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("GetAllProgramsByCourses")]
        public async Task<IActionResult> GetAllProgramsByCourses()
        {
            var response = await _coursesManager.GetAllProgramsByCoursesAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("AssignCourseToProgram/{departmentShortName}")]
        public async Task<IActionResult> AssignCourseToProgram(string departmentShortName, [FromBody] ProgramCoursesDto model)
        {
            var response = await _coursesManager.AssignCoursesToProgramsAsync(departmentShortName, model);
            return StatusCode(response.StatusCode, response);
        }

        // =========================
        // Course Schedule
        // =========================

        [HttpPost]
        [Route("CreateCourseSchedule")]
        public async Task<IActionResult> CreateCourseSchedule([FromBody] CourseScheduleDto model)
        {
            var response = await _coursesManager.CreateCourseScheduleAsync(model);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("CreateCourseSchedulesFromList")]
        public async Task<IActionResult> CreateCourseSchedules([FromBody] List<CourseScheduleDto> model)
        {
            var response = await _coursesManager.CreateCourseScheduleAsync(model);
            return StatusCode(response.StatusCode, response);
        }
 
         

        [HttpPut]
        [Route("UpdateCourseSchedule/{id}")]
        public async Task<IActionResult> UpdateCourseSchedule(int id, [FromBody] CourseScheduleDto model)
        {
            var response = await _coursesManager.UpdateCourseScheduleAsync(id, model);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete]
        [Route("DeleteCourseSchedule/{id}")]
        public async Task<IActionResult> DeleteCourseSchedule(int id)
        {
            var response = await _coursesManager.DeleteCourseScheduleAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete]
        [Route("DeleteManySchedules")]
        public async Task<IActionResult> DeleteManySchedules([FromBody] List<int> ids)
        {
            var response = await _coursesManager.DeleteManyCourseSchedulesAsync(ids);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete]
        [Route("DeleteManySchedulesByModel")]
        public async Task<IActionResult> DeleteManySchedulesByModel([FromBody] List<CourseScheduleDto> model)
        {
            var response = await _coursesManager.DeleteManyCourseSchedulesAsync(model);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("GetCourseScheduleById/{id}")]
        public async Task<IActionResult> GetCourseScheduleById(int id)
        {
            var response = await _coursesManager.GetCourseScheduleByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("GetScheduleByCourseCode/{courseCode}")]
        public async Task<IActionResult> GetScheduleByCourseCode(string courseCode)
        {
            var response = await _coursesManager.GetCourseScheduleByCourseCodeAsync(courseCode);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("GetAllCourseSchedules")]
        public async Task<IActionResult> GetAllCourseSchedules()
        {
            var response = await _coursesManager.GetAllCourseSchedulesAsync();
            return StatusCode(response.StatusCode, response);
        }
    }
}
