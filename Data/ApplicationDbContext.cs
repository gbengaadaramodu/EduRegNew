using EduReg.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EduReg.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicantSignUp>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<StudentSignUp> StudentSignUps { get; set; }
        public DbSet<AcademicLevel> AcademicLevels { get; set; }
        public DbSet<AcademicSession> AcademicSessions { get; set; }

        public DbSet<Departments> Departments { get; set; }
        public DbSet<Programmes> Programmes { get; set; }
        public DbSet<Faculties> Faculties { get; set; }
    }
}
