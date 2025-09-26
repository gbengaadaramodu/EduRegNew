namespace EduReg.Models.Dto
{
    public class BusinessRulesDto
    {
        //id
        public string? InstitutionShortName { get; set; } //Pk -> Institution
        public string? DepartmentCode { get; set; } //FK -> Departments
        public string? ProgrammeCode { get; set; }
        public int SessionId { get; set; } //FK -> AcademicSession
        public int SemesterId { get; set; }
    }
}
