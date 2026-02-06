namespace EduReg.Models.Dto
{
    public class RegisterAdminRequests
    {
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Password { get; set; }
        public string? ConfirmedPassword { get; set; }
        public List<string>? Roles { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
    }


    public class LoginAdminRequest
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }

    }

    public class StudentLoginRequest
    {
        public string? MatricNumber { get; set; }
        public string? Password { get; set; }

    }



    public class LoginToApplicationResponseDto
    {
        public string? UserName { get; set; }
        public string? UserId { get; set; }
        public string? Token { get; set; }
        public IList<string>? Roles { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    } 
    
    public class LoginUserResponseDto
    {
        public string? UserName { get; set; }
        public string? UserId { get; set; }
        public string? Token { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }


    public class ConfirmResetPasswordRequestDto
    {
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }


    public class ResetPasswordRequestDto
    {
        public string? Email { get; set; }
    }



    public class ChangePasswordRequest
    {
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? Email { get; set; }
    }

}
