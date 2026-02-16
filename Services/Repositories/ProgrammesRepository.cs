using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace EduReg.Services.Repositories
{
    public class ProgrammesRepository : IProgrammes
    {
        private readonly ApplicationDbContext _context;
        private readonly RequestContext _requestContext;
        private readonly IMapper _mapper;

        public ProgrammesRepository(ApplicationDbContext context, RequestContext requestContext, IMapper mapper)
        {
            _context = context;
            _requestContext = requestContext;
            _mapper = mapper;
        }

        public async Task<GeneralResponse> CreateProgrammeAsync(ProgrammesDto model)
        {
            try
            {  

                if (await _context.Programmes.AnyAsync(p => p.ProgrammeCode == model.ProgrammeCode && p.InstitutionShortName == _requestContext.InstitutionShortName))
                {
                    return new GeneralResponse
                    {
                        StatusCode = 403,
                        Message = "A Programme with this Programme Code already exists"
                    };
                }

                var programme = new Programmes
                {
                    DepartmentCode = model.DepartmentCode,
                    ProgrammeCode = model.ProgrammeCode,
                    ProgrammeName = model.ProgrammeName,
                    Description = model.Description,
                    Duration = model.Duration,
                    NumberOfSemesters = model.NumberOfSemesters,
                    MaximumNumberOfSemesters = model.MaximumNumberOfSemesters,
                    InstitutionShortName = _requestContext.InstitutionShortName
                };

                await _context.Programmes.AddAsync(programme);
                await _context.SaveChangesAsync();

                var programmeDto = _mapper.Map<ProgrammesDto>(programme);
                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "Programme created successfully",
                    Data = programmeDto
                };

            }

            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"An error occurred while creating the Programme: {ex.Message}"                    
                };
            }

        }

        public async Task<GeneralResponse> DeleteProgrammeAsync(long id)
        {
            try
            {
                var programme = await _context.Programmes
            .FirstOrDefaultAsync(x => x.Id == id && x.InstitutionShortName == _requestContext.InstitutionShortName);
                if (programme == null)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 404,
                        Message = "Programme not found"
                    };
                }

                _context.Programmes.Remove(programme);
                await _context.SaveChangesAsync();

                var programmeDto = _mapper.Map<ProgrammesDto>(programme);

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "Programme deleted successfully",
                    Data = programmeDto
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"An error occurred while deleting the Programme: {ex.Message}"
                };
            }
        }

        public async Task<GeneralResponse> GetAllProgrammesAsync(PagingParameters paging, ProgrammeFilter filter)
        {
            try
            {
                var query = _context.Programmes
                    .AsNoTracking()
                    .AsQueryable();

                    query = query.Where(x =>
                        x.InstitutionShortName == _requestContext.InstitutionShortName);

                // APPLY FILTERS
                if (!string.IsNullOrWhiteSpace(filter?.DepartmentCode))
                {
                    query = query.Where(x =>
                        x.DepartmentCode == filter.DepartmentCode);
                }

                if (!string.IsNullOrWhiteSpace(filter?.ProgrammeCode))
                {
                    query = query.Where(x =>
                        x.ProgrammeCode == filter.ProgrammeCode);
                }

                if (filter?.MinDuration != null)
                {
                    query = query.Where(x =>
                        x.Duration >= filter.MinDuration);
                }

                if (filter?.MaxDuration != null)
                {
                    query = query.Where(x =>
                        x.Duration <= filter.MaxDuration);
                }

                // Generic search 
                if (!string.IsNullOrWhiteSpace(filter?.Search))
                {
                    query = query.Where(x =>
                        (x.ProgrammeCode != null && x.ProgrammeCode.Contains(filter.Search)) ||
                        (x.ProgrammeName != null && x.ProgrammeName.Contains(filter.Search)) ||
                        (x.Description != null && x.Description.Contains(filter.Search))
                    );
                }

                // PAGINATION
                var totalRecords = await query.CountAsync();

                var programmes = await query
                    .OrderBy(x => x.ProgrammeName)
                    .Skip((paging.PageNumber - 1) * paging.PageSize)
                    .Take(paging.PageSize)
                    .Select(p => new ProgrammesDto
                    {
                        DepartmentCode = p.DepartmentCode,
                        ProgrammeCode = p.ProgrammeCode,
                        ProgrammeName = p.ProgrammeName,
                        Description = p.Description,
                        Duration = p.Duration,
                        NumberOfSemesters = p.NumberOfSemesters,
                        MaximumNumberOfSemesters = p.MaximumNumberOfSemesters
                    })
                    .ToListAsync();

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = totalRecords == 0
                        ? "No programmes found."
                        : "Programmes retrieved successfully.",
                    Data = programmes,
                    Meta = new
                    {
                        paging.PageNumber,
                        paging.PageSize,
                        TotalRecords = totalRecords,
                        TotalPages = totalRecords == 0
                            ? 0
                            : (int)Math.Ceiling(totalRecords / (double)paging.PageSize)
                    }
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"An error occurred while retrieving programmes: {ex.Message}",
                    Data = null
                };
            }
        }


        public async Task<GeneralResponse> GetProgrammeByIdAsync(long id)
        {
            try
            {
                var programme = await _context.Programmes
             .AsNoTracking() 
             .FirstOrDefaultAsync(x => x.Id == id && x.InstitutionShortName == _requestContext.InstitutionShortName);

                if (programme == null)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 404,
                        Message = "Programme not found"
                    };
                }

                var programmeDto = new ProgrammesDto
                {
                    DepartmentCode = programme.DepartmentCode,
                    ProgrammeCode = programme.ProgrammeCode,
                    ProgrammeName = programme.ProgrammeName,
                    Description = programme.Description,
                    Duration = programme.Duration,
                    NumberOfSemesters = programme.NumberOfSemesters,
                    MaximumNumberOfSemesters = programme.MaximumNumberOfSemesters,
                    InstitutionShortName = programme.InstitutionShortName
                };

                return new GeneralResponse 
                {
                    StatusCode = 200,
                    Message = "Programme retrieved successfully",
                    Data = programmeDto
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"An error occurred while retrieving the Programme: {ex.Message}"
                };
            }
        }

        public async Task<GeneralResponse> GetProgrammeByNameAsync(string ProgrammeName)
        {
            try
            {
                var programme = await _context.Programmes.FirstOrDefaultAsync(p => p.ProgrammeName.ToLower().Contains(ProgrammeName.ToLower())
                && p.InstitutionShortName == _requestContext.InstitutionShortName);

                if (programme == null)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 404,
                        Message = "Programme not found"
                    };
                }

                var programmeDto = new ProgrammesDto
                {
                    DepartmentCode = programme.DepartmentCode,
                    ProgrammeCode = programme.ProgrammeCode,
                    ProgrammeName = programme.ProgrammeName,
                    Description = programme.Description,
                    Duration = programme.Duration,
                    NumberOfSemesters = programme.NumberOfSemesters,
                    MaximumNumberOfSemesters = programme.MaximumNumberOfSemesters
                };

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "Programme retrieved successfully",
                    Data = programmeDto
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"An error occurred while retrieving the Programme: {ex.Message}"
                };
            }
        }

        public async Task<GeneralResponse> UpdateProgrammeAsync(long id, ProgrammesDto model)
        {
            try
            {
                var programme = await _context.Programmes
            .FirstOrDefaultAsync(x => x.Id == id && x.InstitutionShortName == _requestContext.InstitutionShortName);

                if (programme == null)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 404,
                        Message = "Programme not found"
                    };
                }

                programme.DepartmentCode = model.DepartmentCode;
                programme.ProgrammeCode = model.ProgrammeCode;
                programme.ProgrammeName = model.ProgrammeName;
                programme.Description = model.Description;
                programme.Duration = model.Duration;
                programme.NumberOfSemesters = model.NumberOfSemesters;
                programme.MaximumNumberOfSemesters = model.MaximumNumberOfSemesters;

                _context.Programmes.Update(programme);
                await _context.SaveChangesAsync();

                var programmeDto = _mapper.Map<ProgrammesDto>(programme);

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "Programme updated successfully",
                    Data = programmeDto
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"An error occurred while updating the Programme: {ex.Message}"
                };
            }
        }
    }
}
