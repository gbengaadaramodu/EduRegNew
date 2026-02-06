using Microsoft.EntityFrameworkCore;

namespace EduReg.Models.Dto
{
    [Index(nameof(InstitutionShortName))]
    public class SemestersDto: CommonBaseDto
    {
        public string? InstitutionShortName { get; set; } //FK Institution

        public string? SessionName { get; set; } //FK Academic Session
        public int SessionId { get; set; } //FK Academic Session
        public string? SemesterName { get; set; }
        public int SemesterId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
