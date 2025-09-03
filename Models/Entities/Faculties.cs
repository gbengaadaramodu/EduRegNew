namespace EduReg.Models.Entities
{
    public class Faculties: CommonBase
    {
        public string? FacultyName { get; set; }
        public string? FacultyCode { get; set; } //FK 1001-201
        public string? Description { get; set; }
        public string? InstitutionShortName { get; set; } //FK


    }
}
