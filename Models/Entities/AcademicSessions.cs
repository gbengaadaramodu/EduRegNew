using System.Runtime.CompilerServices;

namespace EduReg.Models.Entities
{
    public class AcademicSession : CommonBase
    {
        public string? BatchShortName { get; set; } //FK from Admission Batches// 
        public int SessionId { get; set; } // PK -> Semester
        public string? SessionName { get; set; } // 2023/2024, 2024/2025 etc
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsDeleted { get; set; }
    }
         
}
