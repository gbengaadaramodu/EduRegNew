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
    }
}
