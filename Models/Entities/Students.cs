using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EduReg.Models.Entities
{
    [Index(nameof(InstitutionShortName))]
    public class Students : IdentityUser
    {
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? MatricNumber { get; set; }
        public string? ApplicantId { get; set; }
        public string? InstitutionShortName { get; set; } 
        public string? BatchShortName { get; set; } //PK
        public string? DepartmentCode { get; set; } //FK ->Department
        public string? ProgrammeCode { get; set; }     
       
        public int AdmittedSessionId { get; set; } //FK -> Academic   SessionId  
        public int AdmittedLevelId { get; set; }
        public int CurrentLevel { get; set; } //FK -> Academic   Level
        public int? CurrentSessionId { get; set; }
        public int? CurrentSemesterId { get; set; }
        public bool? isAdmin { get; set; }
        public DateTime? CreatedDate { get;  set; }
        public List<string>? Roles { get; set; }

        //public bool? IsAdminUser { get; set; }
        //public bool? AdminUserHasChangePassword { get; set; }
        //public string? SelectedCategory { get; set; }
        //public bool? IsActive { get; set; } = true;
        //public bool? IsLock { get; set; }
        //public int ApplicationBatchId { get; set; }
        //public int ProgramTypeId { get; set; }
    }
    //public class Students  : CommonBase
    //{
    //    public string MatricNumber { get; set; }
    //    public string? LastName { get; set; }
    //    public string FirstName { get; set; }
    //    public string? MiddleName { get; set; }
    //    public string Email { get; set; }
    //    public string PhoneNumber { get; set; }
    //   // public string? SelectedCategory { get; set; }
    //    public bool? IsActive { get; set; } = true;
    //    public bool? IsLock { get; set; }
    //    public int ApplicationBatchId { get; set; }
    //    public int ProgramTypeId { get; set; }
    //    public int CurrentAcademicSessionId { get; set; }
    //    public int AdmittedSessionId { get; set; }
    //    public int AdmittedLevelId { get; set; }
    //    public  int AcademicLevelId { get; set; }
    //    public int ProgramId { get; set; }
    //    public string ApplicantId { get; set; }
    //    public string Password { get; set; }
    //}
}
