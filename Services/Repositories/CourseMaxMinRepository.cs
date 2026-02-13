using AutoMapper;
using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduReg.Services.Repositories
{
    public class CourseMaxMinRepository : ICourseMaxMin
    {
        private readonly ApplicationDbContext _context;
        private readonly RequestContext _requestContext;
        private readonly IMapper _mapper;

        public CourseMaxMinRepository(ApplicationDbContext context, RequestContext requestContext, IMapper mapper)
        {
            _context = context;
            _requestContext = requestContext;
            _mapper = mapper;
        }

        // POST: Create
        public async Task<GeneralResponse> CreateCourseMaxMinAsync(string institutionShortName, CourseMaxMinDto dto)
        {
            // 1. Validation
            if (dto.MinimumUnits > dto.MaximumUnits)
            {
                return new GeneralResponse { StatusCode = 400, Message = "Minimum units cannot be greater than maximum units." };
            }

            // 2. Multi-tenant Validation: Ensure the record doesn't already exist for this specific context
            var alreadyExists = await _context.CourseMaxMin.AnyAsync(x =>
                x.InstitutionShortName == institutionShortName &&
                x.ProgramId == dto.ProgramId &&
                x.LevelId == dto.LevelId &&
                x.SemesterId == dto.SemesterId &&
                x.CourseType == dto.CourseType);

            if (alreadyExists)
            {
                return new GeneralResponse { StatusCode = 409, Message = "A unit policy already exists for this program, level, and semester." };
            }

          
            var entity = new CourseMaxMin
            {
                InstitutionShortName = institutionShortName,
                ProgramId = dto.ProgramId,
                LevelId = dto.LevelId,
                SemesterId = dto.SemesterId,
                CourseType = dto.CourseType,
                MinimumUnits = dto.MinimumUnits,
                MaximumUnits = dto.MaximumUnits,
                ActiveStatus = dto.ActiveStatus,
                CreatedBy = dto.CreatedBy,
                Created = DateTime.Now
            };

            _context.CourseMaxMin.Add(entity);
            await _context.SaveChangesAsync();

            var courseMaxMinDto = _mapper.Map<CourseMaxMinDto>(entity);

            return new GeneralResponse {
                StatusCode = 201,
                Message = "Course unit policy created successfully",
                Data = courseMaxMinDto 
            };
        }

        // GET: By ID
        public async Task<GeneralResponse> GetCourseMaxMinByIdAsync(long id)
        {
            var record = await _context.CourseMaxMin
                .FirstOrDefaultAsync(x => x.Id == id);

            if (record == null)
                return new GeneralResponse {
                    StatusCode = 404,
                    Message = "Policy not found" };

            var courseMaxMinDto = _mapper.Map<CourseMaxMinDto>(record);

            return new GeneralResponse {
                StatusCode = 200,
                Message = "Success",
                Data = courseMaxMinDto
            };
        }


        public async Task<GeneralResponse> GetAllCourseMaxMinAsync(string institutionShortName, CourseMaxMinFilter filter, PagingParameters paging)
        {
            // 1. Base Query (Tenant Isolation)
            var query = _context.CourseMaxMin
                .Where(x => x.InstitutionShortName == institutionShortName)
                .AsNoTracking() 
                .AsQueryable();

            // 2. Apply Filters 
            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                string search = filter.Search.ToLower();
                query = query.Where(x =>
                    x.CourseType.ToLower().Contains(search) ||
                    x.ProgramId.ToString().Contains(search));
            }

            if (filter.ProgramId.HasValue)
                query = query.Where(x => x.ProgramId == filter.ProgramId);

           

            // 3. Pagination Logic
            var pagedData = await query
                .Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize)
                .ToListAsync();

            // 4. (Optional) Get Total Count for UI progress bars
            var totalRecords = await query.CountAsync();

            var courseMaxMinsDto = _mapper.Map<List<CourseMaxMinDto>>(pagedData);


            return new GeneralResponse
            {
                StatusCode = 200,
                Message = pagedData.Any() ? "Data retrieved successfully" : "No records found matching criteria",
                Data = courseMaxMinsDto,
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

        // PUT: Update by ID
        public async Task<GeneralResponse> UpdateCourseMaxMinAsync(long id, UpdateCourseMaxMinDto dto)
        {
            var found = await _context.CourseMaxMin.FindAsync(id);

            if (found == null)
                return new GeneralResponse { StatusCode = 404, Message = "Policy not found" };

            // Logic check 
            if (dto.MinimumUnits > dto.MaximumUnits)
                return new GeneralResponse { StatusCode = 400, Message = "Validation failed: Min units > Max units" };

            found.MinimumUnits = dto.MinimumUnits;
            found.MaximumUnits = dto.MaximumUnits;
            found.ActiveStatus = dto.ActiveStatus;
            

            await _context.SaveChangesAsync();

            var courseMaxMinDto = _mapper.Map<CourseMaxMinDto>(found);
            return new GeneralResponse {
                StatusCode = 200,
                Message = "Policy updated successfully",
                Data = courseMaxMinDto };
        }

       
        public async Task<GeneralResponse> DeleteCourseMaxMinAsync(long id)
        {
            var record = await _context.CourseMaxMin.FindAsync(id);

            if (record == null)
                return new GeneralResponse { StatusCode = 404, Message = "Policy not found" };

            // Soft Delete
           
            record.ActiveStatus = 0;

            await _context.SaveChangesAsync();
            return new GeneralResponse { StatusCode = 200, Message = "Policy deleted successfully" };
        }
    }
}
