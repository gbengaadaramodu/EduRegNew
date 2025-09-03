using EduReg.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduReg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsController : ControllerBase
    {
        private readonly ILogger<SchoolsController> _logger;
        private readonly SchoolManager _manager;
        public SchoolsController(ILogger<SchoolsController> logger, SchoolManager manager)
        {
            _logger = logger;
            _manager = manager;
        }

       
        [HttpGet("faculties")]
        public async Task<IActionResult> GetAllFaculties()
        {
            var response = await _manager.GetAllFacultiesAsync();
            if (response.StatusCore == StatusCodes.Status500InternalServerError)
            {
                _logger.LogError($"Error retrieving faculties: {response.Message}");
            }
            return StatusCode(response.StatusCore, response);
        }

    }
}
