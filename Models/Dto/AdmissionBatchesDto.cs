using EduReg.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace EduReg.Models.Dto
{
    public class AdmissionBatchesDto: CommonBaseDto
    {
        public string? InstitutionShortName { get; set; }
        [Required(ErrorMessage = "BatchName is required")]
        public string? BatchName { get; set; }
        [Required(ErrorMessage = "BathShortName is required")]
        public string? BatchShortName { get; set; } //PK
        public string? Description { get; set; }


    }

    public class UpdateAdmissionBatchesDto: CommonBaseDto
    {
        public string? BatchName { get; set; }
        public string? Description { get; set; }
    }
}
