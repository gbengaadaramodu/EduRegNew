using System.ComponentModel.DataAnnotations;

namespace EduReg.Models.Dto
{
    public class StudentStatusDto : CommonBaseDto
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string InstitutionShortName { get; set; } = null!;

        public bool IsActive { get; set; } = true;
    }
}