using Microsoft.AspNetCore.Identity;

namespace EduReg.Models.Entities
{
    public class Roles : IdentityRole
    {
        public Roles()
        {
            
        }
        public Roles(string roleName)
        {
            Name = roleName;
        }
        public string InstitutionShortName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
