namespace EduReg.Models.Dto.Request
{
    public class RegistrationFilter
    {
        public string? InstitutionShortName { get; set; }

        public string? MatricNumber { get; set; }
        public string? ProgrammeCode { get; set; }
        public string? DepartmentId { get; set; }

        public string? SessionId { get; set; }
        public string? SemesterId { get; set; }

        public string? ClassCode { get; set; }
        public string? LevelName { get; set; }
        public string? CourseCode { get; set; }

        public bool? IsApproved { get; set; }
        public bool? IsPaid { get; set; }
        public bool? IsCompleted { get; set; }

        // Date-based filtering (valid here)
        public DateTime? RegisteredFrom { get; set; }
        public DateTime? RegisteredTo { get; set; }

        // Generic search
        public string? Search { get; set; }
    }

}
