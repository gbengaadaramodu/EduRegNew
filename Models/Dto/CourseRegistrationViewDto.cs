namespace EduReg.Models.Dto
{
    public class CourseRegistrationViewDto
    {
        public string? CourseCode { get; set; }
        public string? CourseTitle {  get; set; }
        public int? CourseUnit { get; set; }
        public string? CourseCategory { get; set; }
        public decimal? CourseFee { get; set; }
        public DateTime RegistrationDate { get; set; }
        public long CourseScheduleId { get; set; }
        public long Id { get; set; }
    }
}
