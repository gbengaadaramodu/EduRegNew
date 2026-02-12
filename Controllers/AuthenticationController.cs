using EduReg.Common.Attributes;
using EduReg.Managers;
using EduReg.Models.Dto;
using EduReg.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduReg.Controllers
{
    [Route("api/[controller]")]
    [RequireInstitutionShortName]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationManager _manager;

        public AuthenticationController(AuthenticationManager manager)
        {
            _manager = manager;
        }



        [HttpPost]
        [Route("CreateRoleAsync")]
        public async Task<IActionResult> CreateRoleAsync([FromBody] RoleName model)
        {
            var response = await _manager.CreateRoleAsync(model);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("AssignRoleToUserAsync")]
        public async Task<IActionResult> AssignRoleToUserAsync([FromBody] UserRole user)
        {
            var response = await _manager.AssignRoleToUserAsync(user);
            return StatusCode(response.StatusCode, response);
        }


        [HttpPost]
        [Route("EditRoleAsync")]
        public async Task<IActionResult> EditRoleAsync(string roleId, RoleName model)
        {
            var response = await _manager.EditRoleAsync(roleId, model);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("GetAllRolesAsync")]
        public async Task<IActionResult> GetAllRolesAsync()
        {
            var response = await _manager.GetAllRolesAsync();
            return StatusCode(response.StatusCode, response);
        }


        [HttpPost]
        [Route("CreateUpdatePermission")]
        public async Task<IActionResult> CreateUpdatePermission([FromBody] PermissionModel model)
        {
            var response = await _manager.CreateUpdatePermission(model);
            return StatusCode(response.StatusCode, response);
        }


        [HttpGet]
        [Route("GetAllPermission")]
        public async Task<IActionResult> GetAllPermission()
        {
            var response = await _manager.GetAllPermission();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("GetAllPermissionById")]
        public async Task<IActionResult> GetAllPermissionById(int Id)
        {
            var response = await _manager.GetPermissionById(Id);
            return StatusCode(response.StatusCode, response);
        }


        [HttpPost]
        [Route("CreateUpdatePrivillege")]
        public async Task<IActionResult> CreateUpdatePrivillege([FromBody] PrivilegeModel model)
        {
            var response = await _manager.CreateUpdatePrivillege(model);
            return StatusCode(response.StatusCode, response);
        }


        [HttpGet]
        [Route("GetAllPrivileges")]
        public async Task<IActionResult> GetAllPrivileges()
        {
            var response = await _manager.GetAllPrivileges();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("GetAllPrivilegesById")]
        public async Task<IActionResult> GetAllPrivileges(int Id)
        {
            var response = await _manager.GetPrivilegeById(Id);
            return StatusCode(response.StatusCode, response);
        }



        [HttpPost]
        [Route("RegisterAdminAsync")]
        public async Task<IActionResult> RegisterAdminAsync([FromBody] RegisterAdminRequests model)
        {
            var response = await _manager.RegisterAdminAsync(model);
            return StatusCode(response.StatusCode, response);
        }


        [HttpPost]
        [Route("LoginAdminAsync")]
        public async Task<IActionResult> LoginAdminAsync([FromBody] LoginAdminRequest model)
        {
            var response = await _manager.LoginAdminAsync(model);
            return StatusCode(response.StatusCode, response);
        }

            
        [HttpPost]
        [Route("UpdateAdminAsync")]
        public async Task<IActionResult> UpdateAdminAsync(string email, [FromBody] RegisterAdminRequests model)
        {
            var response = await _manager.UpdateAdminAsync(email, model);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("GetAllAdminAsync")]
        public async Task<IActionResult> GetAllAdminAsync()
        {
            var response = await _manager.GetAllAdminAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("GetAdminByEmailAsync")]
        public async Task<IActionResult> GetAdminByEmailAsync(string email)
        {
            var response = await _manager.GetAdminByEmailAsync(email);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete]
        [Route("GetAdminByEmailAsync")]
        public async Task<IActionResult> DeleteAdminAsync(string email)
        {
            var response = await _manager.DeleteAdminAsync(email);
            return StatusCode(response.StatusCode, response);
        }


        [HttpPost]
        [Route("LoginUserAsync")]
        public async Task<IActionResult> LoginUserAsync([FromBody] StudentLoginRequest model)
        {
            var response = await _manager.LoginUserAsync(model);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("ResetPasswordAsync")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordRequestDto model)
        {
            var response = await _manager.ResetPasswordAsync(model);
            return StatusCode(response.StatusCode, response);
        }


        [HttpPost]
        [Route("ConfirmResetPasswordAsync")]
        public async Task<IActionResult> ConfirmResetPasswordAsync([FromBody] ConfirmResetPasswordRequestDto model)
        {
            var response = await _manager.ConfirmResetPasswordAsync(model);
            return StatusCode(response.StatusCode, response);

        }

        [HttpPost]
        [Route("ChangePasswordAsync")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordRequest model)
        {
            var response = await _manager.ChangePasswordAsync(model);
            return StatusCode(response.StatusCode, response);
        } 
        
        
        [HttpPost]
        [Route("CreateApplicant")]
        public async Task<IActionResult> CreateApplicant([FromBody] MoveStudentDto model)
        {
            var response = await _manager.CreateApplicant(model);
            return StatusCode(response.StatusCode, response);
        }
    }
}
