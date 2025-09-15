using Azure;
using EduReg.Managers;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;
using EduReg.Services.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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

        [HttpGet]
        [Route("GetAllFaculties")]
        public async Task<IActionResult> GetAllFacultiesAsync()
        {

            var response = await _manager.GetAllFacultiesAsync();
            return StatusCode(response.StatusCore, response);
        }
        [HttpPost]
        [Route("CreateFaculty")]
        public async Task<IActionResult> CreateFacultyAsync([FromBody] FacultiesDto model)
        {

            var response = await _manager.CreateFacultyAsync(model);
            return StatusCode(response.StatusCore, response);

        }

        [HttpPut]
        [Route("UpdateFaculty/{id}")]
        public async Task<IActionResult> UpdateFacultyAsync(int id, [FromBody] FacultiesDto model)
        {

            var response = await _manager.UpdateFacultyAsync(id, model);
            return StatusCode(response.StatusCore, response);

        }



        [HttpDelete]
        [Route("DeleteFaculty/{id}")]
        public async Task<IActionResult> DeleteFacultyAsync(int id)
        {

            var response = await _manager.DeleteFacultyAsync(id);
            return StatusCode(response.StatusCore, response);

        }


        [HttpGet]
        [Route("GetByFacultiesById/{id}")]
        public async Task<IActionResult> GetByFacultiesByIdAsync(int id)
        {

            var response = await _manager.GetFacultyByIdAsync(id);
            return StatusCode(response.StatusCore, response);


        }

        [HttpPost]
        [Route("Createdepartment")]
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
        [Route("GetdepartmentbyId/{id}")]
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
                        
            return StatusCode(result.StatusCore, result);

        }

        [HttpGet]
        [Route("GetAllDepartments")]
        public async Task<IActionResult> GetAllDepartmentsAsync()
        {
            var result = await _manager.GetAllDepartmentsAsync();
            return StatusCode(result.StatusCore, result);
        }


        [HttpPost]
        [Route("CreateProgramme")]
        public async Task<IActionResult> CreateProgrammeAsync([FromBody] ProgrammesDto model)
        {
            _logger.LogInformation("POST request received to create a new programme");
            var response = await _manager.CreateProgrammeAsync(model);

            _logger.LogInformation($"{response.Message}");
            return StatusCode(response.StatusCore, response);
        }


        [HttpDelete]
        [Route("DeleteProgramme/{id}")]
        public async Task<IActionResult> DeleteProgrammeAsync(int id)
        {
            _logger.LogInformation($"DELETE request received for programme with ID: {id}");
            var response = await _manager.DeleteProgrammeAsync(id);

            _logger.LogInformation($"{response.Message}");
            return StatusCode(response.StatusCore, response);
        }


        [HttpGet]
        [Route("GetAllProgrammes")]
        public async Task<IActionResult> GetAllProgrammesAsync()
        {
            _logger.LogInformation("GET request received to fetch all programmes");
            var response = await _manager.GetAllProgrammesAsync();

            _logger.LogInformation($"{response.Message}");
            return StatusCode(response.StatusCore, response);

        }

        [HttpGet]
        [Route("GetProgrammeById/{id}")]
        public async Task<IActionResult> GetProgrammeByIdAsync(int id)
        {
            _logger.LogInformation($"GET request received for programme with ID: {id}");
            var response = await _manager.GetProgrammeByIdAsync(id);

            _logger.LogInformation($"{response.Message}");
            return StatusCode(response.StatusCore, response);
        }


        [HttpGet]
        [Route("GetProgrammeByName/{programmeName}")]
        public async Task<IActionResult> GetProgrammeByNameAsync(string programmeName)
        {
            _logger.LogInformation($"GET request received for programme with Name: {programmeName}");
            var response = await _manager.GetProgrammeByNameAsync(programmeName);

            _logger.LogInformation($"{response.Message}");
            return StatusCode(response.StatusCore, response);
        }


        [HttpPut]
        [Route("UpdateProgramme/{id}")]
        public async Task<IActionResult> UpdateProgrammeAsync(int id, [FromBody] ProgrammesDto model)
        {
            _logger.LogInformation($"PUT request received to update programme with ID: {id}");
            var response = await _manager.UpdateProgrammeAsync(id, model);

            _logger.LogInformation($"{response.Message}");
            return StatusCode(response.StatusCore, response);
        }

         
       
    }
}
