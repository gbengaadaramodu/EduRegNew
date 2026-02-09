namespace EduReg.Models.Dto.Request
{
    public class TicketingFilter
    {
        public string? Search { get; set; } // Student Name or Reference Number

        // Valid values: "Open" or "Closed"
        public string? Status { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
