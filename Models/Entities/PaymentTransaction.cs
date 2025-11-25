using System.ComponentModel.DataAnnotations;
using static Azure.Core.HttpHeader;

using EduReg.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduReg.Models.Entities
{
    public class PaymentTransaction : CommonBase
    {
        [Required]
        [StringLength(100)]
        public string? OrderNumber { get; set; }
        
        [Required]
        [StringLength(50)]
        public string MatricNumber { get; set; } = string.Empty; // FK to Student

        [Required]
        [StringLength(100)]
        public string InstitutionShortName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Reference { get; set; } = string.Empty; // Unique payment reference or Merchant reference 

        [Required]
        public decimal Amount { get; set; } // Total of all FeeItems in this transaction

        [StringLength(20)]
        public string Status { get; set; } = PaymentStatus.Pending; // Pending, Success, Failed, Refunded

        [StringLength(100)]
        public string? PaymentGateway { get; set; } // e.g. "Flutterwave", "Paystack"

        [StringLength(100)]
        public string? GatewayReference { get; set; } // Ref returned by gateway

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? PaidAt { get; set; }

        public decimal? AmountPaid { get; set; }

        // Navigation
        public ICollection<PaymentTransactionDetail>? PaymentDetails { get; set; }
    }

   
}

