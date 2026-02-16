using Microsoft.AspNetCore.Http;

namespace EduReg.Models.Dto
{
    public class CreateELibraryDto : CommonBaseDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public string Category { get; set; }
        public string? CourseCode { get; set; }
        public int? ProgramId { get; set; }
        public IFormFile File { get; set; }  // The actual file
        public string InstitutionShortName { get; set; }
    }
}