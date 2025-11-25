using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EduReg.Services.Repositories
{
    public class InstitutionsRepository : IInstitutions
    {
        private readonly ApplicationDbContext _context;

        public InstitutionsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GeneralResponse> CreateInstitutionAsync(InstitutionsDto model)
        {
            try
            {
                var existingInstitution = await _context.Institutions
                    .FirstOrDefaultAsync(i => i.InstitutionShortName == model.InstitutionShortName);

                if (existingInstitution != null)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 400,
                        Message = "Institution with this short name already exists."
                    };
                }

                var institution = new Institutions
                {
                    InstitutionShortName = model.InstitutionShortName,
                    InstitutionName = model.InstitutionName,
                    Address = model.Address,
                    ContactPerson = model.ContactPerson,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Description = model.Description,
                    Created = model.Created,
                    CreatedBy = model.CreatedBy,
                    ActiveStatus = model.ActiveStatus
                };

                _context.Institutions.Add(institution);
                await _context.SaveChangesAsync();

                return new GeneralResponse
                {
                    StatusCode = 201,
                    Message = "Institution created successfully.",
                    Data = institution
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"Internal Server Error: {ex.Message}"
                };
            }
        }

        public async Task<GeneralResponse> DeleteInstitutionAsync(int id)
        {
            try
            {
                var institution = await _context.Institutions.FindAsync(id);
                if (institution == null)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 404,
                        Message = "Institution not found."
                    };
                }

                _context.Institutions.Remove(institution);
                await _context.SaveChangesAsync();

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "Institution deleted successfully."
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"Internal Server Error: {ex.Message}"
                };
            }
        }

        public async Task<GeneralResponse> GetAllInstitutionAsync()
        {
            try
            {
                var institutions = await _context.Institutions.ToListAsync();
                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = institutions.Count == 0 ? "No institutions found." : "Institutions retrieved successfully.",
                    Data = institutions
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"Internal Server Error: {ex.Message}"
                };
            }
        }

        public async Task<GeneralResponse> GetInstitutionByIdAsync(int id)
        {
            try
            {
                var institution = await _context.Institutions.FindAsync(id);
                if (institution == null)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 404,
                        Message = "Institution not found."
                    };
                }

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "Institution retrieved successfully.",
                    Data = institution
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"Internal Server Error: {ex.Message}"
                };
            }
        }

        public async Task<GeneralResponse> GetInstitutionByShortNameAsync(string shortName)
        {
            try
            {
                var institution = await _context.Institutions
                    .FirstOrDefaultAsync(i => i.InstitutionShortName.ToLower() == shortName.ToLower());

                if (institution == null)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 404,
                        Message = "Institution not found."
                    };
                }

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "Institution retrieved successfully.",
                    Data = institution
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"Internal Server Error: {ex.Message}"
                };
            }
        }

        public async Task<GeneralResponse> UpdateInstitutionAsync(int id, InstitutionsDto model)
        {
            try
            {
                var institution = await _context.Institutions.FindAsync(id);
                if (institution == null)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 404,
                        Message = "Institution not found."
                    };
                }

                institution.InstitutionShortName = model.InstitutionShortName;
                institution.InstitutionName = model.InstitutionName;
                institution.Address = model.Address;
                institution.ContactPerson = model.ContactPerson;
                institution.Email = model.Email;
                institution.PhoneNumber = model.PhoneNumber;
                institution.Description = model.Description;
                institution.ActiveStatus = model.ActiveStatus;

                _context.Institutions.Update(institution);
                await _context.SaveChangesAsync();

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "Institution updated successfully.",
                    Data = institution
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"Internal Server Error: {ex.Message}"
                };
            }
        }
    }
}
