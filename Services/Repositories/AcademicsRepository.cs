using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
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
                    StatusCore = 400,
                    Message = "Invalid academic level data",
                    Data = null
                };
            }

            var entity = new AcademicLevel
            {
                LevelName = model.LevelName,
                Description = model.Description
            };

            await _context.AcademicLevels.AddAsync(entity);
            await _context.SaveChangesAsync();

            return new GeneralResponse
            {
                StatusCore = 201,
                Message = "Academic level created successfully",
                Data = entity
            };
        }

        public async Task<GeneralResponse> GetAllAcademicLevelAsync()
        {
            var levels = await _context.AcademicLevels.ToListAsync();

            if (!levels.Any())
            {
                return new GeneralResponse
                {
                    StatusCore = 404,
                    Message = "No academic levels found",
                    Data = null
                };
            }

            return new GeneralResponse
            {
                StatusCore = 200,
                Message = "Academic levels retrieved successfully",
                Data = levels
            };
        }

        public async Task<GeneralResponse> GetAcademicLevelByIdAsync(int Id)
        {
            if (Id <= 0)
            {
                return new GeneralResponse
                {
                    StatusCore = 400,
                    Message = "Invalid ID",
                    Data = null
                };
            }

            var level = await _context.AcademicLevels.FindAsync(Id);
            if (level == null)
            {
                return new GeneralResponse
                {
                    StatusCore = 404,
                    Message = "Academic level not found",
                    Data = null
                };
            }

            return new GeneralResponse
            {
                StatusCore = 200,
                Message = "Academic level retrieved successfully",
                Data = level
            };
        }


        public async Task<GeneralResponse> UpdateAcademicLevelAsync(int Id, AcademicLevelsDto model)
        {
             
            
            var level = await _context.AcademicLevels.FindAsync(Id);

            if (level == null)
            {
                return new GeneralResponse
                {
                    StatusCore = 404,
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
                StatusCore = 200,
                Message = "Academic level updated successfully",
                Data = level
            };
        }


        public async Task<GeneralResponse> DeleteAcademicLevelAsync(int Id)
        {
            var level = await _context.AcademicLevels.FindAsync(Id);

            if (level == null)
            {
                return new GeneralResponse
                {
                    StatusCore = 404,
                    Message = "Academic level not found",
                    Data = null
                };
            }

            _context.AcademicLevels.Remove(level);
            await _context.SaveChangesAsync();

            return new GeneralResponse
            {
                StatusCore = 200,
                Message = "Academic level deleted successfully",
                Data = null
            };
        }

    }

}
    

        

