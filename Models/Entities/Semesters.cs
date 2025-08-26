namespace EduReg.Models.Entities
{
    public class Semesters : CommonBase 
    {
        public int SessionId { get; set; } //FK Academic Session
        public string? SemesterName { get; set; }
        public string? SemesterId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
       
    }
}
