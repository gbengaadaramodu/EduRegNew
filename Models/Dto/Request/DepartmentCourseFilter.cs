namespace EduReg.Models.Dto.Request
{
    public class DepartmentCourseFilter
    {
        public string? InstitutionShortName { get; set; }
        public string? DepartmentCode { get; set; }
        public string? CourseType { get; set; }   // Core, Elective, etc.
        public int? Units { get; set; }

        // Generic search
        public string? Search { get; set; }
    }

}
