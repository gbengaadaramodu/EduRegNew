namespace EduReg.Common
{
    public class GeneralResponse
    {
        public int StatusCode { get; set; } // 200, 201, 400, 401, 403, 404, 500
        public string? Message { get; set; } // Success, Created, Bad Request, Unauthorized, Forbidden, Not Found, Internal Server Error
        public object? Data { get; set; }
    }
}
