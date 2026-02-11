using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request;
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
        private readonly RequestContext _requestContext;

        public InstitutionsRepository(ApplicationDbContext context, RequestContext requestContext)
        {
            _context = context;
            _requestContext = requestContext;
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
                var institution = await _context.Institutions.FirstOrDefaultAsync(x => x.Id == id);
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

        public async Task<GeneralResponse> GetAllInstitutionAsync(PagingParameters paging,InstitutionFilter filter)
        {
            try
            {
                var query = _context.Institutions.AsQueryable();

                
                // Filters

                if (!string.IsNullOrWhiteSpace(filter?.InstitutionShortName))
                    query = query.Where(x => x.InstitutionShortName == filter.InstitutionShortName);

                if (!string.IsNullOrWhiteSpace(filter?.InstitutionName))
                    query = query.Where(x => x.InstitutionName!.Contains(filter.InstitutionName));

                if (!string.IsNullOrWhiteSpace(filter?.Email))
                    query = query.Where(x => x.Email!.Contains(filter.Email));

                if (!string.IsNullOrWhiteSpace(filter?.PhoneNumber))
                    query = query.Where(x => x.PhoneNumber!.Contains(filter.PhoneNumber));

                // Generic Search
                if (!string.IsNullOrWhiteSpace(filter?.Search))
                {
                    query = query.Where(x =>
                        (x.InstitutionShortName != null && x.InstitutionShortName.Contains(filter.Search)) ||
                        (x.InstitutionName != null && x.InstitutionName.Contains(filter.Search)) ||
                        (x.Email != null && x.Email.Contains(filter.Search)) ||
                        (x.PhoneNumber != null && x.PhoneNumber.Contains(filter.Search)) ||
                        (x.Address != null && x.Address.Contains(filter.Search))
                    );
                }

                
                // Pagination
               
                var totalRecords = await query.CountAsync();

                var institutions = await query
                    .OrderBy(x => x.InstitutionName)
                    .Skip((paging.PageNumber - 1) * paging.PageSize)
                    .Take(paging.PageSize)
                    .ToListAsync();

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = totalRecords == 0
                        ? "No institutions found."
                        : "Institutions retrieved successfully.",
                    Data = institutions,
                    Meta = new
                    {
                        paging.PageNumber,
                        paging.PageSize,
                        TotalRecords = totalRecords,
                        TotalPages = totalRecords == 0
                            ? 0
                            : Convert.ToInt32(Math.Ceiling(totalRecords / (double)paging.PageSize))
                    }
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"Internal Server Error: {ex.Message}",
                    Data = null
                };
            }
        }


        public async Task<GeneralResponse> GetInstitutionByIdAsync(int id)
        {
            try
            {
                var institution = await _context.Institutions.FirstOrDefaultAsync(x => x.Id == id);
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

        public async Task<GeneralResponse> UpdateInstitutionAsync(int id, UpdateInstitutionsDto model)
        {
            try
            {
                var institution = await _context.Institutions.FirstOrDefaultAsync(x => x.Id == id);

                if (institution == null)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 404,
                        Message = "Institution not found."
                    };
                }

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

        public async Task<GeneralResponse> UpdateInstitutionByShortNameAsync(string shortname, UpdateInstitutionsDto model)
        {
            try
            {
                var institution = await _context.Institutions.FirstOrDefaultAsync(x => x.InstitutionShortName == shortname);


                if (institution == null)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 404,
                        Message = "Institution not found."
                    };
                }

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
