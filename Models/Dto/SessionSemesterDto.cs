namespace EduReg.Models.Dto
{
    public class SessionSemesterDto : CommonBaseDto
    {
            public string InstitutionShortName { get; set; }

            

        // The foreign keys to link to the parent entities
            public int SessionId { get; set; }
            public int SemesterId { get; set; }
            public bool IsActive { get; set; }
            public DateTime RegistrationStartDate { get; set; }
            public DateTime RegistrationCloseDate { get; set; }
            public DateTime ExamStartDate { get; set; }
            public DateTime ExamEndDate { get; set; }
       
    }

    public class UpdateSessionSemesterDto : CommonBaseDto
    {
        public int SessionSemesterId { get; set; } // The PK

        public DateTime RegistrationStartDate { get; set; }
        public DateTime RegistrationCloseDate { get; set; }

        public DateTime ExamStartDate { get; set; }
        public DateTime ExamEndDate { get; set; }
    }
}
