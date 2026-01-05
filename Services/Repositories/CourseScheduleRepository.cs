using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;

namespace EduReg.Services.Repositories
{
    public class CourseScheduleRepository : ICourseSchedule
    {
        private readonly ApplicationDbContext _context;

        public CourseScheduleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GeneralResponse> CreateCourseScheduleAsync(CourseScheduleDto model)
        {
            try
            {
                // Optional: Validate Model (e.g. Department exists, SessionId valid etc.)

                var entity = MapDtoToEntity(model);

                await _context.CourseSchedules.AddAsync(entity);
                await _context.SaveChangesAsync();

                return new GeneralResponse
                {
                    StatusCode = 201,
                    Message = "Course schedule created successfully.",
                    Data = entity
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"Error when creating course schedule: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<GeneralResponse> CreateCourseScheduleAsync(List<CourseScheduleDto> models)
        {
            try
            {
                var entities = models.Select(MapDtoToEntity).ToList();
                await _context.CourseSchedules.AddRangeAsync(entities);
                await _context.SaveChangesAsync();

                return new GeneralResponse
                {
                    StatusCode = 201,
                    Message = "Course schedules created successfully.",
                    Data = entities
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"Error when creating course schedules in bulk: {ex.Message}",
                    Data = null
                };
            }
        }
 

        public async Task<GeneralResponse> UpdateCourseScheduleAsync(long id, CourseScheduleDto model)
        {
            try
            {
                var entity = await _context.CourseSchedules.FindAsync(id);
                if (entity == null)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 404,
                        Message = "Course schedule not found.",
                        Data = null
                    };
                }

                // Update fields
                entity.InstitutionShortName = model.InstitutionShortName;
                entity.DepartmentCode = model.DepartmentCode;
                entity.ProgrammeCode = model.ProgrammeCode;
                entity.ClassCode = model.ClassCode;
                entity.LevelName = model.LevelName;
                entity.CourseCode = model.CourseCode;
                entity.Title = model.Title;
                entity.Units = model.Units;
                entity.CourseType = model.CourseType;
                entity.Prerequisite = model.Prerequisite;
                entity.BatchShortName = model.BatchShortName;
                entity.SessionId = model.SessionId;
                entity.SemesterId = model.SemesterId;
                entity.CourseFee = model.CourseFee;
                entity.LMSId = model.LMSId;
                entity.IsOnLMS = model.IsOnLMS;

                // If ActiveStatus or other common fields are in Dto / Entities:
                // entity.ActiveStatus = model.ActiveStatus;

                _context.CourseSchedules.Update(entity);
                await _context.SaveChangesAsync();

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "Course schedule updated successfully.",
                    Data = entity
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"Error when updating course schedule: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<GeneralResponse> DeleteCourseScheduleAsync(long id)
        {
            try
            {
                var entity = await _context.CourseSchedules.FindAsync(id);
                if (entity == null)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 404,
                        Message = "Course schedule not found.",
                        Data = null
                    };
                }

                _context.CourseSchedules.Remove(entity);
                await _context.SaveChangesAsync();

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "Course schedule deleted successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"Error when deleting course schedule: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<GeneralResponse> DeleteManyCourseSchedulesAsync(List<CourseScheduleDto> model)
        {
            try
            {
                // Option A: Delete by matching all fields in DTO
                // Option B: Use IDs (if present in the DTO)
                // I'll assume DTO doesn't have ID, so match by unique combinations. Adjust accordingly.

                var toRemove = new List<CourseSchedule>();

                foreach (var dto in model)
                {
                    var matched = await _context.CourseSchedules.FirstOrDefaultAsync(cs =>
                        cs.InstitutionShortName == dto.InstitutionShortName
                        && cs.DepartmentCode == dto.DepartmentCode
                        && cs.ProgrammeCode == dto.ProgrammeCode
                        && cs.CourseCode == dto.CourseCode
                        && cs.ClassCode == dto.ClassCode
                        && cs.SessionId == dto.SessionId
                        && cs.SemesterId == dto.SemesterId
                    );

                    if (matched != null)
                        toRemove.Add(matched);
                }

                if (!toRemove.Any())
                {
                    return new GeneralResponse
                    {
                        StatusCode = 404,
                        Message = "No matching course schedules found to delete.",
                        Data = null
                    };
                }

                _context.CourseSchedules.RemoveRange(toRemove);
                await _context.SaveChangesAsync();

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = $"{toRemove.Count} course schedules deleted successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"Error when deleting many course schedules: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<GeneralResponse> DeleteManyCourseSchedulesAsync(List<long> Ids)
        {
            try
            {
                var entities = await _context.CourseSchedules
                    .Where(cs => Ids.Contains(cs.Id))
                    .ToListAsync();

                if (!entities.Any())
                {
                    return new GeneralResponse
                    {
                        StatusCode = 404,
                        Message = "No course schedules found for the supplied Ids.",
                        Data = null
                    };
                }

                _context.CourseSchedules.RemoveRange(entities);
                await _context.SaveChangesAsync();

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = $"{entities.Count} course schedules deleted successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"Error when deleting many schedules by Ids: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<GeneralResponse> GetCourseScheduleByIdAsync(long id)
        {
            try
            {
                var entity = await _context.CourseSchedules.FindAsync(id);
                if (entity == null)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 404,
                        Message = "Course schedule not found.",
                        Data = null
                    };
                }

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "Success",
                    Data = entity
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"Error retrieving course schedule: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<GeneralResponse> GetCourseScheduleByCourseCodeAsync(string courseCode)
        {
            try
            {
                var entities = await _context.CourseSchedules
                    .Where(cs => cs.CourseCode == courseCode)
                    .ToListAsync();

                if (!entities.Any())
                {
                    return new GeneralResponse
                    {
                        StatusCode = 404,
                        Message = "No course schedules found for the given course code.",
                        Data = null
                    };
                }

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "Success",
                    Data = entities
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"Error when retrieving by course code: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<GeneralResponse> GetAllCourseSchedulesAsync()
        {
            try
            {
                var entities = await _context.CourseSchedules.ToListAsync();

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "Success",
                    Data = entities
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"Error retrieving all course schedules: {ex.Message}",
                    Data = null
                };
            }
        }


        // Private helper to map DTO → Entity
        private CourseSchedule MapDtoToEntity(CourseScheduleDto dto)
        {
            return new CourseSchedule
            {
                InstitutionShortName = dto.InstitutionShortName,
                DepartmentCode = dto.DepartmentCode,
                ProgrammeCode = dto.ProgrammeCode,
                SessionId = dto.SessionId,
                SemesterId = dto.SemesterId,
                CourseCode = dto.CourseCode,
                ClassCode = dto.ClassCode,
                LevelName = dto.LevelName,
                Title = dto.Title,
                Units = dto.Units,
                CourseType = dto.CourseType,
                Prerequisite = dto.Prerequisite,
                BatchShortName = dto.BatchShortName,
                CourseFee = dto.CourseFee,
                LMSId = dto.LMSId,
                IsOnLMS = dto.IsOnLMS,
                // If you have Created, CreatedBy, ActiveStatus in entity
                Created = dto.Created,
                CreatedBy = dto.CreatedBy,
                ActiveStatus = dto.ActiveStatus
            };
        }
    }
}
