using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EduReg.Services.Repositories
{
    public class StudentRepository : IStudent
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        public StudentRepository(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<(StudentResponse? item, string message, bool isSuccess)> StudentLogin(StudentLogin student)
        {
            try
            {

                var stud = _context.Students.FirstOrDefault(s => s.UserName == student.Username || s.Email == student.Username);
                if(stud == null)
                {
                    return (null, "Invalid username. Username can either be Matric number or email", false);
                }

                if (!stud.LockoutEnabled || stud.LockoutEnd.HasValue)
                {
                    return (null, "Your account is locked or not active. Please contact the administrator.", false);
                }

                //if (stud.IsLock.HasValue && stud.IsLock.Value)
                //{
                //    return (null, "Your account is locked. Please contact the administrator.", false);
                //}


                var encryptedPassword = PasswordGenerator.EncryptString(student.Password);
                if(stud.PasswordHash != encryptedPassword)
                {
                    return (null, "Invalid password", false);
                }

                var authClaims = new List<Claim>
                {
                        new Claim(ClaimTypes.Email, stud.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                //var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                var secretKey = _configuration["JWTCoreSettings:SecretKey"];
                if (string.IsNullOrEmpty(secretKey))
                {
                    return (null, "JWT configuration is missing", false);
                }
                
                var authSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(secretKey));
                var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(7),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                var studentDetail = new StudentResponse
                {
                    Token = tokenString,
                    MatricNumber = stud.UserName,
                    LastName = stud.UserName,
                    FirstName = stud.UserName,
                    MiddleName = "",
                    Email = stud.Email,
                    PhoneNumber = stud.PhoneNumber,
                    ProgramTypeId = 0,
                    CurrentAcademicSessionId = 0,
                    CurrentAcademicSession = "Unknown",
                    CurrentLevelId = 0,
                    CurrentLevel = "Unknown",
                    InstitutionShortName = stud.InstitutionShortName
                };

                return (studentDetail, "Successful", true);

            }
            catch (Exception ex)
            {
                return (null, ex.Message, false);
            }
        }
    }
}
