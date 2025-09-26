using EduReg.Common;
using EduReg.Models.Entities;

namespace EduReg.Models.Dto
{
    public class FeeRuleDto : CommonBaseDto
    {

        public int FeeItemId { get; set; }
        public FeeItem FeeItem { get; set; }
        public string? InstitutionShortName { get; set; }

        // Applicability (optional filters)
        public string? ProgrammeCode { get; set; }
        public string? DepartmentCode { get; set; }
        public string? LevelName { get; set; }
        public string? ClassCode { get; set; }

        public string? SessionId { get; set; }
        public string? SemesterId { get; set; }

        public decimal Amount { get; set; }
        public bool IsRecurring { get; set; }

        public FeeRecurrenceType RecurrenceType { get; set; }             // How often
        public FeeApplicabilityScope ApplicabilityScope { get; set; }     // Who it applies to
    }
}
