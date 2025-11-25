
using EduReg.Data;
using EduReg.Managers;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;
using EduReg.Services.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EduReg
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("connectionstring")));
            builder.Services.AddIdentity<Students, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = true;

            }).AddDefaultTokenProviders().AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.Configure<EmailConfiguration>(builder.Configuration.GetSection("EmailSettings"));
            builder.Services.Configure<Cyberpaydata>(builder.Configuration.GetSection("Cyberpay"));
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            });
            builder.Services.Configure<BaseUrlConfiguration>(builder.Configuration.GetSection("BaseUrlConfiguration"));

            //  builder.Services.AddAutoMapper(typeof(MappingProfile));
           // builder.Services.AddScoped<StudentManager>();     



            //IOC for Repositories
         //   builder.Services.AddScoped<IStudent, StudentRepository>();
                       
            builder.Services.AddScoped<IInstitutions, InstitutionsRepository>();
            builder.Services.AddScoped<IAdmissionBatches, AdmissionBatchesRepository>();
            builder.Services.AddScoped<IAcademicSessions, AcademicSessionsRepository>();
            builder.Services.AddScoped<ISemesters, SemestersRepository>();

            // School Controller
            builder.Services.AddScoped<IFaculties, FacultiesRepository>();
            builder.Services.AddScoped<IDepartments, DepartmentsRepository>();
            builder.Services.AddScoped<IProgrammes, ProgrammesRepository>();
            builder.Services.AddScoped<IRegistrations, RegistrationsRepository>();
            builder.Services.AddScoped<IRegistrationsBusinessRules, RegistrationsBusinessRulesRepository>();
            builder.Services.AddScoped<IAcademics, AcademicsRepository>();

            //Courses 
            builder.Services.AddScoped<IDepartmentCourses, DepartmentCoursesRepository>();
            builder.Services.AddScoped<IProgramCourses, ProgramCoursesRepository>();
            builder.Services.AddScoped<ICourseSchedule, CourseScheduleRepository>();

            // Fees
            builder.Services.AddScoped<IFeeItems, FeeItemsRepository>();
            builder.Services.AddScoped<IFeeRules, FeeRulesRepository>();
            builder.Services.AddScoped<IProgrammeFeeSchedule, ProgrammeFeeScheduleRepository>();
            builder.Services.AddScoped<IStudentFeePaymentService, StudentFeePaymentServiceRepository>();


            // Managers

            builder.Services.AddScoped<CoursesManager>();
            builder.Services.AddScoped<InstitutionsManager>();
            builder.Services.AddScoped<AcademicsManager>();
            builder.Services.AddScoped<SchoolsManager>();
           //builder.Services.AddScoped<ProgrammesManager>();
            builder  .Services.AddScoped<RegistrationsManager>();
            builder.Services.AddScoped<ProgrammeFeeScheduleManager>();
            builder .Services.AddScoped<FeeServiceManager>();


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("corspolicy", (policy) =>
                {
                    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
