using EduReg.Managers;
using EduReg.Models.Dto;
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
        private readonly IConfiguration _config;
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
    }
}
