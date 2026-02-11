
namespace EduReg.Models.Dto
{
    public class PermissionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string? InstitutionShortName { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
