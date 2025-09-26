using EduReg.Managers;
using EduReg.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EduReg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationsController : ControllerBase
    {
        private readonly RegistrationsManager _manager;

        public RegistrationsController(RegistrationsManager manager)
        {
            _manager = manager;
        }

        // ========================
        // STUDENT REGISTRATION APIs
        // ========================

        [HttpPost("Create")]
        public async Task<IActionResult> CreateStudentRegistration([FromBody] RegistrationsDto model)
        {
            var response = await _manager.CreateStudentRegistrationAsync(model);
            return StatusCode(response.StatusCore, response);
        }

        [HttpPost("CreateBulk")]
        public async Task<IActionResult> CreateBulkStudentRegistration([FromBody] List<RegistrationsDto> models)
        {
            var response = await _manager.CreateStudentRegistrationAsync(models);
            return StatusCode(response.StatusCore, response);
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> UploadStudentRegistration([FromBody] byte[] fileBytes)
        {
            var response = await _manager.CreateStudentRegistrationAsync(fileBytes);
            return StatusCode(response.StatusCore, response);
        }

        [HttpDelete("Drop/{id}")]
        public async Task<IActionResult> DropStudentRegistration(int id)
        {
            var response = await _manager.DropStudentRegistrationAsync(id);
            return StatusCode(response.StatusCore, response);
        }

        [HttpGet("Student/{matricNumber}")]
        public async Task<IActionResult> GetStudentRegistrations(string matricNumber)
        {
            var response = await _manager.GetAllStudentRegistrationsAync(matricNumber);
            return StatusCode(response.StatusCore, response);
        }

        [HttpPost("StudentBySession")]
        public async Task<IActionResult> GetStudentRegistrationsBySession([FromBody] RegistrationsDto model)
        {
            var response = await _manager.GetStudentRegistrationsBySessionIdAync(model);
            return StatusCode(response.StatusCore, response);
        }

        [HttpPost("StudentBySemester")]
        public async Task<IActionResult> GetStudentRegistrationsBySemester([FromBody] RegistrationsDto model)
        {
            var response = await _manager.GetStudentRegistrationsBySemesterIdAync(model);
            return StatusCode(response.StatusCore, response);
        }

        [HttpGet("DepartmentBySession/{sessionId}")]
        public async Task<IActionResult> GetDepartmentRegistrationsBySession(string sessionId)
        {
            var response = await _manager.GetDepartmentRegistrationsBySessionIdAsync(sessionId);
            return StatusCode(response.StatusCore, response);
        }

        [HttpGet("DepartmentBySemester/{sessionId}")]
        public async Task<IActionResult> GetDepartmentRegistrationsBySemester(string sessionId)
        {
            var response = await _manager.GetDepartmentRegistrationsBySemesterIdAsync(sessionId);
            return StatusCode(response.StatusCore, response);
        }

        // ==========================
        // BUSINESS RULES APIs
        // ==========================

        [HttpPost("Rules/Create")]
        public async Task<IActionResult> CreateRegistrationRule([FromBody] RegistrationBusinessRulesDto model)
        {
            var response = await _manager.CreateRegistrationBusinessRuleAsync(model);
            return StatusCode(response.StatusCore, response);
        }

        [HttpPost("Rules/CreateBulk")]
        public async Task<IActionResult> CreateBulkRegistrationRules([FromBody] List<RegistrationBusinessRulesDto> models)
        {
            var response = await _manager.CreateRegistrationBusinessRuleAsync(models);
            return StatusCode(response.StatusCore, response);
        }

        [HttpPost("Rules/Upload")]
        public async Task<IActionResult> UploadRegistrationRules([FromBody] byte[] fileBytes)
        {
            var response = await _manager.UploadRegistrationBusinessRuleAsync(fileBytes);
            return StatusCode(response.StatusCore, response);
        }

        [HttpPut("Rules/Update/{id}")]
        public async Task<IActionResult> UpdateRegistrationRule(int id, [FromBody] RegistrationBusinessRulesDto model)
        {
            var response = await _manager.UpdateRegistrationBusinessRuleAsync(id, model);
            return StatusCode(response.StatusCore, response);
        }

        [HttpDelete("Rules/Delete/{id}")]
        public async Task<IActionResult> DeleteRegistrationRule(int id)
        {
            var response = await _manager.DeleteRegistrationBusinessRuleAsync(id);
            return StatusCode(response.StatusCore, response);
        }

        [HttpGet("Rules/All")]
        public async Task<IActionResult> GetAllRegistrationRules()
        {
            var response = await _manager.GetAllRegistrationBusinessRulesAsync();
            return StatusCode(response.StatusCore, response);
        }

        [HttpPost("Rules/ByDepartment/{departmentCode}")]
        public async Task<IActionResult> GetRulesByDepartment(string departmentCode, [FromBody] RegistrationBusinessRulesDto model)
        {
            var response = await _manager.GetRegistrationBusinessRulesByDepartmentAsync(departmentCode, model);
            return StatusCode(response.StatusCore, response);
        }

        [HttpPost("Rules/Validate")]
        public async Task<IActionResult> ValidateRegistrationRule([FromBody] RegistrationBusinessRulesDto model)
        {
            var response = await _manager.ValidateStudentRegistrationAsync(model);
            return StatusCode(response.StatusCore, response);
        }
    }
}
