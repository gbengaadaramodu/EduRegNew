namespace EduReg.Models.Dto.Request
{
    public class CourseScheduleFilter
    {
        public string? InstitutionShortName { get; set; }
        public string? DepartmentCode { get; set; }
        public string? ProgrammeCode { get; set; }
        public int? SessionId { get; set; }
        public int? SemesterId { get; set; }
        public string? CourseCode { get; set; }
        public string? ClassCode { get; set; }
        public string? LevelName { get; set; }
        public string? CourseType { get; set; }
        public string? BatchShortName { get; set; }
        public bool? IsOnLMS { get; set; }

        //  Numeric Filters 
        public int? Units { get; set; }
        public decimal? MinCourseFee { get; set; }
        public decimal? MaxCourseFee { get; set; }

        // Search 
        // Applies to: CourseCode, Title, ProgrammeCode, DepartmentCode, Prerequisite
        public string? Search { get; set; }
    }
}
