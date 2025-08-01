namespace EduReg.Models.Dto
{
    public class StudentResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string MatricNumber { get; set; }
        public int ProgramTypeId { get; set; }
        public string CurrentAcademicSession { get; set; }
        public string CurrentLevel { get; set; }
        public int CurrentAcademicSessionId { get; set; }
        public int CurrentLevelId { get; set; }
        public string Token { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string MiddleName { get; set; }
    }
}
