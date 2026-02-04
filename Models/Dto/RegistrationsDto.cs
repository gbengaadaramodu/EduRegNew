using EduReg.Models.Entities;

namespace EduReg.Models.Dto
{
    public class RegistrationsDto : CommonBaseDto
    {
        public string? MatricNumber { get; set; }
        public string? SessionId { get; set; }
        public string? DepartmentId { get; set; }
        public string? SemesterId { get; set; }
        public string? ClassCode { get; set; }
        public string? CourseCode { get; set; }
        public string? LevelName { get; set; }
        public string? CourseTitle { get; set; }
        public int Units { get; set; }
        public DateTime RegisteredOn { get; set; }
        public decimal CourseFee { get; set; } = 0;
        public bool IsApproved { get; set; } = false;
        public bool IsPaid { get; set; } = false;
        public bool IsCompleted { get; set; } = false;
        public double RawScore { get; set; }
        public string? Grade { get; set; }
        public string? ApprovedBy { get; set; }
        public string? ApprovedDate { get; set; }
        public int LMSId { get; set; } = -1;
        public bool IsEnrolledOnLMS { get; set; } = false;

        // CHM302, Advanced Chemistry, 3, 2021/2022, 2nd Semester, 200 Level, SCI/CHM/2002, 45, B, true, true, true, Dr. John Doe, 2023-05-15
    }

}
