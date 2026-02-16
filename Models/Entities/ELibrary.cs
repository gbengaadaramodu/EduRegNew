using Microsoft.EntityFrameworkCore;

namespace EduReg.Models.Entities
{
    [Index(nameof(InstitutionShortName))]
    public class ELibrary : CommonBase
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public string Category { get; set; }
        public string? CourseCode { get; set; }
        public int? ProgramId { get; set; }
        public string FileUrl { get; set; }      // Full URL from FileUploadService
        public string FileName { get; set; }
        public string FileType { get; set; }     // MIME type
        public long FileSizeBytes { get; set; }
        public string InstitutionShortName { get; set; }
    }
}