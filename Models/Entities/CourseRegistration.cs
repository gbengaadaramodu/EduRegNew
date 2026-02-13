namespace EduReg.Models.Entities
{
    public class CourseRegistration: CommonBase
    {
        public string StudentsId { get; set; }
        public Students Students { get; set; }
        //public double? CA { get; set; }
        //public double? ExamScore { get; set; }
        //public bool HasRegisteredForExam { get; set; }
        //public DateTime? ExamRegistrationDate { get; set; }
        //public string? ExamStatus { get; set; } //PENDING, FAILED, PASSED
        //public bool IsCarryOver { get; set; }
        //public long CourseScheduleId { get; set; }
        //public CourseSchedule CourseSchedule { get; set; }
        public long SemesterId { get; set; }
        public long SessionId { get; set; }
        public string? DepartmentCode { get; set; } //FK ->Department
        public string? ProgrammeCode { get; set; }
        public string? ClassCode { get; set; }
        public string? Level { get; set; }
    }


    public class CourseRegistrationDetail : CommonBase
    {
        public long CourseRegistrationId { get; set; }
        public CourseRegistration CourseRegistration { get; set; }
        public double? CA { get; set; }
        public double? ExamScore { get; set; }
        public bool HasRegisteredForExam { get; set; }
        public DateTime? ExamRegistrationDate { get; set; }
        public DateTime? CourseRegistrationDate { get; set; }
        public string? ExamStatus { get; set; } //PENDING, FAILED, PASSED
        public bool IsCarryOver { get; set; }
        public long CourseScheduleId { get; set; }
        public CourseSchedule CourseSchedule { get; set; }
        public string StudentsId { get; set; }
        public Students Students { get; set; }
        public string? CourseCode { get; set; }
        public string? CourseTitle { get; set; }
    }
}
