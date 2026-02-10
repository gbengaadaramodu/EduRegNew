using EduReg.Models.Entities;

namespace EduReg.Common
{
    public class RequestContext
    {
        public string InstitutionShortName { get; set; } = string.Empty;
        public Institutions? Institution { get; set; }
    }

}
