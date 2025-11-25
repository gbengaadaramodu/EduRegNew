using EduReg.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EduReg.Models.Entities
{
    [Index(nameof(InstitutionShortName))]
    [Index(nameof(Name), nameof(InstitutionShortName), IsUnique = true)] // ✅ Prevent duplicates per institution
    public class FeeItem : CommonBase
    {
        [Required]
        [StringLength(100)]
        public string InstitutionShortName { get; set; } = null!; // Tenant key (FK -> Institution)

        //[StringLength(50)]
        //public string? DepartmentCode { get; set; } // Optional: FK -> Department

        //[StringLength(50)]
        //public string? ProgrammeCode { get; set; } // Optional: FK -> Programme

        [Required]
        [StringLength(150)]
        public string Name { get; set; } = null!; // e.g., "Acceptance Fee", "Tuition Fee"

        public FeeCategory FeeCategory { get; set; }

        [StringLength(250)]
        public string? Description { get; set; }
       
        [Precision(18, 2)]
        public decimal Amount { get; set; }

        // ✅ Important: Define how often the fee is expected to recur
        public FeeRecurrenceType FeeRecurrenceType { get; set; } = FeeRecurrenceType.OneTime;

        // ✅ Important: Define which academic segment the fee applies to
        public FeeApplicabilityScope FeeApplicabilityScope { get; set; } = FeeApplicabilityScope.InstitutionWide;

        // ✅ For logical protection of system-defined fees
        public bool IsSystemDefined { get; set; } = false;

        // Navigation Property
        public ICollection<FeeRule>? FeeRules { get; set; }
    }
}
