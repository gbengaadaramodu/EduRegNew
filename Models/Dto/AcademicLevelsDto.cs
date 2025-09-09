namespace EduReg.Models.Dto
{
    public class AcademicLevelsDto : CommonBaseDto
    {

        public string? LevelName { get; set; }
        public string? Description { get; set; }
        public string? LevelId { get; set; } 
   
        public bool IsDeleted { get; set; }
    }
}
