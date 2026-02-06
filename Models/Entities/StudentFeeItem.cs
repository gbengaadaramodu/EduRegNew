using EduReg.Models.Entities;

public class StudentFeeItem : CommonBase
{
    public int StudentFeeScheduleId { get; set; }  // FK to parent
    public StudentFeeSchedule? StudentFeeSchedule { get; set; }  // Navigation property

    public string FeeItemName { get; set; } = string.Empty;  // e.g. "Library Fee"
    public decimal Amount { get; set; }                      // Fee amount
    public bool IsPaid { get; set; } = false;                // Payment status
    public DateTime? DueDate { get; set; }                   // Optional for penalties or reminders
}
