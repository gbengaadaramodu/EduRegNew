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
        public int CourseScheduleId { get; set; }
        public int Id { get; set; }
    }
}
