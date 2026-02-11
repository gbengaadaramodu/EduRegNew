using EduReg.Models.Entities;

namespace EduReg.Models.Entities
{
    public class FeeType : CommonBase
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string InstitutionShortName { get; set; } = string.Empty;
        public bool IsSystemDefined { get; set; } = false;
    }
}