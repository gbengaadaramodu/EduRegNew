namespace EduReg.Models.Entities
{
    public class StudentStatus : CommonBase
    {
        public string Name { get; set; } = string.Empty; // e.g., Active, Suspended, Graduated
        public bool IsActive { get; set; } = true;
        public string InstitutionShortName { get; set; } = string.Empty;
    }
}