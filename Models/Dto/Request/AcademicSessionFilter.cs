namespace EduReg.Models.Dto.Request
{
    public class AcademicSessionFilter
    {
        public string? InstitutionShortName { get; set; }
        public string? BatchShortName { get; set; }
        public int? SessionId { get; set; }
        public bool? IsDeleted { get; set; }

        // Date Range Filters
        public DateTime? StartDateFrom { get; set; }
        public DateTime? StartDateTo { get; set; }

        public DateTime? EndDateFrom { get; set; }
        public DateTime? EndDateTo { get; set; }

        // Search 
        // Applies to: SessionName, InstitutionShortName, BatchShortName
        public string? Search { get; set; }
    }
}
