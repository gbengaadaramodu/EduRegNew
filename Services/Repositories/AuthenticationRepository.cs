using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Runtime.InteropServices.JavaScript;
using System.Security.Claims;
using System.Security;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace EduReg.Services.Repositories
{
    public class AuthenticationRepository : IAuthentication
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<Students> _usermanager;
        private readonly SignInManager<Students> _signInManager;
        private readonly ApplicationDbContext _context;
        private IHttpContextAccessor _accessor;
        private readonly TokenCacheRepositories _tokenCache;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;



        public AuthenticationRepository(RoleManager<IdentityRole> roleManager, UserManager<Students> usermanager, ApplicationDbContext context, IHttpContextAccessor accessor,  SignInManager<Students> signInManager, TokenCacheRepositories tokenCache, IConfiguration configuration, IEmailService emailService)
        {

            _roleManager = roleManager;
            _usermanager = usermanager;
            _context = context;
            _accessor = accessor;
            _signInManager = signInManager;
            _tokenCache = tokenCache;
            _configuration = configuration;
            _emailService = emailService;
        }
        public async Task<GeneralResponse> CreateRoleAsync(RoleName model)
        {
            try
            {
                IdentityRole role = new IdentityRole
                {
                    Name = model.Name,
                    NormalizedName = model.NormalizedName,
                    //IsActive = model.IsActive,
                   
                };

                var result = await _roleManager.CreateAsync(role);

                if (!result.Succeeded)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 500,
                        Message = "Role registration failed.",
                        Data = result.Errors
                    };
                }

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = $"{model.Name} created successfully.",
                    Data = role
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 400,
                    Message = ex.Message
                };
            }
        }


        public async Task<GeneralResponse> EditRoleAsync(string roleId, RoleName model)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(roleId);

                if (role == null)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 404,
                        Message = "Role not found."
                    };
                }

                // Update fields
                role.Name = model.Name;
                role.NormalizedName = model.Name.ToUpper();
              //  role.IsActive = model.IsActive;


                var result = await _roleManager.UpdateAsync(role);

                if (!result.Succeeded)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 500,
                        Message = "Role update failed." ,
                        Data = result.Errors
                    };
                }

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = $"{role.Name} updated successfully.",
                    Data = role
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 400,
                    Message = ex.Message
                };
            }
        }


        public async Task<GeneralResponse> GetAllRolesAsync()
        {
            try
            {
                var roles = await Task.FromResult(_roleManager.Roles
                        .Select(r => new
                        {
                            r.Id,
                            r.Name,
                            r.NormalizedName,
                         //   r.IsActive,
                           
                        })
                        .ToList()
                );

                if (roles == null || roles.Count == 0)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 404,
                        Message = "No roles found."
                    };
                }

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "Roles retrieved successfully.",
                    Data = roles
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }



        public async Task<GeneralResponse> AssignRoleToUserAsync(UserRole user)
        {
            try
            {
                // Find role by name
                var role = await _roleManager.FindByNameAsync(user.RoleName);
                if (role == null)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 404,
                        Message = $"Role '{user.RoleName}' does not exist.",
                        Data = null
                    };
                }

                // Find user by username
                var registeredUser = await _usermanager.FindByNameAsync(user.UserName);
                if (registeredUser == null)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 404,
                        Message = $"User '{user.UserName}' not found.",
                        Data = null
                    };
                }

                // Assign role
                var result = await _usermanager.AddToRoleAsync(registeredUser, role.Name);
                if (!result.Succeeded)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 500,
                        Message = "Failed to assign role to user.",
                        Data = result.Errors
                    };
                }

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = $"Role '{role.Name}' assigned to '{registeredUser.UserName}' successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 400,
                    Message = ex.Message,
                    Data = null
                };
            }
        }



        public async Task<GeneralResponse> CreateUpdatePermission(PermissionModel model)
        {
            var response = new GeneralResponse();
            try
            {
           
                if (model.Id > 0)
                {
                    var permission = _context.PermissionTables.FirstOrDefault(a => a.Id == model.Id);

                    if (permission != null)
                    {
                        permission.Name = model.Name;
                        permission.IsActive = model.IsActive;
                        permission.InstitutionShortName = model.InstitutionShortName;

                        _context.PermissionTables.Update(permission);
                        _context.SaveChanges();

                        return new GeneralResponse
                        {
                            StatusCode = 200,
                            Message = "Permission updated successfully",
                            Data = null
                        };
                    }
                    else
                    {
                        return new GeneralResponse
                        {
                            StatusCode = 404,
                            Message = "The Permission cannot updated as the Permission does not exist",
                            Data = null
                        };
                    }
                }
                else
                {
                    var request = new Permission
                    {
                        Name = model.Name,
                        InstitutionShortName = model.InstitutionShortName,
                        CreatedBy = _accessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email) ?? "Admin",
                        Created = DateTime.Now,
                    };

                    _context.PermissionTables.Add(request);
                    _context.SaveChanges();

                    return new GeneralResponse
                    {
                        StatusCode = 200,
                        Message = "Permission Created successfully",
                        Data = null
                    };

                }


            }
            catch (Exception ex)
            {
               
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while updating faculty",
                    Data = ex.Message
                };
            }

        }
        public async Task<GeneralResponse> GetAllPermission()
        {
            var permission = new List<PermissionModel>();
            var response = new GeneralResponse();

            try
            {
                permission = _context.PermissionTables.Select(model => new PermissionModel
                {
                    Id = (int)model.Id,
                    Name = model.Name,
                    InstitutionShortName = model.InstitutionShortName,
                    IsActive = model.IsActive,
                    CreatedDate = model.Created,
                }).ToList();

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "Record(s) fetched",
                    Data = permission
                };

            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while updating faculty",
                    Data = ex.Message
                };
            }
        }


        public async Task<GeneralResponse> GetPermissionById(int Id)
        {
            var permission = new PermissionModel();
            var response = new GeneralResponse();
            try
            {
                permission = _context.PermissionTables.Where(a => a.Id == Id).Select(model => new PermissionModel
                {
                    Id = (int)model.Id,
                    Name = model.Name,
                    InstitutionShortName = model.InstitutionShortName,
                    IsActive = model.IsActive,
                    CreatedDate = model.Created,
                }).FirstOrDefault();

               
                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "Record fetched",
                    Data = permission
                };

            }
            catch (Exception ex)
            {

                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while updating faculty",
                    Data = ex.Message
                };
            }

           
        }



        public async Task<GeneralResponse> CreateUpdatePrivillege(PrivilegeModel model)
        {
            var response = new GeneralResponse();
            try
            {

                if (model.Id > 0)
                {
                    var privillege = _context.Privileges.FirstOrDefault(a => a.Id == model.Id);

                    if (privillege != null)
                    {
                        privillege.RoleId = model.RoleId;
                        privillege.PermissionId = model.PermissionId;
                        privillege.InstitutionShortName = model.InstitutionShortName;

                        _context.Privileges.Update(privillege);
                        _context.SaveChanges();

                        return new GeneralResponse
                        {
                            StatusCode = 200,
                            Message = "Privilege updated successfully",
                            Data = null
                        };
                    }
                    else
                    {
                        return new GeneralResponse
                        {
                            StatusCode = 404,
                            Message = "The Privilege cannot updated as the Permission does not exist",
                            Data = null
                        };
                    }
                }
                else
                {
                    var request = new Privilege
                    {
                        RoleId = model.RoleId,
                        InstitutionShortName = model.InstitutionShortName,
                        CreatedBy = _accessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email) ?? "Admin",
                        Created = DateTime.Now,
                    };

                    _context.Privileges.Add(request);
                    _context.SaveChanges();

                    return new GeneralResponse
                    {
                        StatusCode = 200,
                        Message = "Privilege Created successfully",
                        Data = null
                    };

                }


            }
            catch (Exception ex)
            {

                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while updating faculty",
                    Data = ex.Message
                };
            }

        }

        public async Task<GeneralResponse> GetAllPrivileges()
        {
            var privilege = new List<PrivilegeModel>();
            var response = new GeneralResponse();

            try
            {
                privilege = _context.Privileges.Select(model => new PrivilegeModel
                {
                    Id = (int)model.Id,
                    RoleId = model.RoleId,
                    PermissionId = model.PermissionId,
                    InstitutionShortName = model.InstitutionShortName,
                    CreatedDate = model.Created,
                }).ToList();

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "Record(s) fetched",
                    Data = privilege
                };

            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while updating faculty",
                    Data = ex.Message
                };
            }
        }


        public async Task<GeneralResponse> GetPrivilegeById(int Id)
        {
            var privilege = new PrivilegeModel();
            var response = new GeneralResponse();
            try
            {
                privilege = _context.Privileges.Where(a => a.Id == Id).Select(model => new PrivilegeModel
                {
                    Id = (int)model.Id,
                    RoleId = model.RoleId,
                    PermissionId = model.PermissionId,
                    InstitutionShortName = model.InstitutionShortName,
                    CreatedDate = model.Created,
                }).FirstOrDefault();


                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "Record fetched",
                    Data = privilege
                };

            }
            catch (Exception ex)
            {

                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while updating faculty",
                    Data = ex.Message
                };
            }


        }



        public async Task<GeneralResponse> RegisterAdminAsync(RegisterAdminRequests model)
        {
            var checkEmail = await _usermanager.FindByEmailAsync(model.Email);

            if (checkEmail != null)
            {
                return new GeneralResponse
                {
                    StatusCode = 401,
                    Message = "Admin already exists",
                    Data = null
                };
            }


            foreach (var role in model.Roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Ensure roles exist
            //if (!await _roleManager.RoleExistsAsync("Admin"))
            //    await _roleManager.CreateAsync(new IdentityRole("Admin"));

            //if (!await _roleManager.RoleExistsAsync("SuperAdmin"))
            //    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));

            // Create the user
            var adminUser = new Students
            {
                LastName = model.LastName,
                FirstName = model.FirstName,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                PhoneNumber = model.PhoneNumber,
                UserName = model.Email,
                isAdmin = true,
            };

            var result = await _usermanager.CreateAsync(adminUser, model.Password);

            if (!result.Succeeded)
            {
                return new GeneralResponse
                {
                    StatusCode = 401,
                    Message = "Unable to register an admin",
                    Data = null
                };
            }

            // Add user to role
            if (model.Roles != null && model.Roles.Any())
            {
                var roleResult = await _usermanager.AddToRolesAsync(adminUser, model.Roles);

                if (!roleResult.Succeeded)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 400,
                        Message = "User created but roles could not be assigned",
                        Data = roleResult.Errors
                    };
                }
            }


            //// ✅ Send welcome email with login credentials
            //var subject = "CyberCloud Equote";


            //string message = @$"
            //<p>Dear {adminUser.FirstName}</p>
            //<p>Kindly find attached your userid: <b>{model.Email}</b> and password: <b>{model.Password}</b></p>
            //<p>Please log in to <a href=https://localhost:5173/reset-password>Admin Login</a> and change your password immediately for security reasons.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</p>
            //<p>Regards,</p>
            //<p>CyberCloud Admin</p>";

            //await _emailService.SendEmailAsync(model.Email, subject, message);

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Admin created successfully. Login details have been sent via email.",
                Data = model
            };
        }


        public async Task<GeneralResponse> GetAllAdminAsync()
        {
            try
            {
                var admins = await _usermanager.Users
                    .Where(u => u.isAdmin == true)
                    .OrderByDescending(u => u.CreatedDate)
                    .Select(u => new
                    {
                        u.Id,
                        u.LastName,
                        u.FirstName,
                        u.Email,
                        u.PhoneNumber,
                        u.CreatedDate,

                        // Fetch roles from UserRoles table
                        Roles = (from userRole in _context.UserRoles
                                 join role in _context.Roles on userRole.RoleId equals role.Id
                                 where userRole.UserId == u.Id
                                 select role.Name).ToList()
                    })
                    .ToListAsync();

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "Successful",
                    Data = admins
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }


        public async Task<GeneralResponse> GetAdminByEmailAsync(string email)
        {
            var response = new GeneralResponse();

            try
            {
                var admin = await _usermanager.Users
                    .Where(u => u.Email == email).OrderByDescending(u => u.CreatedDate)
                    .Select(u => new
                    {
                        u.Id,
                        u.LastName,
                        u.FirstName,   
                        u.Email,
                        u.PhoneNumber,
                        u.CreatedDate,

                        Roles = (from userRole in _context.UserRoles
                                 join role in _context.Roles
                                    on userRole.RoleId equals role.Id
                                 where userRole.UserId == u.Id
                                 select role.Name).ToList()
                    })
                    .FirstOrDefaultAsync();

                if (admin == null)
                {
                    response.StatusCode = 404;
                    response.Message = "Admin not found";
                    response.Data = null;
                }
                else
                {
                    response.StatusCode = 200;
                    response.Message = "Successful";
                    response.Data = admin;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = $"An error occurred: {ex.Message}";
                response.Data = null;
            }

            return response;
        }

        public async Task<GeneralResponse> UpdateAdminAsync(string email, RegisterAdminRequests model)
        {
            var response = new GeneralResponse();

            try
            {
                var admin = await _usermanager.FindByEmailAsync(email);

                if (admin == null)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 404,
                        Message = "Admin not found",
                        Data = null
                    };
                }

                // Update basic fields
                admin.LastName = model.LastName ?? admin.LastName;
                admin.FirstName = model.FirstName ?? admin.FirstName;
                admin.Email = model.Email ?? admin.Email;
                admin.PhoneNumber = model.PhoneNumber ?? admin.PhoneNumber;
                admin.isAdmin = true;

                var updateResult = await _usermanager.UpdateAsync(admin);

                if (!updateResult.Succeeded)
                {
                    var errorMessages = string.Join("; ", updateResult.Errors.Select(e => e.Description));

                    return new GeneralResponse
                    {
                        StatusCode = 400,
                        Message = $"Failed to update admin: {errorMessages}",
                        Data = null
                    };
                }


                if (model.Roles != null && model.Roles.Any())
                {
                    foreach (var role in model.Roles)
                    {
                        if (!await _roleManager.RoleExistsAsync(role))
                        {
                            await _roleManager.CreateAsync(new IdentityRole(role));
                        }
                    }
                    var currentRoles = await _usermanager.GetRolesAsync(admin);

                    await _usermanager.RemoveFromRolesAsync(admin, currentRoles);

                    // Add new roles
                    var roleResult = await _usermanager.AddToRolesAsync(admin, model.Roles);

                    if (!roleResult.Succeeded)
                    {
                        return new GeneralResponse
                        {
                            StatusCode = 400,
                            Message = "Admin updated but roles could not be assigned",
                            Data = roleResult.Errors
                        };
                    }
                }

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "Admin updated successfully",
                    Data = model
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<GeneralResponse> DeleteAdminAsync(string Email)
        {
            var response = new GeneralResponse();

            try
            {
                var admin = _usermanager.Users.FirstOrDefault(u => u.Email == Email);

                if (admin == null)
                {
                    response.StatusCode = 404;
                    response.Message = "Admin not found";
                    response.Data = null;
                    return response;
                }

                var result = await _usermanager.DeleteAsync(admin);

                if (result.Succeeded)
                {
                    response.StatusCode = 200;
                    response.Message = "Admin deleted successfully";
                    response.Data = null;
                }
                else
                {
                    response.StatusCode = 400;
                    response.Message = "Failed to delete admin";
                    response.Data = result.Errors; // optional: include error details
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = $"An error occurred: {ex.Message}";
                response.Data = null;
            }

            return response;
        }

        public async Task<GeneralResponse> LoginAdminAsync(LoginAdminRequest model)
        {
            var userToken = new LoginToApplicationResponseDto
            {
                UserName = model.UserName
            };

            // Find user
            var user = await _usermanager.FindByEmailAsync(model.UserName);
            if (user == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "Invalid credentials",
                    Data = null
                };
            }

            // Validate password
            var loginResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (!loginResult.Succeeded)
            {
                return new GeneralResponse
                {
                    StatusCode = 401,
                    Message = "Invalid Username/ Password",
                    Data = null
                };
            }

            // Get user roles
            var roles = await _usermanager.GetRolesAsync(user);
            userToken.Roles = roles.ToList();

            // Generate JWT token
            var token = await GenerateJwtToken(user);

            userToken.Token = token;
            userToken.UserId = user.Id;
            userToken.FirstName = user.FirstName;
            userToken.LastName = user.LastName;

            _tokenCache.AddToken(token);

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Successful",
                Data = userToken
            };
        }

        public async Task<GeneralResponse> ResetPasswordAsync(ResetPasswordRequestDto model)

        {
            var user = await _usermanager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "User not found",
                    Data = null
                };
            }

            // Generate a password reset token
            var token = await _usermanager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Uri.EscapeDataString(token);

            // Create a secure reset password URL (replace with your actual frontend URL)
            var resetLink = $"http://localhost:3000/reset-password?email={Uri.EscapeDataString(user.Email)}&token={encodedToken}";

            var subject = $"{user.UserName} - Password Reset";

            string message = @$"
        <p>Dear {user.FirstName},</p>
        <p>We received a request to reset your password.</p>
        <p>Please click the link below to reset your password securely:</p>
        <p><a href='{resetLink}'>Reset Password</a></p>
        <p>If you did not request this, please ignore this email.</p>
        <br />
        <p>Regards,</p>";

            await _emailService.SendEmailAsync(model.Email, subject, message);

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "A password reset link has been sent to your email.",
                Data = null
            };
        }


        public async Task<GeneralResponse> ConfirmResetPasswordAsync(ConfirmResetPasswordRequestDto model)
        {
            var user = await _usermanager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "User not found.",
                    Data = null
                };
            }

            var decodedToken = Uri.UnescapeDataString(model.Token);

            var result = await _usermanager.ResetPasswordAsync(user, decodedToken, model.NewPassword);

            if (result.Succeeded)
            {
                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "Password has been reset successfully.",
                    Data = null
                };
            }

            var errors = string.Join("; ", result.Errors.Select(e => e.Description));
            return new GeneralResponse
            {
                StatusCode = 400,
                Message = $"Failed to reset password: {errors}",
                Data = null
            };
        }


        public async Task<GeneralResponse> ChangePasswordAsync(ChangePasswordRequest model)
        {
            var user = await _usermanager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "User does not exist",
                    Data = null
                };
            }

            var result = await _usermanager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (result.Succeeded)
            {
                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "Password changed successfully",
                    Data = null
                };
            }

            var errors = string.Join("; ", result.Errors.Select(e => e.Description));
            return new GeneralResponse
            {
                StatusCode = 400,
                Message = $"Unable to change password: {errors}",
                Data = null
            };
        }



        public async Task<GeneralResponse> LoginUserAsync(StudentLoginRequest model)
        {


            var userToken = new LoginUserResponseDto();

            userToken.UserName = model.MatricNumber;

            //Does this user exists?
            var email = await _usermanager.FindByEmailAsync(model.MatricNumber);
            if (email != null)
            { // Email exists
                await _signInManager.PasswordSignInAsync(model.MatricNumber, model.Password, false, false);


                var token = await GenerateJwtToken(email);
                userToken.Token = token;
                // Add the token to a cache

                _tokenCache.AddToken(token.ToString());


                return new GeneralResponse { StatusCode = 200, Message = "Successful", Data = userToken };
            }
            else
            {

                var mat = await _usermanager.FindByNameAsync(model.MatricNumber);
                if (mat != null)
                { // Email exists
                    var res = await _signInManager.PasswordSignInAsync(model.MatricNumber, model.Password, false, false);
                    if (res.Succeeded)
                    {
                        var token = await GenerateJwtToken(mat);
                        userToken.Token = token;

                        //Add the token to a cache
                        _tokenCache.AddToken(token.ToString());
                        return new GeneralResponse { StatusCode = 200, Message = "Successful", Data = userToken };
                    }
                    else
                    {
                        return new GeneralResponse { StatusCode = 200, Message = "Invalid password", Data = userToken };
                    }

                }
            }

            return new GeneralResponse { StatusCode = 404, Message = "Invalid credentials", Data = null };

        }






        private async Task<string?> GenerateJwtToken(Students user)
        {

            var userRoles = await _usermanager.GetRolesAsync(user);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("FullName", user.Email)
            };

            // Add roles as claims
            var roleClaims = new List<Claim>();
            foreach (var role in userRoles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
           issuer: _configuration["JwtSettings:Issuer"],
           audience: _configuration["JwtSettings:Audience"],
           claims: claims.Concat(roleClaims),
           expires: DateTime.UtcNow.AddHours(2),
           signingCredentials: creds
       );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

    }
}
