using Microsoft.AspNetCore.Identity;

namespace EduReg.Models.Entities
{
    public class Roles : IdentityRole
    {
        public bool IsActive { get; set; }
    }
}
