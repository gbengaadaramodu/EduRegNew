using System.ComponentModel.DataAnnotations;

namespace EduReg.Models.Dto
{
    public class ProgrammesDto : CommonBaseDto
    {
        [Key]
        public string? DepartmentCode { get; set; } //FK
        public string? ProgrammeCode { get; set; }

        public string? InstitutionShortName { get; set; }
        public string? ProgrammeName { get; set; }
        public string? Description { get; set; }
        public int Duration { get; set; }
        public int NumberOfSemesters { get; set; }
        public int MaximumNumberOfSemesters { get; set; }

    }
}
