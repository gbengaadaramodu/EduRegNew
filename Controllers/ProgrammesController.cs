using EduReg.Managers;
using EduReg.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduReg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgrammesController : ControllerBase
    {
        private readonly ILogger<ProgrammesController> _logger;
        private readonly ProgrammesManager _programmesManager;

        public ProgrammesController(ProgrammesManager programmesManager, ILogger<ProgrammesController> logger)
        {
            _programmesManager = programmesManager;
            _logger = logger;
        }


        [HttpPost("CreateProgramme")]
        public async Task<IActionResult> CreateProgramme([FromBody] ProgrammesDto model)
        {
            _logger.LogInformation("POST request received to create a new programme");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for creating a programme");
                return BadRequest(ModelState);
            }
            var response = await _programmesManager.CreateProgrammeAsync(model);

            if (response.StatusCore == 403)
            {
                _logger.LogWarning($"{response.Message}");
                return Conflict(response);
            }

            _logger.LogInformation("Successfully created new programme");
            return Ok(response);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProgramme(int id)
        {
            _logger.LogInformation($"DELETE request received for programme with ID: {id}");
            var response = await _programmesManager.DeleteProgrammeAsync(id);

            if (response.StatusCore == 404)
            {
                _logger.LogWarning($"Program with ID: {id} not found for deletion");
                return NotFound(response);
            }

            _logger.LogInformation($"Successfully deleted programme with ID: {id}");
            return Ok(response);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllProgrammes()
        {
            _logger.LogInformation("GET request received to fetch all programmes");

            var response = await _programmesManager.GetAllProgrammesAsync();

            if (response.StatusCore == 200)
            {
                _logger.LogInformation("Successfully retrieved all programmes");
            }

            else
            {
                _logger.LogError("An error occurred while retrieving all programmes.");
            }

            return Ok(response);

        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProgrammeById(int id)
        {
            _logger.LogInformation($"GET request received for programme with ID: {id}");
            var response = await _programmesManager.GetProgrammeByIdAsync(id);

            if (response.StatusCore == 404)
            {
                _logger.LogWarning($"Programme with ID: {id} not found");
                return NotFound(response);
            }

            _logger.LogInformation($"Successfully retrieved programme with ID: {id}");
            return Ok(response);
        }


        [HttpGet("GetByName/{programmeName}")]
        public async Task<IActionResult> GetProgrammeByName(string programmeName)
        {
            _logger.LogInformation($"GET request received for programme with Name: {programmeName}");
            var response = await _programmesManager.GetProgrammeByNameAsync(programmeName);

            if (response.StatusCore == 404)
            {
                _logger.LogWarning($"Programme with Name: {programmeName} not found");
                return NotFound(response);
            }

            _logger.LogInformation($"Successfully retrieved programme with Name: {programmeName}");
            return Ok(response);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProgramme(int id, [FromBody] ProgrammesDto model)
        {
            _logger.LogInformation($"PUT request received to update programme with ID: {id}");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for updating a programme");
                return BadRequest(ModelState);
            }

            var response = await _programmesManager.UpdateProgrammeAsync(id, model);

            if (response.StatusCore == 404)
            {
                _logger.LogWarning($"Programme with ID: {id} not found for update");
                return NotFound(response);
            }

            _logger.LogInformation($"Successfully updated programme with ID: {id}");
            return Ok(response);
        }
    }
}
