namespace EduReg.Models.Dto
{
    public class CommonBaseDto
    {
        public DateTime Created { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public int ActiveStatus { get; set; }
    }
}
