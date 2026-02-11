using Microsoft.AspNetCore.Identity;

namespace EduReg.Models.Entities
{
    public class UserTable : IdentityUser
    {
        public string? Role { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
    }
}
