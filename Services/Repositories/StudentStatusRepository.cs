using AutoMapper;
using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EduReg.Services.Repositories
{
    public class StudentStatusRepository : IStudentStatus
    {
        private readonly ApplicationDbContext _context;
        private readonly RequestContext _requestContext;
        private readonly IMapper _mapper;

        public StudentStatusRepository(ApplicationDbContext context, RequestContext requestContext, IMapper mapper)
        {
            _context = context;
            _requestContext = requestContext;
            _mapper = mapper;
        }

        public async Task<GeneralResponse> CreateStudentStatusAsync(StudentStatusDto model)
        {
            var exists = await _context.StudentStatuses
                .AnyAsync(x => x.Name.ToLower() == model.Name.ToLower() &&
                               x.InstitutionShortName == _requestContext.InstitutionShortName);

            if (exists)
            {
                return new GeneralResponse { StatusCode = 400, Message = "Student Status already exists." };
            }

            var status = new StudentStatus
            {
                Name = model.Name,
                IsActive = model.IsActive,
                InstitutionShortName = _requestContext.InstitutionShortName,
                Created = DateTime.Now,
                CreatedBy = model.CreatedBy,
                ActiveStatus = model.ActiveStatus
            };

            _context.StudentStatuses.Add(status);
            await _context.SaveChangesAsync();

            var statusDto = _mapper.Map<StudentStatusDto>(status);

            return new GeneralResponse { StatusCode = 200, Message = "Created successfully.", Data = statusDto };
        }

        public async Task<GeneralResponse> GetAllStudentStatusAsync(PagingParameters paging, string? institutionShortName = null)
        {
            var query = _context.StudentStatuses.AsQueryable();

            
            query = query.Where(x => x.InstitutionShortName == _requestContext.InstitutionShortName);
            

            var totalRecords = await query.CountAsync();
            var data = await query
                .OrderBy(x => x.Name)
                .Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize)
                .ToListAsync();

            var Dto = _mapper.Map<StudentStatusDto>(data);

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Success",
                Data = Dto,
                Meta = new
                {
                    paging.PageNumber,
                    paging.PageSize,
                    TotalRecords = totalRecords,
                    TotalPages = (int)Math.Ceiling(totalRecords / (double)paging.PageSize)
                }
            };
        }

        public async Task<GeneralResponse> GetStudentStatusByIdAsync(long id)
        {
            var item = await _context.StudentStatuses.FirstOrDefaultAsync(x => x.Id == id && x.InstitutionShortName == _requestContext.InstitutionShortName);
            if (item == null) return new GeneralResponse { StatusCode = 404, Message = "Not found." };

            var Dto = _mapper.Map<StudentStatusDto>(item);

            return new GeneralResponse { StatusCode = 200, Message = "Success", Data = Dto };
        }

        public async Task<GeneralResponse> UpdateStudentStatusAsync(long id, StudentStatusDto model)
        {
            var existing = await _context.StudentStatuses.FirstOrDefaultAsync(x=>x.Id == id && x.InstitutionShortName == _requestContext.InstitutionShortName);
            if (existing == null) return new GeneralResponse { StatusCode = 404, Message = "Not found." };

            existing.Name = model.Name;
            existing.IsActive = model.IsActive;
            existing.ActiveStatus = model.ActiveStatus;

            await _context.SaveChangesAsync();

            var Dto = _mapper.Map<StudentStatusDto>(existing);
            return new GeneralResponse { StatusCode = 200, Message = "Updated successfully.", Data = Dto};
        }

        public async Task<GeneralResponse> DeleteStudentStatusAsync(long id)
        {
            var item = await _context.StudentStatuses.FirstOrDefaultAsync(x => x.Id == id && x.InstitutionShortName == _requestContext.InstitutionShortName);
            if (item == null) return new GeneralResponse { StatusCode = 404, Message = "Not found." };

            _context.StudentStatuses.Remove(item);
            await _context.SaveChangesAsync();
            return new GeneralResponse { StatusCode = 200, Message = "Deleted successfully." };
        }
    }
}