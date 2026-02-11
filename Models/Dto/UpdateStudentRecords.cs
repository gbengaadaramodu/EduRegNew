namespace EduReg.Models.Dto
{
    public class UpdateStudentRecordsDto : CommonBaseDto
    {
        // Personal Information
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }

        // Contact 
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        // Academic 
        public string? InstitutionShortName { get; set; }
        public string? BatchShortName { get; set; }
        public string? DepartmentCode { get; set; }
        public string? ProgrammeCode { get; set; } // ← The matric trigger

        // Progression
        public int AdmittedSessionId { get; set; }
        public int AdmittedLevelId { get; set; }
        public int CurrentLevel { get; set; }
        public int? CurrentSessionId { get; set; }
        public int? CurrentSemesterId { get; set; }
        public string? ApplicantId { get; set; }
    }
}