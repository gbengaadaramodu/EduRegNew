using System.ComponentModel.DataAnnotations;

namespace EduReg.Models.Entities
{
     

    public class StudentFeeSchedule
    {
        public int Id { get; set; }

        [Required]
        public string? MatricNumber { get; set; }         
        public string? OrderNumber { get; set; } // Student's StudentOrderDetails containing a list of applicable fee to this student
        // Optional contextual data
        public string? SessionId { get; set; }        // For per-session fees
        public string? SemesterId { get; set; }       // For per-semester fees
        public string? CourseCode { get; set; }       // For per-course fees
        public string? ProgrammeCode { get; set; }    // For programme-specific fees
        public string? DepartmentCode { get; set; }   // Optional filtering

        // Rule source
        public int? FeeRuleId { get; set; }           // Trace back to the rule applied
        public FeeRule? FeeRule { get; set; }

        public decimal Amount { get; set; }

        public bool IsPaid { get; set; } = false;
        public DateTime ScheduledDate { get; set; } = DateTime.UtcNow;
        public DateTime? PaidDate { get; set; }

        public string ScheduledBy { get; set; } = "System"; // or AdminUserId

        // Audit trail
        public string? PaymentReference { get; set; } // Transaction ref
        public string? PaymentChannel { get; set; }   // e.g., Flutterwave, Bank, etc.
    }

}
