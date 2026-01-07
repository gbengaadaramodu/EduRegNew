using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduReg.Services.Repositories
{
    public class ProgrammeFeeScheduleRepository : IProgrammeFeeSchedule
    {
        private readonly ApplicationDbContext _context;

        public ProgrammeFeeScheduleRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<GeneralResponse> GenerateProgrammeFeeSchedulesAsync(string institutionShortName, AcademicContextDto model)
        {
            var feeItems = await _context.FeeItem
                .Where(f => f.InstitutionShortName == model.InstitutionShortName)
                .ToListAsync();

            var programmes = await _context.Programmes
                .Where(p => p.InstitutionShortName == model.InstitutionShortName)
                .ToListAsync();

            var newSchedules = new List<ProgrammeFeeSchedule>();

            foreach (var programme in programmes)
            {
                foreach (var feeItem in feeItems)
                {
                    // Skip semester-based fees if semester is not specified
                    if (feeItem.FeeRecurrenceType == FeeRecurrenceType.PerSemester && model.SemesterId == 0)
                        continue;

                    // Avoid duplicates
                    bool exists = await _context.ProgrammeFeeSchedule.AnyAsync(p =>
                        p.InstitutionShortName == model.InstitutionShortName &&
                        p.ProgrammeCode == programme.ProgrammeCode &&
                        p.FeeItemId == feeItem.Id &&
                        p.SessionId == model.SessionId &&
                        (feeItem.FeeRecurrenceType == FeeRecurrenceType.PerSession ||
                         (feeItem.FeeRecurrenceType == FeeRecurrenceType.PerSemester && p.SemesterId == model.SemesterId))
                    );

                    if (exists) continue;

                    newSchedules.Add(new ProgrammeFeeSchedule
                    {
                        InstitutionShortName = institutionShortName,
                        ProgrammeCode = programme.ProgrammeCode,
                        SessionId = model.SessionId,
                        SemesterId = feeItem.FeeRecurrenceType == FeeRecurrenceType.PerSemester ? model.SemesterId : null,
                        FeeItemId = feeItem.Id,
                        Amount = 0, // could be set from FeeRule later
                        CreatedBy = "System",
                        ActiveStatus = 1
                    });
                }
            }

            _context.ProgrammeFeeSchedule.AddRange(newSchedules);
            await _context.SaveChangesAsync();

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = $"{newSchedules.Count} Programme Fee Schedules generated successfully.",
                Data = newSchedules
            };
        }

        public async Task<GeneralResponse> CreateProgrammeFeeScheduleAsync(ProgrammeFeeScheduleDto model)
        {
            if (string.IsNullOrWhiteSpace(model.InstitutionShortName))
                return new GeneralResponse { StatusCode = 400, Message = "InstitutionShortName is required." };

            if (model.FeeItemId <= 0)
                return new GeneralResponse { StatusCode = 400, Message = "FeeItemId is required." };

            var exists = await _context.ProgrammeFeeSchedule.AnyAsync(p =>
                p.InstitutionShortName == model.InstitutionShortName &&
                p.ProgrammeCode == model.ProgrammeCode &&
                p.SessionId == model.SessionId &&
                p.SemesterId == model.SemesterId &&
                p.FeeItemId == model.FeeItemId);

            if (exists)
                return new GeneralResponse { StatusCode = 409, Message = "Programme Fee Schedule already exists for this fee item." };

            var entity = new ProgrammeFeeSchedule
            {
                InstitutionShortName = model.InstitutionShortName,
                DepartmentCode = model.DepartmentCode,
                ProgrammeCode = model.ProgrammeCode,
                SessionId = model.SessionId,
                SemesterId = model.SemesterId,
                CourseCode = model.CourseCode,
                FeeItemId = model.FeeItemId,
                FeeRuleId = model.FeeRuleId,
                Amount = model.Amount,
                CreatedBy = model.CreatedBy,
                ActiveStatus = model.ActiveStatus
            };

            _context.ProgrammeFeeSchedule.Add(entity);
            await _context.SaveChangesAsync();

            return new GeneralResponse { StatusCode = 201, Message = "Created successfully.", Data = entity };
        }

        public async Task<GeneralResponse> UpdateProgrammeFeeScheduleAsync(long id, ProgrammeFeeScheduleDto model)
        {
            var entity = await _context.ProgrammeFeeSchedule
                .FirstOrDefaultAsync(p => p.Id == id && p.InstitutionShortName == model.InstitutionShortName);

            if (entity == null)
                return new GeneralResponse { StatusCode = 404, Message = "Record not found." };

            entity.DepartmentCode = model.DepartmentCode;
            entity.ProgrammeCode = model.ProgrammeCode;
            entity.SessionId = model.SessionId;
            entity.SemesterId = model.SemesterId;
            entity.CourseCode = model.CourseCode;
            entity.FeeItemId = model.FeeItemId;
            entity.FeeRuleId = model.FeeRuleId;
            entity.Amount = model.Amount;
            entity.ActiveStatus = model.ActiveStatus;

            _context.ProgrammeFeeSchedule.Update(entity);
            await _context.SaveChangesAsync();

            return new GeneralResponse { StatusCode = 200, Message = "Updated successfully.", Data = entity };
        }

        public async Task<GeneralResponse> DeleteProgrammeFeeScheduleAsync(long id, string institutionShortName)
        {
            var entity = await _context.ProgrammeFeeSchedule
                .FirstOrDefaultAsync(p => p.Id == id && p.InstitutionShortName == institutionShortName);

            if (entity == null)
                return new GeneralResponse { StatusCode = 404, Message = "Record not found." };

            _context.ProgrammeFeeSchedule.Remove(entity);
            await _context.SaveChangesAsync();

            return new GeneralResponse { StatusCode = 200, Message = "Deleted successfully." };
        }

        public async Task<GeneralResponse> GetProgrammeFeeScheduleByIdAsync(long id, string institutionShortName)
        {
            var entity = await _context.ProgrammeFeeSchedule
                .Include(p => p.FeeItem)
                .Include(p => p.FeeRule)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id && p.InstitutionShortName == institutionShortName);

            if (entity == null)
                return new GeneralResponse { StatusCode = 404, Message = "Not found." };

            return new GeneralResponse { StatusCode = 200, Message = "Retrieved successfully.", Data = entity };
        }

        public async Task<GeneralResponse> GetAllProgrammeFeeSchedulesAsync(string institutionShortName)
        {
            var list = await _context.ProgrammeFeeSchedule
                .Where(p => p.InstitutionShortName == institutionShortName)
                .Include(p => p.FeeItem)
                .Include(p => p.FeeRule)
                .AsNoTracking()
                .ToListAsync();

            return new GeneralResponse { StatusCode = 200, Message = "Retrieved successfully.", Data = list };
        }

        public async Task<GeneralResponse> GetProgrammeFeeSchedulesByProgrammeAsync(string institutionShortName, string programmeCode)
        {
            var list = await _context.ProgrammeFeeSchedule
                .Where(p => p.InstitutionShortName == institutionShortName && p.ProgrammeCode == programmeCode)
                .Include(p => p.FeeItem)
                .Include(p => p.FeeRule)
                .AsNoTracking()
                .ToListAsync();

            return new GeneralResponse { StatusCode = 200, Message = "Programme Fee Schedules retrieved successfully.", Data = list };
        }

        public async Task<GeneralResponse> GetProgrammeFeeSchedulesByFeeItemAsync(string institutionShortName, int feeItemId)
        {
            var list = await _context.ProgrammeFeeSchedule
                .Where(p => p.InstitutionShortName == institutionShortName && p.FeeItemId == feeItemId)
                .Include(p => p.FeeItem)
                .Include(p => p.FeeRule)
                .AsNoTracking()
                .ToListAsync();

            return new GeneralResponse { StatusCode = 200, Message = "FeeItem-based Programme Fee Schedules retrieved successfully.", Data = list };
        }
    }
}
