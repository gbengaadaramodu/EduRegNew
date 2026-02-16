namespace EduReg.Models.Dto
{
    public class TicketDto
    {
       // public string StudentName { get; set; }

        public string MatricNumber { get; set; }
        public string Title { get; set; }
        public string MessageBody { get; set; }
        // Note: ReferenceNumber and Status are NOT here because the system generates them
    }

    public class GetTicketDto
    {
        public long Id { get; set; }
        public string MatricNumber { get; set; }
        public string Title { get; set; }
        public string MessageBody { get; set; }

        public string TicketStatus { get; set; }
    }

    public class RespondToTicketDto
    {
       
        public string MatricNumber { get; set; }
        public string ResponseBody { get; set; }
        
    }
}
