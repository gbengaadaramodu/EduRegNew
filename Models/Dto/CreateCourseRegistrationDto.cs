namespace EduReg.Models.Dto
{
    public class CreateCourseRegistrationDto
    {
        public string MatricNo { get; set; }
        public string? InstitutionShortName { get; set; }

        public List<long> CourseScheduleIds { get; set; }
    }

    public class CourseRegistrationRequestDto
    {
        public int? SessionId { get; set; }
        public int? SemesterId { get; set; }
        public string? InstitutionShortName { get; set; }
        public string? MatricNo { get; set; }

    }

    public class CoursesStudentCanRegisterRequestDto
    {
        public int SessionId { get; set; }
        public int SemesterId { get; set; }
        public string? InstitutionShortName { get; set; }
        public string MatricNo { get; set; }

    }
}
