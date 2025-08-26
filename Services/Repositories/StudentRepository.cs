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
        public async Task<(StudentResponse item, string message, bool isSuccess)> StudentLogin(StudentLogin student)
        {
            try
            {

                var stud = _context.StudentSignUps.FirstOrDefault(s => s.MatricNumber == student.Username || s.Email == student.Username);
                if(stud == null)
                {
                    return (null, "Invalid username. Username can either be Matric number or email", false);
                }

                if (!stud.IsActive.HasValue || !stud.IsActive.Value)
                {
                    return (null, "Your account is not active. Please contact the administrator.", false);
                }

                //if (stud.IsLock.HasValue && stud.IsLock.Value)
                //{
                //    return (null, "Your account is locked. Please contact the administrator.", false);
                //}


                var encryptedPassword = PasswordGenerator.EncryptString(student.Password);
                if(stud.Password != encryptedPassword)
                {
                    return (null, "Invalid password", false);
                }

                var authClaims = new List<Claim>
                {
                        new Claim(ClaimTypes.Email, stud.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                //var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                var authSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(_configuration["JWTCoreSettings:SecretKey"]));
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
                    MatricNumber = stud.MatricNumber,
                    LastName = stud.LastName,
                    FirstName = stud.FirstName,
                    MiddleName = stud.MiddleName,
                    Email = stud.Email,
                    PhoneNumber = stud.PhoneNumber,
                    ProgramTypeId = stud.ProgramTypeId,
                    CurrentAcademicSessionId = stud.CurrentAcademicSessionId,
                    CurrentAcademicSession = _context.AcademicSessions
                        .Where(s => s.Id == stud.CurrentAcademicSessionId)
                        .Select(s => s.SessionName)
                        .FirstOrDefault(),
                    CurrentLevelId = stud.AcademicLevelId,
                    CurrentLevel = _context.AcademicLevels
                        .Where(l => l.Id == stud.AcademicLevelId)
                        .Select(l => l.LevelName)
                        .FirstOrDefault(),
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
