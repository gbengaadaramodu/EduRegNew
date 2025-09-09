
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
        //
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("connectionstring")));
            builder.Services.AddIdentity<ApplicantSignUp, IdentityRole>(options =>
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
            builder.Services.AddScoped<StudentManager>();
         



            //IOC for Repositories
            builder.Services.AddScoped<IStudent, StudentRepository>();
                       
            builder.Services.AddScoped<IInstitutions, InstitutionsRepository>();
            builder.Services.AddScoped<IAdmissionBatches, AdmissionBatchesRepository>();
            builder.Services.AddScoped<IAcademicSessions, AcademicSessionsRepository>();
            builder.Services.AddScoped<ISemesters, SemestersRepository>();
            builder.Services.AddScoped<IFaculties, FacultiesRepository>();
            builder.Services.AddScoped<IDepartments, DepartmentsRepository>();
            builder.Services.AddScoped<IProgrammes, ProgrammesRepository>();

            // Managers
            builder.Services.AddScoped<InstitutionsManager>();
            builder.Services.AddScoped<AcademicsManager>();
            builder.Services.AddScoped<SchoolManager>();
            builder.Services.AddScoped<ProgrammesManager>();

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
