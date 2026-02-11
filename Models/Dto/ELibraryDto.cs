using System.ComponentModel.DataAnnotations;

namespace EduReg.Models.Dto
{
    public class ELibraryDto : CommonBaseDto
    {
        [Required]
        public string CourseCode { get; set; } = null!;

        [Required]
        public long ProgramId { get; set; }

        [Required]
        public string FilePath { get; set; } = null!;

        public string Title { get; set; } = string.Empty;

        [Required]
        public string InstitutionShortName { get; set; } = null!;
    }
}