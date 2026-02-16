namespace EduReg.Models.Dto.Request
{
    public class ELibraryFilter
    {
        public string? InstitutionShortName { get; set; }
        public string? CourseCode { get; set; }
        public string? Category { get; set; }  // e.g., "Textbook", "Journal", "Reference"
        public int? ProgramId { get; set; }
        public string? Author { get; set; }

        // Search
        // Applies to: Title, Author, Description
        public string? Search { get; set; }
    }
}