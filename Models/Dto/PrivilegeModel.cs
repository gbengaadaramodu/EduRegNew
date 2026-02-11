
namespace EduReg.Models.Dto
{
    public class PrivilegeModel
    {
        public int Id { get; set; }
        public string RoleId { get; set; }
        public int PermissionId { get; set; }
        public string InstitutionShortName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
