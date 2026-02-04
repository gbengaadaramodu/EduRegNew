namespace EduReg.Models.Dto.Request
{
    public class ProgrammeFilter
    {
        public string? InstitutionShortName { get; set; }
        public string? DepartmentCode { get; set; }

        public string? ProgrammeCode { get; set; }

        public int? MinDuration { get; set; }
        public int? MaxDuration { get; set; }

        // Generic search
        public string? Search { get; set; }
    }

}
