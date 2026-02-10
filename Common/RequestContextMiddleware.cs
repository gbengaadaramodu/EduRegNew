using EduReg.Common.Attributes;
using EduReg.Services.Interfaces;
using EduReg.Services.Repositories;
using Microsoft.Net.Http.Headers;

namespace EduReg.Common
{
    public class RequestContextMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, RequestContext requestContext, IInstitutions repository)
        {
            var endpoint = context.GetEndpoint();
            if (endpoint == null)
            {
                await _next(context);
                return;
            }

            // Respect attributes
            var requireHeader = endpoint.Metadata.GetMetadata<RequireInstitutionShortNameAttribute>() != null;
            var skipHeader = endpoint.Metadata.GetMetadata<SkipRequireInstitutionShortNameAttribute>() != null;

            if (!requireHeader || skipHeader)
            {
                await _next(context);
                return;
            }

            // Header validation
            if (!context.Request.Headers.TryGetValue("InstitutionShortName", out var institutionHeader)
                || string.IsNullOrWhiteSpace(institutionHeader))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "Missing or empty InstitutionShortName header"
                });
                return;
            }

            // DB validation
            var institutionShortName = institutionHeader.ToString().Trim();
            var institution = await repository.GetInstitutionByShortNameAsync(institutionShortName);
            if (institution == null || institution.StatusCode != 200)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new GeneralResponse
                {
                    StatusCode = 400,
                    Message = $"Invalid InstitutionShortName {institutionShortName}"
                });
                return;
            }

            // Store in RequestContext for downstream usage
            requestContext.InstitutionShortName = institutionShortName;
            requestContext.Institution = (Models.Entities.Institutions)institution.Data;

            await _next(context);
        }
    }


}
