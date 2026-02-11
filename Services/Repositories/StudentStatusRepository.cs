using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduReg.Services.Repositories
{
    public class StudentStatusRepository : IStudentStatus
    {
        private readonly ApplicationDbContext _context;

        public StudentStatusRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GeneralResponse> CreateStudentStatusAsync(StudentStatusDto model)
        {
            var exists = await _context.StudentStatuses
                .AnyAsync(x => x.Name.ToLower() == model.Name.ToLower() &&
                               x.InstitutionShortName == model.InstitutionShortName);

            if (exists)
            {
                return new GeneralResponse { StatusCode = 400, Message = "Student Status already exists." };
            }

            var status = new StudentStatus
            {
                Name = model.Name,
                IsActive = model.IsActive,
                InstitutionShortName = model.InstitutionShortName,
                Created = DateTime.Now,
                CreatedBy = model.CreatedBy,
                ActiveStatus = model.ActiveStatus
            };

            _context.StudentStatuses.Add(status);
            await _context.SaveChangesAsync();

            return new GeneralResponse { StatusCode = 200, Message = "Created successfully.", Data = status };
        }

        public async Task<GeneralResponse> GetAllStudentStatusAsync(PagingParameters paging, string? institutionShortName = null)
        {
            var query = _context.StudentStatuses.AsQueryable();

            if (!string.IsNullOrWhiteSpace(institutionShortName))
            {
                query = query.Where(x => x.InstitutionShortName == institutionShortName);
            }

            var totalRecords = await query.CountAsync();
            var data = await query
                .OrderBy(x => x.Name)
                .Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize)
                .ToListAsync();

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Success",
                Data = data,
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
            var item = await _context.StudentStatuses.FindAsync(id);
            if (item == null) return new GeneralResponse { StatusCode = 404, Message = "Not found." };

            return new GeneralResponse { StatusCode = 200, Message = "Success", Data = item };
        }

        public async Task<GeneralResponse> UpdateStudentStatusAsync(long id, StudentStatusDto model)
        {
            var existing = await _context.StudentStatuses.FindAsync(id);
            if (existing == null) return new GeneralResponse { StatusCode = 404, Message = "Not found." };

            existing.Name = model.Name;
            existing.IsActive = model.IsActive;
            existing.ActiveStatus = model.ActiveStatus;

            await _context.SaveChangesAsync();
            return new GeneralResponse { StatusCode = 200, Message = "Updated successfully.", Data = existing };
        }

        public async Task<GeneralResponse> DeleteStudentStatusAsync(long id)
        {
            var item = await _context.StudentStatuses.FindAsync(id);
            if (item == null) return new GeneralResponse { StatusCode = 404, Message = "Not found." };

            _context.StudentStatuses.Remove(item);
            await _context.SaveChangesAsync();
            return new GeneralResponse { StatusCode = 200, Message = "Deleted successfully." };
        }
    }
}