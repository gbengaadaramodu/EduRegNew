using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduReg.Services.Repositories
{
    public class CourseMaxMinRepository : ICourseMaxMin
    {
        private readonly ApplicationDbContext _context;

        public CourseMaxMinRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: Create
        public async Task<GeneralResponse> CreateCourseMaxMinAsync(CourseMaxMinDto dto)
        {
            // Business Rule: Ensure Min isn't higher than Max
            if (dto.MinimumUnits > dto.MaximumUnits)
            {
                return new GeneralResponse { StatusCode = 400, Message = "Minimum units cannot be greater than maximum units." };
            }

            var entity = new CourseMaxMin
            {
                InstitutionShortName = dto.InstitutionShortName,
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

            return new GeneralResponse { StatusCode = 201, Message = "Course unit policy created successfully", Data = entity };
        }

        // GET: By ID
        public async Task<GeneralResponse> GetCourseMaxMinByIdAsync(long id)
        {
            var record = await _context.CourseMaxMin
                .FirstOrDefaultAsync(x => x.Id == id);

            if (record == null)
                return new GeneralResponse { StatusCode = 404, Message = "Policy not found" };

            return new GeneralResponse { StatusCode = 200, Message = "Success", Data = record };
        }

        // GET: All (Filtered by School)
        public async Task<GeneralResponse> GetAllCourseMaxMinAsync(string institutionShortName)
        {
            var list = await _context.CourseMaxMin
                .Where(x => x.InstitutionShortName == institutionShortName)
                .ToListAsync();

            return new GeneralResponse { StatusCode = 200, Message = "Data retrieved successfully", Data = list };
        }

        // PUT: Update by ID
        public async Task<GeneralResponse> UpdateCourseMaxMinAsync(long id, UpdateCourseMaxMinDto dto)
        {
            var found = await _context.CourseMaxMin.FindAsync(id);

            if (found == null)
                return new GeneralResponse { StatusCode = 404, Message = "Policy not found" };

            // Logic check for the update
            if (dto.MinimumUnits > dto.MaximumUnits)
                return new GeneralResponse { StatusCode = 400, Message = "Validation failed: Min units > Max units" };

            found.MinimumUnits = dto.MinimumUnits;
            found.MaximumUnits = dto.MaximumUnits;
            found.ActiveStatus = dto.ActiveStatus;
            // Note: We don't change ProgramId or LevelId usually in an update

            await _context.SaveChangesAsync();
            return new GeneralResponse { StatusCode = 200, Message = "Policy updated successfully", Data = found };
        }

        // DELETE: Soft Delete by ID
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
