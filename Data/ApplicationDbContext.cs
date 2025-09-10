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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AcademicSession>().HasKey(x => x.SessionId);
            builder.Entity<Semester>().HasKey(x => x.SemesterId);
            builder.Entity<Semester>().HasOne(s => s.Session)
                .WithMany(a => a.Semesters)
                .HasForeignKey(s => s.SessionId);
        }

        public DbSet<StudentSignUp> StudentSignUps { get; set; }
        public DbSet<AcademicLevel> AcademicLevels { get; set; }
        public DbSet<AcademicSession> AcademicSessions { get; set; }

        public DbSet<Departments> Departments { get; set; }
        public DbSet<Programmes> Programmes { get; set; }
        public DbSet<Faculties> Faculties { get; set; }
        public DbSet<Semester> Semesters { get; set; }
    }
}
