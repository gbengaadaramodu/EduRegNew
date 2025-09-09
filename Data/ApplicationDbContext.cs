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
        public DbSet<Level> AcademicLevels { get; set; }
        public DbSet<AcademicSession> AcademicSessions { get; set; }
        public DbSet<Faculties> Faculties { get; set; }
    }
}
