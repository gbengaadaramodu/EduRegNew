namespace EduReg.Models.Dto.Request
{
    public class AdmissionBatchFilter
    {
        //  Filters 
        public string? InstitutionShortName { get; set; }
        public string? BatchShortName { get; set; }

        // Search 
        // Applies to: BatchShortName, BatchName, Description
        public string? Search { get; set; }
    }
}
