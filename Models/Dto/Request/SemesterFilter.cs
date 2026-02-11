namespace EduReg.Models.Dto.Request
{
    public class SemesterFilter
    {
        public string? InstitutionShortName { get; set; }
        public int? SessionId { get; set; }

        public long? SemesterId { get; set; }
        public string? SemesterName { get; set; }

        // Date range filters (valid here)
        public DateTime? StartDateFrom { get; set; }
        public DateTime? StartDateTo { get; set; }

        // Generic search
        public string? Search { get; set; }
    }

}
