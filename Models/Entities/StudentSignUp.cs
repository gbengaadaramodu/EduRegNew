using Microsoft.AspNetCore.Identity;

namespace EduReg.Models.Entities
{

    public class ApplicantSignUp : IdentityUser
    {
        public string? LastName { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        // public string Email { get; set; }
        //public string PhoneNumber { get; set; }
        public bool? IsAdminUser { get; set; }
        public bool? AdminUserHasChangePassword { get; set; }
        public string? SelectedCategory { get; set; }
        public bool? IsActive { get; set; } = true;
        public bool? IsLock { get; set; }
        public int ApplicationBatchId { get; set; }
        public int ProgramTypeId { get; set; }
    }
    public class StudentSignUp  : CommonBase
    {
        public string MatricNumber { get; set; }
        public string? LastName { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
       // public string? SelectedCategory { get; set; }
        public bool? IsActive { get; set; } = true;
        public bool? IsLock { get; set; }
        public int ApplicationBatchId { get; set; }
        public int ProgramTypeId { get; set; }
        public int CurrentAcademicSessionId { get; set; }
        public int AdmittedSessionId { get; set; }
        public int AdmittedLevelId { get; set; }
        public  int AcademicLevelId { get; set; }
        public int ProgramId { get; set; }
        public string ApplicantId { get; set; }
        public string Password { get; set; }
    }
}
