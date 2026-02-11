namespace EduReg.Models.Entities
{
    public class Permission : CommonBase
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string? InstitutionShortName { get; set; }
    }
}
