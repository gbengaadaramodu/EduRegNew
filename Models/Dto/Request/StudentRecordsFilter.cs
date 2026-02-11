namespace EduReg.Models.Dto.Request
{
    public class StudentRecordsFilter
    {
   
        public string? InstitutionShortName { get; set; }
        public string? BatchShortName { get; set; }
        public string? MatricNumber { get; set; } // Specific lookup
        public string? ProgrammeCode { get; set; }
        public string? DepartmentCode { get; set; }
        public string? ApplicantId { get; set; }
        public int? CurrentLevel { get; set; }
        public int? CurrentSessionId { get; set; }
        public int? CurrentSemesterId { get; set; }
        public int? AdmittedSessionId { get; set; }
        public int? ActiveStatus { get; set; }

        public string? Search { get; set; }
    }
}