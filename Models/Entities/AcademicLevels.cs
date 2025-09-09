namespace EduReg.Models.Entities
{
    public class AcademicLevel : CommonBase
    {

        public string? LevelName { get; set; }
        public string? Description { get; set; }
        public int LevelId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
