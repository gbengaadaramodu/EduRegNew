using System.ComponentModel.DataAnnotations;

namespace EduReg.Models.Dto.Request
{
    public class DepartmentCoursesUploadModel
    {
        public string DepartmentCode { get; set; }
        public string CourseCode { get; set; }
        public string Title { get; set; }
        public int Units { get; set; }
        public string CourseType { get; set; }
        [MaxLength(8000)]
        public string? Description { get; set; }
    }
}
