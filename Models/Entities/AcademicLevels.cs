namespace EduReg.Models.Entities
{
    public class Level : CommonBase
    {
        public string LevelName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
