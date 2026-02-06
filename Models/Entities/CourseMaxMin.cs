namespace EduReg.Models.Entities
{
    public class CourseMaxMin : CommonBase
    {
        public string? InstitutionShortName { get; set; }

        // Relationship Links (Foreign Keys)
        public int ProgramId { get; set; }  
        public int LevelId { get; set; }    
        public int SemesterId { get; set; } 

        // Rules
        public string? CourseType { get; set; } // e.g., "Major", "Elective", "General"

        public int MinimumUnits { get; set; } 
        public int MaximumUnits { get; set; } 

       
    }
}
