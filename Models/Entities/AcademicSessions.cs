namespace EduReg.Models.Entities
{
    public class AcademicSession : CommonBase
    {
        //Id, Name(2023/2024), isActive
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

    }
}
