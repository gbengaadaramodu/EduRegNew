using Microsoft.EntityFrameworkCore;

namespace EduReg.Models.Entities
{
    [Index(nameof(InstitutionShortName))]
    public class EmailConfiguration
    {
        public string? InstitutionShortName { get; set; } = null!;
        public string? Sender { get; set; }
        public string? SmtpServer { get; set; }
        public int Port { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
