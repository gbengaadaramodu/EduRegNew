using EduReg.Common;
using Microsoft.EntityFrameworkCore;

namespace EduReg.Models.Entities
{
    [Index(nameof(InstitutionShortName))]
    public class FeeRule: CommonBase
    {
       
        public long FeeItemId { get; set; }
        public FeeItem? FeeItem { get; set; }
        public string? InstitutionShortName { get; set; }

        // Applicability (optional filters)
        public string? ProgrammeCode { get; set; }
        public string? DepartmentCode { get; set; }
        public string? LevelName { get; set; }
        public string? ClassCode { get; set; }

        public string? SessionId { get; set; }
        public string? SemesterId { get; set; }

        [Precision(18, 2)]
        public decimal Amount { get; set; }
        public bool IsRecurring { get; set; }
        public DateTime? EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }

        public FeeRecurrenceType RecurrenceType { get; set; }             // How often
        public FeeApplicabilityScope ApplicabilityScope { get; set; }     // Who it applies to
        public DateTime CreatedAt { get; internal set; }
        public DateTime UpdatedAt { get; internal set; }
    }
}
