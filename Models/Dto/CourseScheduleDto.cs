namespace EduReg.Models.Dto
{
    public class CourseScheduleDto : CommonBaseDto
    {
        public string? InstitutionShortName { get; set; } //Pk -> Institution
        public string? DepartmentCode { get; set; } //FK -> Departments
        public string? ProgrammeCode { get; set; }
        public string? CourseCode { get; set; }
        public string? ClassCode { get; set; }//1, 2, 100, 200, 300, 800, 900
        public string? LevelName { get; set; }// NCE, ND, HND, BSc, MSc, PhD
        public string? Title { get; set; }
        public int Units { get; set; } // Editable
        public string? CourseType { get; set; } // Core, Elective, Mandatory
        public string? Prerequisite { get; set; } //FK -> CourseCode

        public string? BatchShortName { get; set; } //PK -> AcademicBatch 

        public int SessionId { get; set; } //FK -> AcademicSession

        public decimal CourseFee { get; set; } = 0;

        public int SemesterId { get; set; }
        public int LMSId { get; set; } = 1;
        public bool IsOnLMS { get; set; }

    }
}


