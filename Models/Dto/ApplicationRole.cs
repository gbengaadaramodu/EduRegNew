namespace EduReg.Models.Dto
{
    public class RoleName
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public bool IsActive { get; set; }
        public string InstitutionShortName { get; set; }
        public string Description { get; set; }
    }



    public class UserRole
    {
        public string UserName { get; set; }
        public string RoleName { get; set; }

    }
}
