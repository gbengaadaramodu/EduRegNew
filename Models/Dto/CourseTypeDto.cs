namespace EduReg.Models.Dto
{
    public class CourseTypeDto : CommonBaseDto
    {
        // The name of the course type (e.g., Core, Elective, Audit)
        public string Name { get; set; }
        

        
    }

    public class UpdateCourseTypeDto : CommonBaseDto
    {
        public string Name { get; set; }
    }
}
