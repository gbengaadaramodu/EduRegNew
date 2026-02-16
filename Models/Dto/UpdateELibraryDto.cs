using Microsoft.AspNetCore.Http;

namespace EduReg.Models.Dto
{
    public class UpdateELibraryDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public string? Category { get; set; }
        public string? CourseCode { get; set; }
        public int? ProgramId { get; set; }
        public IFormFile? File { get; set; }  // Optional: only if replacing file
    }
}