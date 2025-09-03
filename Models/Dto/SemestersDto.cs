namespace EduReg.Models.Dto
{
    public class SemestersDto: CommonBaseDto
    {

        public int SessionId { get; set; } //FK Academic Session
        public string? SemesterName { get; set; }
        public int SemesterId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
