namespace EduReg.Models.Entities
{
    public class SessionSemester : CommonBase
    {
           

            // Multi-tenant Reference
            public string? InstitutionShortName { get; set; }
            public int SessionId { get; set; }   // FK from AcademicSession
            public int SemesterId { get; set; }  // FK from Semester
            public bool IsActive { get; set; }   // Your "Active Status"
            public DateTime CreatedDate { get; set; } = DateTime.Now;

            // Registration Window
            public DateTime RegistrationStartDate { get; set; }
            public DateTime RegistrationCloseDate { get; set; }

            // Examination Window
            public DateTime ExamStartDate { get; set; }
            public DateTime ExamEndDate { get; set; }

            // Optional: Useful for Soft Deletes
            public bool IsDeleted { get; set; }
        }


    
}
