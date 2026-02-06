namespace EduReg.Models.Dto.Request
{
    public class ProgramCourseFilter
    {
        public string? InstitutionShortName { get; set; }

        public string? DepartmentCode { get; set; }
        public string? ProgrammeCode { get; set; }

        public string? CourseCode { get; set; }
        public string? ClassCode { get; set; }
        public string? LevelName { get; set; }

        public string? CourseType { get; set; } // Core, Elective, Mandatory

        public int? MinUnits { get; set; }
        public int? MaxUnits { get; set; }

        // Generic search
        public string? Search { get; set; }
    }

}
