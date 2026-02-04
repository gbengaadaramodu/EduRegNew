namespace EduReg.Models.Dto
{
    public class CourseMaxMinDto : CommonBaseDto
    {
        public string InstitutionShortName { get; set; }

        // Logic Links
        public int ProgramId { get; set; }  // The Department/Faculty
        public int LevelId { get; set; }    // 100, 200, etc.
        public int SemesterId { get; set; } // Link to the Semester definition

        // The Rules
        public string CourseType { get; set; } // e.g., "General", "Core", "Elective"
        public int MinimumUnits { get; set; }
        public int MaximumUnits { get; set; }
    }

    public class UpdateCourseMaxMinDto : CommonBaseDto
    {
        // The PK from CommonBase (Id)
        

        // What is allowed to change
        public int MinimumUnits { get; set; }
        public int MaximumUnits { get; set; }

        // You might want to allow changing the status (Active/Inactive)
        // which is already handled by inheriting CommonBaseDto
    }
}
