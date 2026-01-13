namespace EduReg.Models.Dto.Request
{
    public class FacultyFilter
    {
        public string? InstitutionShortName { get; set; }
        public string? FacultyCode { get; set; }

        // Generic text search
        public string? Search { get; set; }
    }

}
