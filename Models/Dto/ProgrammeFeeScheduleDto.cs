using EduReg.Common;

namespace EduReg.Models.Dto
{
    public class ProgrammeFeeScheduleDto : CommonBaseDto
    {
        public string InstitutionShortName { get; set; } = null!;
        public string? DepartmentCode { get; set; }
        public string? ProgrammeCode { get; set; }
        public int SessionId { get; set; }
        public int SemesterId { get; set; }
        public string? CourseCode { get; set; }

        public int FeeItemId { get; set; }                // Required
        public int? FeeRuleId { get; set; }               // Optional
        public decimal Amount { get; set; }
    }
}
