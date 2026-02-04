namespace EduReg.Models.Entities
{
    public class StudentDiscount
    {
        public int Id { get; set; }
        public string? MatricNumber { get; set; }
        public decimal DiscountAmount { get; set; }
        public string? Reason { get; set; }
        public DateTime AppliedDate { get; set; }
    }

}
