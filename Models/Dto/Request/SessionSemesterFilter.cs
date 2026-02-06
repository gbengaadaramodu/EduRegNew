namespace EduReg.Models.Dto.Request
{
    public class SessionSemesterFilter
    {
        // General search (can be used to search related Session names if joined)
        public string? Search { get; set; }

        // Specific FK Filters
        public int? SessionId { get; set; }
        public int? SemesterId { get; set; }

        // Status filter
        public bool? IsActive { get; set; }

        // Logic Filters
        public bool? IsRegistrationOpen { get; set; } 

        // Date Range Filters
        public DateTime? RegistrationAfter { get; set; }
        public DateTime? RegistrationBefore { get; set; }
    }
}
