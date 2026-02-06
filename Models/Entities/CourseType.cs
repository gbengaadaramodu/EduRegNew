namespace EduReg.Models.Entities
{
    public class CourseType : CommonBase
    {
        public string Name { get; set; } // e.g., "Core", "Elective"
        public string? InstitutionShortName { get; set; } 
        
    }
}
