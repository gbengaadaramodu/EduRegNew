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
        public string InstitutionShortName { get; set; }
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
        public string? InstitutionShortName { get; set; }
    } 
    
    public class LoginUserResponseDto
    {
        public string? UserName { get; set; }
        public string? UserId { get; set; }
        public string? Token { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? email { get; set; }
        public string? MatricNumber { get; set; }
        public string? InstitutionShortName { get; set; }
    }


    public class ConfirmResetPasswordRequestDto
    {
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string? MatricNumber { get; set; }
    }


    public class ResetPasswordRequestDto
    {
        public string? Email { get; set; }
        public string? MatricNumber { get; set; }
    }



    public class ChangePasswordRequest
    {
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? Email { get; set; }
        public string? MatricNumber { get; set; }
    }



    public class MoveStudentDto
    {
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        public string MatricNumber { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int? CountryId { get; set; }
        public int? StateId { get; set; }

        public int SessionId { get; set; }
        public int ApplicationBatchId { get; set; }

       // public int ProgramId { get; set; }
      //  public int ModeOfEntryId { get; set; }
       // public int ModeOfStudyId { get; set; }

        public string? ImageUrl { get; set; }

       // public int ProgramTypeId { get; set; }

        public string? ApplicationNumber { get; set; }

        public DateTime? AdmissionDate { get; set; }
        public DateTime? AcceptanceDate { get; set; }
        public string? Password { get; set; }
        public string ProgrammeCode { get; set; }
        public string BatchShortName { get; set; }
        public string DepartmentCode { get; set; }
        public string InstitutionShortName { get; set; }
    }

}
