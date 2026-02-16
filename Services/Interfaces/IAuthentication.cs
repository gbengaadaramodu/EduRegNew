using EduReg.Common;
using EduReg.Models.Dto;
using EduReg.Models.Entities;

namespace EduReg.Services.Interfaces
{
    public interface IAuthentication
    {
        Task<GeneralResponse> CreateRoleAsync(RoleName model);
        Task<GeneralResponse> EditRoleAsync(string roleId, RoleName model);
        Task<GeneralResponse> GetAllRolesAsync();
        Task<GeneralResponse> AssignRoleToUserAsync(UserRole user);
        Task<GeneralResponse> CreateUpdatePermission(PermissionModel model);
        Task<GeneralResponse> GetAllPermission();
        Task<GeneralResponse> GetPermissionById(int Id);
        Task<GeneralResponse> CreateUpdatePrivillege(PrivilegeModel model);
        Task<GeneralResponse> GetAllPrivileges();
        Task<GeneralResponse> GetPrivilegeById(int Id);
        Task<GeneralResponse> RegisterAdminAsync(RegisterAdminRequests model);
        Task<GeneralResponse> GetAllAdminAsync();
        Task<GeneralResponse> GetAdminByEmailAsync(string email);
        Task<GeneralResponse> UpdateAdminAsync(string email, RegisterAdminRequests model);
        Task<GeneralResponse> DeleteAdminAsync(string Email);
        Task<GeneralResponse> LoginAdminAsync(LoginAdminRequest model);
        Task<GeneralResponse> LoginUserAsync(StudentLoginRequest model);
        Task<GeneralResponse> ResetPasswordAsync(ResetPasswordRequestDto model);
        Task<GeneralResponse> ConfirmResetPasswordAsync(ConfirmResetPasswordRequestDto model);
        Task<GeneralResponse> ChangePasswordAsync(ChangePasswordRequest model);
        Task<GeneralResponse> CreateApplicant(List<MoveStudentDto> model);

    }
}
