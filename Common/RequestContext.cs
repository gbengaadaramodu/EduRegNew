using EduReg.Models.Dto;
using EduReg.Models.Entities;

namespace EduReg.Common
{
    public class RequestContext
    {
        public string InstitutionShortName { get; set; } = string.Empty;
        public InstitutionsDto? Institution { get; set; }
    }

}
