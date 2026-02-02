namespace EduReg.Models.Dto.Request
{
    public class DepartmentFilter
    {
        public string? InstitutionShortName { get; set; }
        public string? FacultyCode { get; set; }
        public int? Duration { get; set; }

        // Generic search
        public string? Search { get; set; }
    }

}
