using EduReg.Managers;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;
using EduReg.Services.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduReg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsController : ControllerBase
    {
        private readonly ILogger<SchoolsController> _logger;
        private readonly SchoolsManager _manager;
        public SchoolsController(ILogger<SchoolsController> logger, SchoolsManager manager)
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

        [HttpPost]
        [Route("CreateDepartment")]
        public async Task<IActionResult> CreateDepartmentAsync([FromBody] DepartmentsDto model)
        {
            var result = await _manager.CreateDepartmentAsync(model);

            // return StatusCode(result.StatusCore, result);

            if (result.StatusCore == 500)
            {
                _logger.LogError($"Error Creating Department: {result.Message}");
            }

            return StatusCode(result.StatusCore, result);
        }

        [HttpGet]
        [Route("GetDepartmentById/{id}")]
        public async Task<IActionResult> GetDepartmentByIdAsync(int id)
        {
            var result = await _manager.GetDepartmentByIdAsync(id);

            if (result.StatusCore == 500)
            {
                _logger.LogError($"Error Getting Department: {result.Message}");
            }
            return StatusCode(result.StatusCore, result);
        }

        [HttpGet]
        [Route("GetDepartmentByName/{name}")]
        public async Task<IActionResult> GetDepartmentByNameAsync(string name)
        {
            var result = await _manager.GetDepartmentByNameAsync(name);

            if (result.StatusCore == 500)
            {
                _logger.LogError($"Error Getting Department: {result.Message}");
            }
            return StatusCode(result.StatusCore, result);
        }

        [HttpPut]
        [Route("UpdateDepartment/{id}")]
        public async Task<IActionResult> UpdateDepartmentAsync(int id, [FromBody] DepartmentsDto model)
        {
            var result = await _manager.UpdateDepartmentAsync(id, model);

            if (result.StatusCore == 500)
            {
                _logger.LogError($"Error Updating Department: {result.Message}");
            }
            return StatusCode(result.StatusCore, result);
        }

        [HttpDelete]
        [Route("DeleteDepartment/{id}")]
        public async Task<IActionResult> DeleteDepartmentAsync(int id)
        {
            var result = await _manager.DeleteDepartmentAsync(id);

            if (result.StatusCore == 500)
            {
                _logger.LogError($"Error Deleting Department: {result.Message}");
            }
            return StatusCode(result.StatusCore, result);

        }

        [HttpGet]
        [Route("GetAllDepartments")]
        public async Task<IActionResult> GetAllDepartmentsAsync()
        {
            var result = await _manager.GetAllDepartmentsAsync();
            return StatusCode(result.StatusCore, result);
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
            var response = await _manager.CreateProgrammeAsync(model);

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
            var response = await _manager.DeleteProgrammeAsync(id);

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

            var response = await _manager.GetAllProgrammesAsync();

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
            var response = await _manager.GetProgrammeByIdAsync(id);

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
            var response = await _manager.GetProgrammeByNameAsync(programmeName);

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

            var response = await _manager.UpdateProgrammeAsync(id, model);

            if (response.StatusCore == 404)
            {
                _logger.LogWarning($"Programme with ID: {id} not found for update");
                return NotFound(response);
            }

            _logger.LogInformation($"Successfully updated programme with ID: {id}");
            return Ok(response);
        }

        [HttpPost]
        [Route("createFaculty")]

        public async Task<IActionResult> CreateFaculty([FromBody] FacultiesDto model)
        {

            var resp = await _manager.CreateFacultyAsync(model);
            return resp.StatusCore switch
            {
                200 => Ok(resp),
                404 => NotFound(resp),
                400 => BadRequest(resp),
                _ => StatusCode(500, resp)
            };

        }

        [HttpPut("updateFaculty/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FacultiesDto model)
        {

            var resp = await _manager.UpdateFacultyAsync(id, model);
            return resp.StatusCore switch
            {
                200 => Ok(resp),
                404 => NotFound(resp),
                400 => BadRequest(resp),
                _ => StatusCode(500, resp)
            };
        }



        [HttpDelete("deleteFaculty/{id}")]

        public async Task<IActionResult> DeleteFacultyAsync(int id)
        {

            var resp = await _manager.DeleteFacultyAsync(id);
            return resp.StatusCore switch
            {
                200 => Ok(resp),
                404 => NotFound(resp),
                400 => BadRequest(resp),
                _ => StatusCode(500, resp)
            };
        }

        [HttpGet]
        [Route("getAllFaculty")]

        public async Task<IActionResult> GetAll()
        {

            var resp = await _manager.GetAllFacultiesAsync();
            return resp.StatusCore switch
            {
                200 => Ok(resp),
                404 => NotFound(resp),
                400 => BadRequest(resp),
                _ => StatusCode(500, resp)
            };
        }

        [HttpGet("getFacultyById/{id}")]

        public async Task<IActionResult> GetById(int id)
        {

            var resp = await _manager.GetFacultyByIdAsync(id);

            return resp.StatusCore switch
            {
                200 => Ok(resp),
                404 => NotFound(resp),
                400 => BadRequest(resp),
                _ => StatusCode(500, resp)
            };
        }
    }
}
