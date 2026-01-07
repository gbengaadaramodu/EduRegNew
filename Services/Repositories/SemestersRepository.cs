using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduReg.Services.Repositories
{
    public class SemestersRepository : ISemesters
    {
        private readonly ApplicationDbContext _context;
        public SemestersRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<GeneralResponse> CreateSemesterAsync(SemestersDto model)
        {
            if (model == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "Invalid semester data",
                    Data = null
                };
            }

            var entity = new Semesters
            {
                InstitutionShortName = model.InstitutionShortName,
                SessionId = model.SessionId,
                SemesterName = model.SemesterName,
                SemesterId = model.SemesterId,
                StartDate = model.StartDate,
                EndDate = model.EndDate
            };

            await _context.Semesters.AddAsync(entity);
            await _context.SaveChangesAsync();

            return new GeneralResponse
            {
                StatusCode = 201,
                Message = "Semester created successfully",
                Data = entity
            };
        }

        public async Task<GeneralResponse> DeleteSemesterAsync(long Id)
        {
            var semester = await _context.Semesters.FindAsync(Id);

            if (semester == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "Semester not found",
                    Data = null
                };
            }

            _context.Semesters.Remove(semester);
            await _context.SaveChangesAsync();

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Semester deleted successfully",
                Data = null
            };
        }

        public async Task<GeneralResponse> GetAllSemestersAsync()
        {
            var semesters = await _context.Semesters.ToListAsync();

            if (!semesters.Any())
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "No semesters found",
                    Data = null
                };
            }

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Semesters retrieved successfully",
                Data = semesters
            };
        }

        public async Task<GeneralResponse> GetSemesterByIdAsync(long Id)
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

            var semester = await _context.Semesters.FindAsync(Id);
            if (semester == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "Semester not found",
                    Data = null
                };
            }

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Semester retrieved successfully",
                Data = semester
            };
        }

        public async Task<GeneralResponse> UpdateSemesterAsync(long Id, SemestersDto model)
        {
            var semester = await _context.Semesters.FindAsync(Id);

            if (semester == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "Semester not found",
                    Data = null
                };
            }

            semester.InstitutionShortName = model.InstitutionShortName;
            semester.SessionId = model.SessionId;
            semester.SemesterName = model.SemesterName;
            semester.SemesterId = model.SemesterId;
            semester.StartDate = model.StartDate;
            semester.EndDate = model.EndDate;

            _context.Semesters.Update(semester);
            await _context.SaveChangesAsync();

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Semester updated successfully",
                Data = semester
            };
        }
    }
}
