namespace EduReg.Models.Dto.Request
{
    public class ProgrammeFeeScheduleFilter
    {
        //public string? InstitutionShortName { get; set; }

        public string? DepartmentCode { get; set; }
        public string? ProgrammeCode { get; set; }

        public int? SessionId { get; set; }
        public int? SemesterId { get; set; }

        public string? CourseCode { get; set; }
        public long? FeeItemId { get; set; }

        public decimal? MinAmount { get; set; }
        public decimal? MaxAmount { get; set; }

        // Generic search
        public string? Search { get; set; }
    }

}
