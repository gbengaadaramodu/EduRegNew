using EduReg.Common;
using EduReg.Managers;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EduReg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        // This controller is responsible for handling student-related requests.
        private readonly StudentManager _studentRepository;
        public StudentsController(StudentManager studentManager)
        {
            // Initialize any dependencies here if needed
            _studentRepository = studentManager; // Assuming you have a concrete implementation of IStudent
        }

        [HttpPost]
        [Route("LoginStudent")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [Produces("application/json")]
        public async Task<IActionResult> LoginStudent([FromBody] StudentLogin model)
        {
            try
            {
                var result = await _studentRepository.StudentLogin(model);  
                if(result.isSuccess)
                {
                    return Ok(new { Data = result.item,Status = 200, Message = result.message });
                }
                else
                {
                    return BadRequest(new { Status = 404, Message = result.message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Status = 500, Message = ex.Message });
            }
        }


        [HttpPost]
        [Route("CreateCourseRegistration")]
        public async Task<IActionResult> CreateDepartmentCourse([FromBody] CreateCourseRegistrationDto model)
        {
            var response = await _studentRepository.CreateCourseRegistrationAsync(model);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("GetCourseRegistration")]
        public async Task<IActionResult> GetCourseRegistration([FromQuery] CourseRegistrationRequestDto model)
        {
            var response = await _studentRepository.GetCourseRegistration(model);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("GetCourseRegistrationById/{id}")]
        public async Task<IActionResult> GetCourseRegistrationById(int id)
        {
            var response = await _studentRepository.GetCourseRegistrationById(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("GetAllStudentRecords")]
        public async Task<IActionResult> GetAllStudentRecords([FromQuery] PagingParameters paging, [FromQuery] StudentRecordsFilter filter)
        {
            var response = await _studentRepository.GetAllStudentRecords(paging, filter);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("GetStudentRecordsById/{id}")]
        public async Task<IActionResult> GetStudentRecordsById(string id)
        {
            var response = await _studentRepository.GetStudentRecordsById(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut] // Using HttpPut for updates
        [Route("UpdateStudentRecords/{id}")]
        public async Task<IActionResult> UpdateStudentRecords(string id, [FromBody] UpdateStudentRecordsDto model)
        {
            var response = await _studentRepository.UpdateStudentRecords(id, model);
            return StatusCode(response.StatusCode, response);
        }
    }
}
