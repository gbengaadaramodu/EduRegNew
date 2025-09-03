namespace EduReg.Models.Entities
{
    public class Semesters : CommonBase 
    {
        public int SessionId { get; set; } //FK Academic Session
        public string? SemesterName { get; set; }// First, Second, Third
        public int SemesterId { get; set; } // 1, 2, 3
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
       
    }
}
