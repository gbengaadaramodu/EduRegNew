namespace EduReg.Models.Dto.Request
{
    public class RegistrationBusinessRuleFilter
    {
       // public string? InstitutionShortName { get; set; }

        public string? DepartmentCode { get; set; }
        public string? ProgrammeCode { get; set; }

        public int? SemesterId { get; set; }
        public string? LevelName { get; set; }
        public string? ClassCode { get; set; }

        public int? MinTotalUnits { get; set; }
        public int? MaxTotalUnits { get; set; }

        // Generic search
        public string? Search { get; set; }
    }

}
