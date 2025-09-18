namespace EduReg.Models.Dto
{
    public class RegistrationBusinessRulesDto: CommonBaseDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? InstitutionShortName { get; set; } //FK
        public string? DepartmentCode { get; set; } //FK
        public string? ProgrammeCode { get; set; } //FK
        public int SemesterId { get; set; } //FK
        public string? LevelName { get; set; } //FK
        public string? ClassCode { get; set; } //FK
        public int TotalCompulsoryUnits { get; set; }
        public int TotalElectiveUnits { get; set; }
        public int TotalMinimumCreditUnits { get; set; }// Comebeack to enforce
        public int TotalMaximumCreditUnits { get; set; }
        public string? Remarks { get; set; }
    }
}
