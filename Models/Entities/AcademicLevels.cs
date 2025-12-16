namespace EduReg.Models.Entities
{
    public class AcademicLevel : CommonBase
    {

        public string? ClassCode { get; set; } //ND I, ND II, HND I, HND II , //100 Level, 200 Level, 300 Level, Part 1, Part 2
        public string? LevelName { get; set; } //Level
        public string? ProgrammeCode { get; set; } //FK -> Programmes //201
        public string? CourseAdviser { get; set; } ="Not Assigned"; //FK -> Staff
        public string? Description { get; set; }
        public int LevelId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int? Order { get; set; }
        public string? InstitutionShortName { get; set; }
    }
}
