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
        private readonly RequestContext _requestContext;

        public AcademicsRepository(ApplicationDbContext context, RequestContext requestContext)
        {
            _context = context;
            _requestContext = requestContext;
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

            var programmeExists = await _context.Programmes.AnyAsync(x => x.ProgrammeCode == model.ProgrammeCode && x.InstitutionShortName == model.InstitutionShortName);
            if (programmeExists)
            {
                return new GeneralResponse
                {
                    StatusCode = 400,
                    Message = $"Invalid programme code: {model.ProgrammeCode}",
                    Data = null
                };
            }

            var academicLevelExists = await _context.AcademicLevels.AnyAsync(x => x.InstitutionShortName == _requestContext.InstitutionShortName && x.ClassCode == model.ClassCode && x.ProgrammeCode == model.ProgrammeCode);
            if (academicLevelExists)
            {
                return new GeneralResponse
                {
                    StatusCode = 400,
                    Message = $"Academic level with class code {model.ClassCode} already exists",
                    Data = null
                };
            }

            var academicLevel = await _context.AcademicLevels.OrderByDescending(x => x.Order).AsNoTracking().FirstOrDefaultAsync(x => x.InstitutionShortName == _requestContext.InstitutionShortName && x.ProgrammeCode == model.ProgrammeCode);
            model.InstitutionShortName = _requestContext.InstitutionShortName;
            int levelOrder = 0;
            if(academicLevel != null)
            {
                levelOrder = Convert.ToInt32(academicLevel.Order) + 1;
            }
            else
            {
                levelOrder = 1;
            }

            var entity = new AcademicLevel
            {
                LevelName = model.LevelName,
                Description = model.Description,
                InstitutionShortName = model.InstitutionShortName,
                ClassCode = model.ClassCode,
                Order = levelOrder,
                ProgrammeCode = model.ProgrammeCode,
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
            //level.ClassCode = model.ClassCode;
            //level.ProgrammeCode = model.ProgrammeCode;

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
    

        

