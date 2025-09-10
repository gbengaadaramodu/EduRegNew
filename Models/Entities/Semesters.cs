using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduReg.Models.Entities
{
    public class Semester : CommonBase 
    {
        public int SessionId { get; set; } //FK Academic Session

        [ForeignKey(nameof(SessionId))]
        public AcademicSession Session { get; set; }
        public string? SemesterName { get; set; }// First, Second, Third
        
        [Key]
        public int SemesterId { get; set; } // 1, 2, 3
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
       
    }
}
