using System.ComponentModel.DataAnnotations;

namespace EduReg.Models.Dto
{
    public class StudentLogin
    {
        [Required]
        public  string Username { get; set; }
        [Required]
        public  string Password { get; set; }
    }
}
