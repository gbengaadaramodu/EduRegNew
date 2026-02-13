using EduReg.Models.Dto;
using System.ComponentModel.DataAnnotations;

namespace EduReg.Models.Entities
{
    public class PaymentTransactionDetailDto : CommonBaseDto
    {
        [Required]
        public int PaymentTransactionId { get; set; } // FK from PaymentTransaction (the OrderNumber)
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
