using Microsoft.EntityFrameworkCore;

namespace EduReg.Models.Entities
{
    [Index(nameof(InstitutionShortName))]
    public class InstallmentPayment
    {
        public int Id { get; set; }
        public string? InstitutionShortName { get; set; }
        public int StudentFeeScheduleId { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? PaymentReference { get; set; }
    }
}
