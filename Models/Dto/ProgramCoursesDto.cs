using System.ComponentModel.DataAnnotations;

namespace EduReg.Models.Dto
{
    public class ProgramCoursesDto : CommonBaseDto
    {
        public string? InstitutionShortName { get; set; } //Pk -> Institution
        public string? DepartmentCode { get; set; } //FK -> Departments
        public string? ProgrammeCode { get; set; }
        public string? CourseCode { get; set; }
        public string? ClassCode { get; set; }//1, 2, 100, 200, 300, 800, 900
        public string? LevelName { get; set; }// NCE, ND, HND, BSc, MSc, PhD
        public string? Title { get; set; }
        public int Units { get; set; } // Editable
        public string? CourseType { get; set; } // Core, Elective, Mandatory
        public string? Prerequisite { get; set; } //FK -> CourseCode

        [MaxLength(8000)]
        public string? Description { get; set; } //FK -> Departments
    }
}
