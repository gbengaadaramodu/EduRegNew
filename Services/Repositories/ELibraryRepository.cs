using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduReg.Services.Repositories
{
    public class ELibraryRepository : IELibrary
    {
        private readonly ApplicationDbContext _context;

        public ELibraryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GeneralResponse> CreateELibraryAsync(ELibraryDto model)
        {
            var library = new ELibrary
            {
                CourseCode = model.CourseCode,
                ProgramId = model.ProgramId,
                FilePath = model.FilePath,
                Title = model.Title,
                InstitutionShortName = model.InstitutionShortName,
                Created = DateTime.Now,
                CreatedBy = model.CreatedBy,
                ActiveStatus = 1
            };

            _context.ELibraries.Add(library);
            await _context.SaveChangesAsync();
            return new GeneralResponse { StatusCode = 200, Message = "E-Library resource added successfully", Data = library };
        }

        public async Task<GeneralResponse> GetELibraryByIdAsync(long id)
        {
            var item = await _context.ELibraries.FindAsync(id);
            if (item == null) return new GeneralResponse { StatusCode = 404, Message = "Resource not found." };

            return new GeneralResponse { StatusCode = 200, Message = "Success", Data = item };
        }

        

        public async Task<GeneralResponse> GetAllELibraryAsync(PagingParameters paging, string? institutionShortName = null, string? courseCode = null)
        {
            var query = _context.ELibraries.AsQueryable();


            if (!string.IsNullOrEmpty(institutionShortName))
                query = query.Where(x => x.InstitutionShortName == institutionShortName);

         
            if (!string.IsNullOrEmpty(courseCode))
                query = query.Where(x => x.CourseCode.ToLower().Contains(courseCode.ToLower()));

            var data = await query.OrderByDescending(x => x.Created)
                                  .Skip((paging.PageNumber - 1) * paging.PageSize)
                                  .Take(paging.PageSize)
                                  .ToListAsync();

            return new GeneralResponse { StatusCode = 200, Message = "Success", Data = data };
        }

        public async Task<GeneralResponse> UpdateELibraryAsync(long id, ELibraryDto model)
        {
            var existing = await _context.ELibraries.FindAsync(id);
            if (existing == null) return new GeneralResponse { StatusCode = 404, Message = "Resource not found" };

            existing.CourseCode = model.CourseCode;
            existing.ProgramId = model.ProgramId;
            existing.FilePath = model.FilePath;

            await _context.SaveChangesAsync();
            return new GeneralResponse { StatusCode = 200, Message = "Updated successfully", Data = existing };
        }

        public async Task<GeneralResponse> DeleteELibraryAsync(long id)
        {
            var item = await _context.ELibraries.FindAsync(id);
            if (item == null) return new GeneralResponse { StatusCode = 404, Message = "Resource not found" };

            _context.ELibraries.Remove(item);
            await _context.SaveChangesAsync();
            return new GeneralResponse { StatusCode = 200, Message = "Deleted successfully" };
        }
    }
}