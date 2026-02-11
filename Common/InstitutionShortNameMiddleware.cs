using EduReg.Common.Attributes;

namespace EduReg.Common
{
    public class InstitutionShortNameMiddleware
    {
        private readonly RequestDelegate _next;
        private const string HeaderName = "InstitutionShortName";

        public InstitutionShortNameMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            if (endpoint == null)
            {
                await _next(context);
                return;
            }

            var requireHeader =
                endpoint.Metadata.GetMetadata<RequireInstitutionShortNameAttribute>() != null;

            var skipHeader =
                endpoint.Metadata.GetMetadata<SkipRequireInstitutionShortNameAttribute>() != null;

            // Only enforce if controller has [RequireHeader]
            if (!requireHeader || skipHeader)
            {
                await _next(context);
                return;
            }

            if (!context.Request.Headers.TryGetValue(HeaderName, out var value)
                || string.IsNullOrWhiteSpace(value))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                //await context.Response.WriteAsJsonAsync(new
                //{
                //    error = $"Missing required header: {HeaderName}"
                //});
                await context.Response.WriteAsJsonAsync(new GeneralResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = $"Missing required header: {HeaderName}"
                });
                return;
            }

            await _next(context);
        }

    }
}
