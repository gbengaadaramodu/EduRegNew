namespace EduReg.Models.Entities
{
    public class LateFeeRule
    {
        public int Id { get; set; }
        public int FeeRuleId { get; set; }
        public decimal PenaltyAmount { get; set; }
        public int DaysAfterDue { get; set; }
    }

}
