using EduReg.Common;

namespace EduReg.Models.Dto.Request
{
    public class FeeRuleFilter
    {
        public string? InstitutionShortName { get; set; }

        public long? FeeItemId { get; set; }

        public string? ProgrammeCode { get; set; }
        public string? DepartmentCode { get; set; }
        public string? LevelName { get; set; }
        public string? ClassCode { get; set; }

        public string? SessionId { get; set; }
        public string? SemesterId { get; set; }

        public FeeRecurrenceType? RecurrenceType { get; set; }
        public FeeApplicabilityScope? ApplicabilityScope { get; set; }

        public bool? IsRecurring { get; set; }

        // Date-based rule validity 
        public DateTime? EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }

        // Generic search
        public string? Search { get; set; }
    }

}
