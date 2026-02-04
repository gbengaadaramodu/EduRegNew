using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EduReg.Models.Entities
{
    [Index(nameof(InstitutionShortName))]
    public class Programmes : CommonBase
    {
        public string? InstitutionShortName { get; set; } //FK
        public string? DepartmentCode { get; set; } //FK
        public string? ProgrammeCode { get; set; } 
        public string? ProgrammeName { get; set; }
        public string? Description { get; set; }
        public int Duration { get; set; }
        public int NumberOfSemesters { get; set; }
        public int MaximumNumberOfSemesters { get; set; }
    }
}
