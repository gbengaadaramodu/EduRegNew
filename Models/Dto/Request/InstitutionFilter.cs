namespace EduReg.Models.Dto.Request
{
    public class InstitutionFilter
    {
        public string? InstitutionShortName { get; set; }
        public string? InstitutionName { get; set; }

        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        // Generic search
        public string? Search { get; set; }
    }

}
