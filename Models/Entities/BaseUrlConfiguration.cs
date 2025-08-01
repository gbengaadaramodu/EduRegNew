namespace EduReg.Models.Entities
{
    public class BaseUrlConfiguration
    {
        public string BaseVeryUrl { get; set; }
        public string BaseUrl { get; set; }
        public string BaseUrlVerify { get; set; }
        public string BasePasswordRestUrl { get; set; }
        public string BaseAdminPasswordRestUrl { get; set; }
        public string BaseUrlLogin { get; set; }
        public string BaseAdminUrl { get; set; }

    }

    public class ForgotPasswordEmail
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Link { get; set; }
        public string Name { get; set; }
    }

    public class VerifyEmail
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Link { get; set; }
        public string Subject { get; set; }
        public string Password { get; set; }
        public string Message { get; set; }
    }

    public class LoginEmail
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Name { get; set; }
    }
}
