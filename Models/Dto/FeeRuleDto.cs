using EduReg.Common;
using EduReg.Models.Entities;
using Microsoft.EntityFrameworkCore; 

namespace EduReg.Models.Dto
{
     
    public class FeeRuleDto : CommonBaseDto
    {
        public long Id { get; set; }
        public FeeItem? FeeItem { get; set; }
        public string? InstitutionShortName { get; set; }

        public string? ProgrammeCode { get; set; }
        public string? DepartmentCode { get; set; }
        public string? LevelName { get; set; }
        public string? ClassCode { get; set; }

        public string? SessionId { get; set; }
        public string? SemesterId { get; set; }

        public decimal Amount { get; set; }
        public bool IsRecurring { get; set; }
        public DateTime? EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }

        public FeeRecurrenceType RecurrenceType { get; set; }
        public FeeApplicabilityScope ApplicabilityScope { get; set; }
    }
}

