using EduReg.Common;
using EduReg.Common.Attributes;
using EduReg.Managers;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EduReg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [RequireInstitutionShortName]
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
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("CreateBulk")]
        public async Task<IActionResult> CreateBulkStudentRegistration([FromBody] List<RegistrationsDto> models)
        {
            var response = await _manager.CreateStudentRegistrationAsync(models);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> UploadStudentRegistration([FromBody] byte[] fileBytes)
        {
            var response = await _manager.CreateStudentRegistrationAsync(fileBytes);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("Drop/{id}")]
        public async Task<IActionResult> DropStudentRegistration(int id)
        {
            var response = await _manager.DropStudentRegistrationAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("Student/{matricNumber}")]
        public async Task<IActionResult> GetStudentRegistrations(string matricNumber)
        {
            var response = await _manager.GetAllStudentRegistrationsAync(matricNumber);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("StudentBySession")]
        public async Task<IActionResult> GetStudentRegistrationsBySession([FromBody] RegistrationsDto model)
        {
            var response = await _manager.GetStudentRegistrationsBySessionIdAync(model);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("StudentBySemester")]
        public async Task<IActionResult> GetStudentRegistrationsBySemester([FromBody] RegistrationsDto model)
        {
            var response = await _manager.GetStudentRegistrationsBySemesterIdAync(model);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("DepartmentBySession/{sessionId}")]
        public async Task<IActionResult> GetDepartmentRegistrationsBySession(string sessionId, [FromQuery] RegistrationFilter filter, [FromQuery] PagingParameters paging)
        {
            var response = await _manager.GetDepartmentRegistrationsBySessionIdAsync(sessionId, filter ,paging);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("DepartmentBySemester/{sessionId}")]
        public async Task<IActionResult> GetDepartmentRegistrationsBySemester(string sessionId, [FromQuery]RegistrationFilter filter, [FromQuery] PagingParameters paging)
        {
            var response = await _manager.GetDepartmentRegistrationsBySemesterIdAsync(sessionId,filter, paging);
            return StatusCode(response.StatusCode, response);
        }

        // ==========================
        // BUSINESS RULES APIs
        // ==========================

        [HttpPost("Rules/Create")]
        public async Task<IActionResult> CreateRegistrationRule([FromBody] RegistrationBusinessRulesDto model)
        {
            var response = await _manager.CreateRegistrationBusinessRuleAsync(model);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("Rules/CreateBulk")]
        public async Task<IActionResult> CreateBulkRegistrationRules([FromBody] List<RegistrationBusinessRulesDto> models)
        {
            var response = await _manager.CreateRegistrationBusinessRuleAsync(models);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("Rules/Upload")]
        public async Task<IActionResult> UploadRegistrationRules([FromBody] byte[] fileBytes)
        {
            var response = await _manager.UploadRegistrationBusinessRuleAsync(fileBytes);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("Rules/Update/{id}")]
        public async Task<IActionResult> UpdateRegistrationRule(int id, [FromBody] RegistrationBusinessRulesDto model)
        {
            var response = await _manager.UpdateRegistrationBusinessRuleAsync(id, model);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("Rules/Delete/{id}")]
        public async Task<IActionResult> DeleteRegistrationRule(int id)
        {
            var response = await _manager.DeleteRegistrationBusinessRuleAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("Rules/All")]
        public async Task<IActionResult> GetAllRegistrationRules([FromQuery] RegistrationBusinessRuleFilter filter, [FromQuery] PagingParameters paging)
        {
            var response = await _manager.GetAllRegistrationBusinessRulesAsync(filter, paging);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("Rules/ByDepartment/{departmentCode}")]
        public async Task<IActionResult> GetRulesByDepartment(string departmentCode, [FromBody] RegistrationBusinessRulesDto model)
        {
            var response = await _manager.GetRegistrationBusinessRulesByDepartmentAsync(departmentCode, model);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("Rules/Validate")]
        public async Task<IActionResult> ValidateRegistrationRule([FromBody] RegistrationBusinessRulesDto model)
        {
            var response = await _manager.ValidateStudentRegistrationAsync(model);
            return StatusCode(response.StatusCode, response);
        }
    }
}
