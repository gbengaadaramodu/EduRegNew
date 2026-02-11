using System.Net.Mail;
using System.Net;
using EduReg.Services.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace EduReg.Services.Repositories
{
    public class EmailSenderRepository : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailSenderRepository(IConfiguration config)
        {
            _config = config;
        }
        public async Task SendResetPasswordEmailAsync(string email, string token)
        {
            var client = new SendGridClient(_config["SendGrid:ApiKey"]);
            var from = new EmailAddress(_config["SendGrid:FromEmail"], _config["SendGrid:FromName"]);
            var to = new EmailAddress(email);

            var resetLink = $"{_config["App:FrontendUrl"]}/reset-password?email={Uri.EscapeDataString(email)}&token={Uri.EscapeDataString(token)}";

            var subject = "Password Reset Request";
            var htmlContent = $"<p>Click <a href='{resetLink}'>here</a> to reset your password. This link expires in 1 hour.</p>";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlContent);
            var response = await client.SendEmailAsync(msg);

            if (response.StatusCode != HttpStatusCode.Accepted)
            {
                throw new Exception("Email sending failed.");
            }
        }

        public async Task SendEmailAsync(string toEmail, string subject, string htmlMessage)
        {


            var apiKey = _config["SendGrid:ApiKey"];
            var fromEmail = _config["SendGrid:FromEmail"];
            var fromName = _config["SendGrid:FromName"];

            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(fromEmail, fromName);
            var to = new EmailAddress(toEmail);

            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);
            await client.SendEmailAsync(msg);
        }
    }
}
