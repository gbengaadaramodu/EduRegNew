using EduReg.Models.Entities;

namespace EduReg.Models.Dto
{
    public class AcademicSessionsDto: CommonBaseDto
    {
        public string? BatchShortName { get; set; } //FK from Admission Batches
        
        public string? SessionId { get; set; } // Unoque
        public string? SessionName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
