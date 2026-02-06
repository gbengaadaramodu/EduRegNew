namespace EduReg.Models.Dto.Request
{
    public class CourseTypeFilter
    {
        // Search by Name (e.g., "Core", "Elective")
        public string? Search { get; set; }

        // Filter by Active/Inactive status
        public int? ActiveStatus { get; set; }
    }
}
