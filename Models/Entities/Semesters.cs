using Microsoft.EntityFrameworkCore;

namespace EduReg.Models.Entities
{
    [Index(nameof(InstitutionShortName))]
    public class Semesters : CommonBase 
    {
        public string? InstitutionShortName { get; set; } //Pk -> Institution

        public string? SessionName { get; set; } //FK Academic Session
        public int SessionId { get; set; } //FK Academic Session
        public string? SemesterName { get; set; }// First, Second, Third
        public int SemesterId { get; set; } // 1, 2, 3
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
       
    }
}
