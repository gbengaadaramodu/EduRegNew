using System.ComponentModel.DataAnnotations;

namespace EduReg.Models.Dto
{
    public class DepartmentCoursesDto : CommonBaseDto
    {
        //id
        public string? InstitutionShortName { get; set; } //Pk -> Institution
        public string? DepartmentCode { get; set; } //FK -> Departments
        public string? CourseCode { get; set; }
        public string? Title { get; set; }
        public int Units { get; set; }
        public string? CourseType { get; set; }
        public string? Prerequisite { get; set; } //FK -> CourseCode

        [MaxLength(8000)]
        public string? Description { get; set; } //FK -> Departments

        //ActiveStatus = 1, 2, InactiveStatus = 0, DeletedStatus = -1
    }
}
