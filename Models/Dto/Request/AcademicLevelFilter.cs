namespace EduReg.Models.Dto.Request
{
    public class AcademicLevelFilter
    {
        // Filters
        public string? InstitutionShortName { get; set; }
        public string? ProgrammeCode { get; set; }
        public string? ClassCode { get; set; }
        public string? LevelName { get; set; }
        public int? LevelId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        // Ordering 
        public int? Order { get; set; }

        // Search
        // Applies to: ClassCode, LevelName, ProgrammeCode, Description, CourseAdviser
        public string? Search { get; set; }
    }
}
