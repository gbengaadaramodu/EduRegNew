
using EduReg.Common;
using EduReg.Data;
using EduReg.Interfaces;
using EduReg.Managers;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;
using EduReg.Services.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;


namespace EduReg
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("connectionstring")));
            builder.Services.AddIdentity<Students, Roles>(options =>
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



            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["Secret"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];

            if (string.IsNullOrEmpty(secretKey))
            {
                throw new InvalidOperationException("JWT Secret is missing from AppSettings.json.");
            }

            // ? Add JWT Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidIssuer = issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });



            // IOC for Repositories
            builder.Services.AddScoped<IStudent, StudentRepository>();
            builder.Services.AddScoped<IStudentStatus, StudentStatusRepository>();
            builder.Services.AddScoped<IStudentRecords, StudentRecordsRepository>();
                       
            builder.Services.AddScoped<IInstitutions, InstitutionsRepository>();
            builder.Services.AddScoped<IAdmissionBatches, AdmissionBatchesRepository>();
            builder.Services.AddScoped<IAcademicSessions, AcademicSessionsRepository>();
            builder.Services.AddScoped<ISemesters, SemestersRepository>();
            builder.Services.AddScoped<ISessionSemester, SessionSemesterRepository>();

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
            builder.Services.AddScoped<ICourseRegistration, CourseRegistrationRepository>();
            builder.Services.AddScoped<ICourseMaxMin, CourseMaxMinRepository>();
            builder.Services.AddScoped<ICourseType, CourseTypeRepository>();

            // Fees
            builder.Services.AddScoped<IFeeTypes, FeeTypeRepository>();
            builder.Services.AddScoped<IFeeItems, FeeItemsRepository>();
            builder.Services.AddScoped<IFeeRules, FeeRulesRepository>();
            builder.Services.AddScoped<IProgrammeFeeSchedule, ProgrammeFeeScheduleRepository>();
            builder.Services.AddScoped<IStudentFeePaymentService, StudentFeePaymentServiceRepository>();

            //Library
            //Ticketing
            builder.Services.AddScoped<ITicketing, TicketingRepository>();

            builder.Services.AddScoped<IELibrary, ELibraryRepository>();


            //Authentication
            builder.Services.AddScoped<IAuthentication, AuthenticationRepository>();
            builder.Services.AddScoped<TokenCacheRepositories>();
            builder.Services.AddMemoryCache();

            //Email Service
            builder.Services.AddScoped<IEmailService, EmailSenderRepository>();



            // Managers


            builder.Services.AddScoped<StudentManager>();
            builder.Services.AddScoped<CoursesManager>();
            builder.Services.AddScoped<InstitutionsManager>();
            builder.Services.AddScoped<AcademicsManager>();
            builder.Services.AddScoped<SchoolsManager>();
            builder.Services.AddScoped<RegistrationsManager>();
            builder.Services.AddScoped<ProgrammeFeeScheduleManager>();
            builder.Services.AddScoped<FeeServiceManager>();
            builder.Services.AddScoped<FeePaymentManager>();
            builder.Services.AddScoped<AuthenticationManager>();

            builder.Services.AddScoped<TicketingManager>();


           
            
            
            
            builder.Services.AddScoped<RequestContext>();
            


            builder.Services.AddScoped<LibraryManager>();



            builder.Services.AddCors(options =>
            {
                options.AddPolicy("corspolicy", (policy) =>
                {
                    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });


            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/";
            });


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                // Bearer token
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer {token}'"
                });

                //InstitutionShortName header
                options.AddSecurityDefinition("InstitutionShortName", new OpenApiSecurityScheme
                {
                    Name = "InstitutionShortName",
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Description = "Institution short name"
                });

                // Apply both globally
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    },
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "InstitutionShortName"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}


            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseCors("corspolicy");

            //app.UseMiddleware<InstitutionShortNameMiddleware>();
            app.UseMiddleware<RequestContextMiddleware>();

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }


    }
}
