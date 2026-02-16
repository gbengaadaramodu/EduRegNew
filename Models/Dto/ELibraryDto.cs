namespace EduReg.Models.Dto
{
    public class ELibraryDto : CommonBaseDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public string Category { get; set; }  // e.g., "Textbook", "Journal", "Reference"
        public string? CourseCode { get; set; }
        public int? ProgramId { get; set; }
        public string? FileUrl { get; set; }       // Full URL to download
        public string? FileName { get; set; }
        public string? FileType { get; set; }
        public long FileSizeBytes { get; set; }
        public string? InstitutionShortName { get; set; }
    }
}