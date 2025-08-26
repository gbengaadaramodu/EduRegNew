using EduReg.Models.Entities;

namespace EduReg.Models.Dto
{
    public class AdmissionBatchesDto: CommonBaseDto
    {
        public string? InstitutionShortName { get; set; }
        public string? BatchName { get; set; }
        public string? BatchShortName { get; set; } //PK
        public string? Description { get; set; }


    }
}
