using EduReg.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduReg.Models.Dto
{
    public class StudentFeeScheduleDto : CommonBaseDto
    {
        [Required]
        public string InstitutionShortName { get; set; } = string.Empty;

        [Required]
        public string MatricNumber { get; set; } = string.Empty;  // UNIQUE IDENTIFIER for each student

        [Required]
        public string ProgrammeCode { get; set; } = string.Empty;

        [Required]
        public int SessionId { get; set; }

        public int Level { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal AmountPaid { get; set; } = 0;

        [NotMapped]
        public decimal OutstandingAmount => TotalAmount - AmountPaid;

        [NotMapped]
        public bool IsFullyPaid => OutstandingAmount <= 0;

        public ICollection<StudentFeeItem>? StudentFeeItems { get; set; }
    }
}
