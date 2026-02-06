using EduReg.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduReg.Models.Entities
{
    public class PaymentTransactionDetail : CommonBase
    {
        [Required]
        public int PaymentTransactionId { get; set; } // FK
        public PaymentTransaction? PaymentTransaction { get; set; }

        [Required]
        public int StudentFeeItemId { get; set; } // FK
        public StudentFeeItem? StudentFeeItem { get; set; }

        public string? Description { get; set; } = string.Empty;

        [Required]
        public decimal Amount { get; set; }

        public bool IsPaid { get; set; } = false;
    }
}
