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

    public class CourseTypeRepository : ICourseType
    {
        private readonly ApplicationDbContext _context;
        private readonly RequestContext _requestContext;
        private readonly IMapper _mapper;


        public CourseTypeRepository(ApplicationDbContext context, RequestContext requestContext, IMapper mapper)
        {
            _context = context;
            _requestContext = requestContext;
            _mapper = mapper;
        }

        // POST: Create
        public async Task<GeneralResponse> CreateCourseTypeAsync(string institutionShortName, CourseTypeDto dto)
        {
            // 1. Validation: Prevent duplicate names for the same institution
            // We use .ToLower() to ensure "Core" and "core" are treated as the same
            var exists = await _context.CourseTypes.AnyAsync(x =>
                x.Name.ToLower() == dto.Name.ToLower() &&
                x.InstitutionShortName == institutionShortName);

            if (exists)
            {
                return new GeneralResponse
                {
                    StatusCode = 400,
                    Message = $"Course Type '{dto.Name}' already exists for this institution."
                };
            }
           
            var entity = new CourseType
            {
                Name = dto.Name,
                InstitutionShortName = institutionShortName,
                ActiveStatus = dto.ActiveStatus,
                CreatedBy = dto.CreatedBy,
                Created = DateTime.Now
            };

            _context.CourseTypes.Add(entity);
            await _context.SaveChangesAsync();
            
            var courseTypeDto = _mapper.Map<CourseTypeDto>(entity);

            return new GeneralResponse {
                StatusCode = 201,
                Message = "Course Type created successfully",
                Data = courseTypeDto
            };
        }

        // GET: Get By Id
        public async Task<GeneralResponse> GetCourseTypeByIdAsync(long id)
        {
           
            var record = await _context.CourseTypes.FindAsync(id);

            if (record == null)
                return new GeneralResponse {
                    StatusCode = 404,
                    Message = "Not Found"
                };

            var courseTypeDto = _mapper.Map<CourseTypeDto>(record);

            return new GeneralResponse {
                StatusCode = 200,
                Message = "Success",
                Data = courseTypeDto
            };
        }

        // GET ALL: Filtered by School
        public async Task<GeneralResponse> GetAllCourseTypesAsync(string institutionShortName, CourseTypeFilter filter, PagingParameters paging)
        {
            // 1. Initialize the query with Tenant Isolation
            var query = _context.CourseTypes
                .Where(x => x.InstitutionShortName == institutionShortName)
                .AsNoTracking() 
                .AsQueryable();

            // 2. Apply Filtering (Search by Name)
            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                string search = filter.Search.ToLower();
                query = query.Where(x => x.Name.ToLower().Contains(search));
            }

            // 3. Apply Status Filter (Active/Inactive)
            if (filter.ActiveStatus.HasValue)
            {
                query = query.Where(x => x.ActiveStatus == filter.ActiveStatus);
            }

            // 4. Order for consistent results
            query = query.OrderBy(x => x.Name);

            // 5. Get Totl Count 
            var totalRecords = await query.CountAsync();

            // 6.  Pagination
            var items = await query
                .Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize)
                .ToListAsync();
            var courseTypesDto = _mapper.Map<List<CourseTypeDto>>(items);

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Data retrieved successfully",
                Data = courseTypesDto,//batches, // EMPTY LIST if none
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

        // PUT: Update 
        public async Task<GeneralResponse> UpdateCourseTypeAsync(long id, UpdateCourseTypeDto dto)
        {
            var record = await _context.CourseTypes.FindAsync(id);
            if (record == null) return new GeneralResponse { StatusCode = 404, Message = "Not Found" };

            record.Name = dto.Name;
            record.ActiveStatus = dto.ActiveStatus;


            await _context.SaveChangesAsync();

            var courseTypeDto = _mapper.Map<CourseTypeDto>(record);
            return new GeneralResponse {
                StatusCode = 200,
                Message = "Updated",
                Data = courseTypeDto
            };
        }

        // DELETE: Hard Delete
        public async Task<GeneralResponse> DeleteCourseTypeAsync(long id)
        {
            var record = await _context.CourseTypes.FindAsync(id);
            if (record == null) return new GeneralResponse { StatusCode = 404, Message = "Not Found" };

            _context.CourseTypes.Remove(record); // Remove from DB
            await _context.SaveChangesAsync();

            return new GeneralResponse { StatusCode = 200, Message = "Permanently Deleted" };
        }
    }
} 
