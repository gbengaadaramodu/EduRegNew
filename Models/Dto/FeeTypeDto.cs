using System.ComponentModel.DataAnnotations;

namespace EduReg.Models.Dto
{
    public class FeeTypeDto : CommonBaseDto
    {
        [Required]
        [StringLength(100)]
        public string InstitutionShortName { get; set; } = null!;

        [Required]
        [StringLength(150)]
        public string Name { get; set; } = null!;

        [StringLength(250)]
        public string? Description { get; set; }

        public bool IsSystemDefined { get; set; } = false;
    }
}