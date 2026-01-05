namespace EduReg.Models.Entities
{
    public class CourseRegistrationPolicy: CommonBase
    {
        public string? InstitutionShortName { get; set; } //Pk -> Institution
        public string? ProgrammeCode { get; set; }
        public int SessionId { get; set; } //FK -> AcademicSession
        public int SemesterId { get; set; }
        public int MaxUnits { get; set; }
        public bool IsMaxUnitsActive { get; set; }
        public int MinUnits { get; set; }
        public bool IsMinUnitsActive { get; set; }
    }
}
