namespace EduReg.Models.Entities
{
    public class Ticketing : CommonBase
    {
        // Request Fields (Student)
        public string StudentName { get; set; }
        public string MatricNumber { get; set; }

       // public string StudentId { get; set; }
        public string Title { get; set; }
        public string MessageBody { get; set; }
        public string ReferenceNumber { get; set; } // Generated: e.g., TIC-2026-A1B2
        public string InstitutionShortName { get; set; }

        // Status Tracking
        public string TicketStatus { get; set; } // "Open", "Closed"

        // Response Fields (Staff/Admin)
        public string? ResponseBody { get; set; }
        public DateTime? DateOfResponse { get; set; }
    }
}
