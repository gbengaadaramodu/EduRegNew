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
        public long CourseScheduleId { get; set; }
        public CourseSchedule CourseSchedule { get; set; }
    }


    public class CourseRegistrationDetail : CommonBase
    {
        public long CourseRegistrationId { get; set; }
        public CourseRegistration CourseRegistration { get; set; }
        public double? CA { get; set; }
        public double? ExamScore { get; set; }
        public bool HasRegisteredForExam { get; set; }
        public DateTime? ExamRegistrationDate { get; set; }
        public string? ExamStatus { get; set; } //PENDING, FAILED, PASSED
        public bool IsCarryOver { get; set; }
    }
}
