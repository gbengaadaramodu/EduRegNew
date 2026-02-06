namespace EduReg.Models.Dto.Request
{
    public class CourseMaxMinFilter
    {
        // Search keyword (searches CourseType or ProgramId)
        public string? Search { get; set; }

        // Specific filters
        public int? ProgramId { get; set; }
        public int? LevelId { get; set; }
        public int? SemesterId { get; set; }
        public string? CourseType { get; set; }
    }
}
