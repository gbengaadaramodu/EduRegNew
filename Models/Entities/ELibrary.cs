namespace EduReg.Models.Entities
{
    public class ELibrary : CommonBase
    {
        public string CourseCode { get; set; } = string.Empty;
        public long ProgramId { get; set; }
        public string FilePath { get; set; } = string.Empty; // URL or Storage path
        public string Title { get; set; } = string.Empty;
        public string InstitutionShortName { get; set; } = string.Empty;
    }
}