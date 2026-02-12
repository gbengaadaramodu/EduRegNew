using EduReg.Common;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace EduReg.Managers
{
    public class AuthenticationManager : IAuthentication
    {
        private readonly IAuthentication _auth;

        public AuthenticationManager(IAuthentication auth)
        {
            _auth = auth;
        }

        public Task<GeneralResponse> AssignRoleToUserAsync(UserRole user)
        {
            return  _auth.AssignRoleToUserAsync(user);
        }

        public Task<GeneralResponse> CreateRoleAsync(RoleName model)
        {
            return _auth.CreateRoleAsync(model); ;
        }

        public Task<GeneralResponse> CreateUpdatePermission(PermissionModel model)
        {
            return _auth.CreateUpdatePermission(model);
        }

        public Task<GeneralResponse> CreateUpdatePrivillege(PrivilegeModel model)
        {
            return _auth.CreateUpdatePrivillege(model);
        }
        public Task<GeneralResponse> EditRoleAsync(string roleId, RoleName model)
        {
            return _auth.EditRoleAsync(roleId, model);
        }

        public Task<GeneralResponse> GetAllPermission()
        {
            return _auth.GetAllPermission();
        }

        public Task<GeneralResponse> GetAllPrivileges()
        {
            return _auth.GetAllPrivileges();
        }

        public Task<GeneralResponse> GetAllRolesAsync()
        {
            return _auth.GetAllRolesAsync();
        }

        public Task<GeneralResponse> GetPermissionById(int Id)
        {
            return _auth.GetPermissionById(Id);
        }

        public Task<GeneralResponse> GetPrivilegeById(int Id)
        {
            return _auth.GetPrivilegeById(Id);
        }

        public Task<GeneralResponse> RegisterAdminAsync(RegisterAdminRequests model)
        {
            return  _auth.RegisterAdminAsync(model);
        }

        public Task<GeneralResponse> UpdateAdminAsync(string email, RegisterAdminRequests model)
        {
            return _auth.UpdateAdminAsync(email, model);
        }

        public Task<GeneralResponse> GetAdminByEmailAsync(string email)
        {
            return _auth.GetAdminByEmailAsync(email);
        }

        public Task<GeneralResponse> GetAllAdminAsync()
        {
            return _auth.GetAllAdminAsync();
        }

        public Task<GeneralResponse> DeleteAdminAsync(string Email)
        {
            return _auth.DeleteAdminAsync(Email);
        } 
        
        public Task<GeneralResponse> LoginAdminAsync(LoginAdminRequest model)
        {
            return _auth.LoginAdminAsync(model);
        } 
        
        public Task<GeneralResponse> LoginUserAsync(StudentLoginRequest model)
        {
            return _auth.LoginUserAsync(model);
        }


        public Task<GeneralResponse> ResetPasswordAsync(ResetPasswordRequestDto model)
        {
            return _auth.ResetPasswordAsync(model);
        }

        public Task<GeneralResponse> ConfirmResetPasswordAsync(ConfirmResetPasswordRequestDto model)
        {
            return _auth.ConfirmResetPasswordAsync(model);
        }

        public Task<GeneralResponse> ChangePasswordAsync(ChangePasswordRequest model)
        {
            return _auth.ChangePasswordAsync(model);
        }  
        
        public Task<GeneralResponse> CreateApplicant(MoveStudentDto model)
        {
            return _auth.CreateApplicant(model);
        }
    }
}
