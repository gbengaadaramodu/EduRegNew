using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EduReg.Models.Entities
{
    [Index(nameof(InstitutionShortName))]
    public class ProgrammeFeeSchedule : CommonBase
    {
        // Tenant Identifier
        public string? InstitutionShortName { get; set; } // FK to Institution

        // Optional contextual filters
        public string? DepartmentCode { get; set; }
        public string? ProgrammeCode { get; set; }
        public int? SessionId { get; set; }
        public int? SemesterId { get; set; }
        public string? CourseCode { get; set; }

        // FeeItem reference (core fee category)
        public int FeeItemId { get; set; }                     // FK to FeeItem
        public List<FeeItem>? FeeItem { get; set; }                  // Navigation property

        // Rule source (optional)
        public int? FeeRuleId { get; set; }                    // FK to FeeRule
        public FeeRule? FeeRule { get; set; }                  // Navigation property

        public decimal Amount { get; set; }
    }
}
