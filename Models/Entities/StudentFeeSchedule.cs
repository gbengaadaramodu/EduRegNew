using EduReg.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduReg.Models.Entities
{

    //    [Index(nameof(InstitutionShortName))]
    //    public class StudentFeeSchedule
    //    {
    //        public int Id { get; set; }
    //        public string? InstitutionShortName { get; set; } // Pk -> Institution

    //        [Required]
    //        public string? MatricNumber { get; set; }         
    //        public string? OrderNumber { get; set; } // Student's StudentOrderDetails containing a list of applicable fee to this student
    //        // Optional contextual data
    //        public string? SessionId { get; set; }        // For per-session fees
    //        public string? SemesterId { get; set; }       // For per-semester fees
    //        public string? CourseCode { get; set; }       // For per-course fees
    //        public string? ProgrammeCode { get; set; }    // For programme-specific fees
    //        public string? DepartmentCode { get; set; }   // Optional filtering

    //        // Rule source
    //        public int? FeeRuleId { get; set; }           // Trace back to the rule applied
    //        public FeeRule? FeeRule { get; set; }

    //        public decimal Amount { get; set; }

    //        public bool IsPaid { get; set; } = false;
    //        public DateTime ScheduledDate { get; set; } = DateTime.UtcNow;
    //        public DateTime? PaidDate { get; set; }

    //        public string ScheduledBy { get; set; } = "System"; // or AdminUserId

    //        // Audit trail
    //        public string? PaymentReference { get; set; } // Transaction ref
    //        public string? PaymentChannel { get; set; }   // e.g., Flutterwave, Bank, etc.
    //    }

    //}
    [Index(nameof(InstitutionShortName))]
    public class StudentFeeSchedule : CommonBase
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
        public FeeItem? FeeItem { get;  set; }
    }
}

