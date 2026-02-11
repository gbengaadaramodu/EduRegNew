using EduReg.Models.Entities;

namespace EduReg.Models.Dto
{
    public class CourseRegistrationViewDto
    {
        public string StudentsId { get; set; }
        public int SemesterId { get; set; }
        public int SessionId { get; set; }
        public string? DepartmentCode { get; set; } //FK ->Department
        public string? ProgrammeCode { get; set; }
        public string? Level { get; set; }
        public DateTime RegistrationDate { get; set; }
        public long Id { get; set; }
        public List<CourseRegistrationDetailViewDto>? CourseRegistrationDetails { get; set; }
    }


    public class CourseRegistrationDetailViewDto
    {
        public string? CourseCode { get; set; }
        public string? CourseTitle { get; set; }
        public int? CourseUnit { get; set; }
        public string? CourseCategory { get; set; }
        public decimal? CourseFee { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public long CourseScheduleId { get; set; }
        public long Id { get; set; }
        public bool IsCarryOver { get; set; }
    }
}
