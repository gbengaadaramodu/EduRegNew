namespace EduReg.Models.Dto
{
    public class UpdateStudentRecordsResponse
    {
        // Personal Information
        public string? Id { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }

        // MatricNumber - ALWAYS returned, NEVER in request
        public string? MatricNumber { get; set; }

        // Contact 
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        // Academic Institution
        public string? InstitutionShortName { get; set; }
        public string? BatchShortName { get; set; }
        public string? DepartmentCode { get; set; }
        public string? ProgrammeCode { get; set; }
        public string? ProgrammeName { get; set; }

        // Admission Info (used for MatricNumber)
        public int AdmittedSessionId { get; set; }
        public string? AdmittedSessionName { get; set; }
        public int AdmittedLevelId { get; set; }
        public string? AdmittedLevelName { get; set; }

        // Current Progression
        public int CurrentLevel { get; set; }
        public int? CurrentSessionId { get; set; }
        public string? CurrentSessionName { get; set; }
        public int? CurrentSemesterId { get; set; }
        public string? CurrentSemesterName { get; set; }

        // Other
        public string? ApplicantId { get; set; }
    }
}