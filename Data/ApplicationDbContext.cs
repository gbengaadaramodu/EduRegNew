using EduReg.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EduReg.Data
{
    public class ApplicationDbContext : IdentityDbContext<Students>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Institutions> Institutions { get; set; }
        public DbSet<Students> Students { get; set; }
        public DbSet<AcademicLevel> AcademicLevels { get; set; }
        public DbSet<AcademicSession> AcademicSessions { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<Programmes> Programmes { get; set; }
        public DbSet<Faculties> Faculties { get; set; }
        public DbSet<CourseSchedule> CourseSchedules { get; set; }
        public DbSet<Semesters> Semesters { get; set; }
        public DbSet<AdmissionBatches> AdmissionBatches { get; set; }
        public DbSet<ProgramCourses> ProgramCourses { get; set; }
        public DbSet<DepartmentCourses> DepartmentCourses { get; set; }
        public DbSet<CourseSchedule> CourseSchedule { get; set; }

        public DbSet<Registrations> Registrations { get; set; }

        public DbSet<RegistrationsBusinessRules> RegistrationsBusinessRules { get; set; }
         
        public DbSet<FeeItem> FeeItem { get; set; }
        public DbSet<FeeRule> FeeRule { get; set; }
        public DbSet<StudentFeeSchedule> StudentFeeSchedule { get; set; }
                     
        public DbSet<ProgrammeFeeSchedule> ProgrammeFeeSchedule { get; set; }

   
        public DbSet<StudentFeeItem> StudentFeeItem { get; set; }
        public DbSet<CourseRegistration> CourseRegistrations { get; set; }
        public DbSet<CourseRegistrationDetail> CourseRegistrationDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<AdmissionBatches>()
                .HasKey(x => new { x.Id, x.BatchShortName });
            
            modelBuilder.Entity<Institutions>()
                .HasKey(x => new { x.Id, x.InstitutionShortName });
            
        }

    }
}
