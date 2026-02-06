using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EduReg.Models.Dto
{
    public class InstitutionsDto: CommonBaseDto
    {
        
        [MaxLength(8)]
        public string? InstitutionShortName { get; set; }
        public string? InstitutionName { get; set; }
        public string? Address { get; set; }
        public string? ContactPerson { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Description { get; set; }
        
    }

    public class UpdateInstitutionsDto : CommonBaseDto
    {
        public string? InstitutionName { get; set; }
        public string? Address { get; set; }
        public string? ContactPerson { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Description { get; set; }
    }

}
