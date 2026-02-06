namespace EduReg.Models.Entities
{
    public class Privilege : CommonBase
    {
        public string RoleId { get; set; }
        public int PermissionId { get; set; }
        public string InstitutionShortName { get; set; }
    }
}
