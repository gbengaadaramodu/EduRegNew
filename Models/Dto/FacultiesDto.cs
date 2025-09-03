namespace EduReg.Models.Dto
{
    public class FacultiesDto: CommonBaseDto
    {
        public string? FacultyName { get; set; }
        public string? FacultyCode { get; set; } //FK 1001-201
        public string? Description { get; set; }
        public string? InstitutionShortName { get; set; } //FK

    }
}
