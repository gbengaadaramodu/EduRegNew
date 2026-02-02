using EduReg.Common;

namespace EduReg.Models.Dto.Request
{
    public class FeeItemFilter
    {
        public string? InstitutionShortName { get; set; }

        public FeeCategory? FeeCategory { get; set; }
        public FeeRecurrenceType? FeeRecurrenceType { get; set; }
        public FeeApplicabilityScope? FeeApplicabilityScope { get; set; }

        public bool? IsSystemDefined { get; set; }

        // Amount range filtering (useful for finance dashboards)
        public decimal? MinAmount { get; set; }
        public decimal? MaxAmount { get; set; }

        // Generic text search
        public string? Search { get; set; }
    }

}
