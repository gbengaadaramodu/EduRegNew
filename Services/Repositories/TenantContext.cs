using EduReg.Services.Interfaces;

namespace EduReg.Services.Repositories
{


    public class TenantContext : ITenantContext
    {
        public string InstitutionShortName { get; private set; }

        public TenantContext(IHttpContextAccessor accessor)
        {
            InstitutionShortName = accessor.HttpContext?.User?.FindFirst("tenant")?.Value ?? "default";
        }
    }
}