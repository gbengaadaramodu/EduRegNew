using EduReg.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace EduReg.Models.Dto
{
    public class FeeItemDto:CommonBaseDto
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
