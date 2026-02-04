using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace EduReg.Services.Repositories
{
    public class AcademicsRepository : IAcademics
    {
        private readonly ApplicationDbContext _context;

        public AcademicsRepository(ApplicationDbContext context)
        {
            _context = context;
             
             
            
        }


        public async Task<GeneralResponse> CreateAcademicLevel(AcademicLevelsDto model)
        {
            if (model == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "Invalid academic level data",
                    Data = null
                };
            }

            var entity = new AcademicLevel
            {
                LevelName = model.LevelName,
                Description = model.Description,
                InstitutionShortName = model.InstitutionShortName
            };

            await _context.AcademicLevels.AddAsync(entity);
            await _context.SaveChangesAsync();

            return new GeneralResponse
            {
                StatusCode = 201,
                Message = "Academic level created successfully",
                Data = entity
            };
        }

        public async Task<GeneralResponse> GetAllAcademicLevelAsync(PagingParameters paging, AcademicLevelFilter filter)
        {
            var query = _context.AcademicLevels.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter?.InstitutionShortName))
            {
                query = query.Where(x => x.InstitutionShortName == filter.InstitutionShortName);
            }

            if (!string.IsNullOrWhiteSpace(filter?.Search))
            {
                query = query.Where(x =>
                    x.LevelName!.Contains(filter.Search));
            }

            // Total count BEFORE pagination
            var totalRecords = await query.CountAsync();

            //Pagination
            var levels = await query
                .OrderBy(x => x.LevelName)
                .Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize)
                .ToListAsync();

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = totalRecords == 0
                    ? "No academic levels found"
                    : "Academic levels retrieved successfully",
                Data = levels,
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


        public async Task<GeneralResponse> GetAcademicLevelByIdAsync(long Id)
        {
            if (Id <= 0)
            {
                return new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "Invalid ID",
                    Data = null
                };
            }

            var level = await _context.AcademicLevels.FindAsync(Id);
            if (level == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "Academic level not found",
                    Data = null
                };
            }

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Academic level retrieved successfully",
                Data = level
            };
        }


        public async Task<GeneralResponse> UpdateAcademicLevelAsync(long Id, AcademicLevelsDto model)
        {
             
            
            var level = await _context.AcademicLevels.FindAsync(Id);

            if (level == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "Academic level not found",
                    Data = null
                };
            }

            level.LevelName = model.LevelName;
            level.Description = model.Description;

            _context.AcademicLevels.Update(level);
            await _context.SaveChangesAsync();

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Academic level updated successfully",
                Data = level
            };
        }


        public async Task<GeneralResponse> DeleteAcademicLevelAsync(long Id)
        {
            var level = await _context.AcademicLevels.FindAsync(Id);

            if (level == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "Academic level not found",
                    Data = null
                };
            }

            _context.AcademicLevels.Remove(level);
            await _context.SaveChangesAsync();

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Academic level deleted successfully",
                Data = null
            };
        }

    }

}
    

        

