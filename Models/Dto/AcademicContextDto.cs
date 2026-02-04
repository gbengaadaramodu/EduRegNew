namespace EduReg.Models.Dto
{
    public class AcademicContextDto
    {
        public string InstitutionShortName { get; set; } = null!; // FK: Institution
        public string? DepartmentCode { get; set; }               // FK: Department
        public string? ProgrammeCode { get; set; }                // FK: Programme
        public int SessionId { get; set; }                        // FK: Academic Session
        public int SemesterId { get; set; }                       // FK: Semester
    }
}

