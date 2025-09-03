using System.ComponentModel.DataAnnotations;

namespace EduReg.Models.Entities
{
    public class Institutions : CommonBase
    {
       

        [Key]
        public string? InstitutionShortName { get; set; } //FK
        public string? InstitutionName { get; set; }
        public string?  Address { get; set; }
        public string? ContactPerson { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Description { get; set; }       
        public string? InstitutionKey { get; set; } = Guid.NewGuid().ToString();


    }
}
