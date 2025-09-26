using EduReg.Common;
using System.ComponentModel.DataAnnotations;

namespace EduReg.Models.Entities
{
    public class FeeItem: CommonBase
    {        

        [Required]
        public string? Name { get; set; }   // e.g., "Admission Acceptance Fee"

        public string? Description { get; set; }

        // Optional: Indicates whether this is system-critical (cannot be deleted)
        public bool IsSystemDefined { get; set; } = false;

        public string? InstitutionShortName { get; set; } 

        // Navigation
        public ICollection<FeeRule> FeeRules { get; set; }
    }
}
