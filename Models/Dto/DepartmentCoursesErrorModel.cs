namespace EduReg.Models.Dto
{
    public class DepartmentCoursesErrorModel
    {
        public string CourseCode { get; set; }
        public string DepartmentCode { get; set; }
        public string Title { get; set; }
        public string ErrorMessage { get; set; }
    }
}
