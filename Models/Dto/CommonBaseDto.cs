namespace EduReg.Models.Dto
{
    public class CommonBaseDto
    {
        public DateTime Created { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public bool ActiveStatus { get; set; }
    }
}
