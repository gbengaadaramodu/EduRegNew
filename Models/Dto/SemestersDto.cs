using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EduReg.Models.Dto
{
    [Index(nameof(InstitutionShortName))]
    public class SemestersDto: CommonBaseDto
    {
        public string? InstitutionShortName { get; set; } //FK Institution
        [Required(ErrorMessage = "SessionName is required")]
        public string? SessionName { get; set; } //FK Academic Session
        [Required(ErrorMessage = "Session Id is required")]
        public int SessionId { get; set; } //FK Academic Session
        [Required(ErrorMessage = "SemesterName is required")]
        public string? SemesterName { get; set; }
        //public int SemesterId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "BatchShortName is required")]
        public string BatchShortName { get; set; }
        public DateTime? RegistrationStartDate { get; set; }
        public DateTime? RegistrationEndDate { get; set; }
        public DateTime? RegistrationCloseDate { get; set; }
    }
}
