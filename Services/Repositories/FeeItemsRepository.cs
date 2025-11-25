using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduReg.Services.Repositories
{
    public class FeeItemsRepository : IFeeItems
    {
        private readonly ApplicationDbContext _context;

        public FeeItemsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ Create a new Fee Item
        public async Task<GeneralResponse> CreateFeeItemAsync(FeeItemDto model)
        {
            var existingItem = await _context.FeeItem
                .FirstOrDefaultAsync(x => x.Name == model.Name &&
                                          x.InstitutionShortName == model.InstitutionShortName);

            if (existingItem != null)
            {
                return new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "FeeItem already exists for this institution."
                };
            }

            var item = new FeeItem
            {
                InstitutionShortName = model.InstitutionShortName,
                Name = model.Name,
                FeeCategory = model.FeeCategory,
                FeeApplicabilityScope = model.FeeApplicabilityScope,
                FeeRecurrenceType = model.FeeRecurrenceType,
                Description = model.Description, 
                Amount = model.Amount,
                IsSystemDefined = model.IsSystemDefined,
                Created= model.Created,
                CreatedBy = model.CreatedBy,
                ActiveStatus = model.ActiveStatus,
                FeeRules = model.FeeRules
                
            };

            _context.FeeItem.Add(item);
            await _context.SaveChangesAsync();

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Created successfully.",
                Data = item
            };
        }


        // ✅ Get all Fee Items
        public async Task<GeneralResponse> GetAllFeeItemsAsync()
        {
            var items = await _context.FeeItem
                .AsNoTracking()
                .Include(x => x.FeeRules)
                .ToListAsync();

            if (!items.Any())
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "No fee items found."
                };
            }

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Success",
                Data = items
            };
        }


        // ✅ Get Fee Item by Id
        public async Task<GeneralResponse> GetFeeItemByIdAsync(int Id)
        {
            var item = await _context.FeeItem
                .Include(x => x.FeeRules)
                .FirstOrDefaultAsync(x => x.Id == Id);

            if (item == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "Fee item not found."
                };
            }

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Success",
                Data = item
            };
        }


        // ✅ Update Fee Item
        public async Task<GeneralResponse> UpdateFeeItemAsync(int Id, FeeItemDto model)
        {
            var existingItem = await _context.FeeItem.FindAsync(Id);

            if (existingItem == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "Fee item not found."
                };
            }

            // Prevent updating system-defined items
            if (existingItem.IsSystemDefined)
            {
                return new GeneralResponse
                {
                    StatusCode = 403,
                    Message = "System-defined fee items cannot be updated."
                };
            }

            existingItem.Name = model.Name ?? existingItem.Name;
            existingItem.Description = model.Description ?? existingItem.Description;
            existingItem.FeeCategory = model.FeeCategory;
            existingItem.ActiveStatus = model.ActiveStatus;
            existingItem.Amount = model.Amount;
            existingItem.Created = model.Created;
            existingItem.FeeApplicabilityScope = model.FeeApplicabilityScope;
            existingItem.FeeRecurrenceType = model.FeeRecurrenceType;
            existingItem.FeeRules = model.FeeRules;
            _context.Entry(existingItem).State = EntityState.Modified;
            _context.FeeItem.Update(existingItem);
            await _context.SaveChangesAsync();

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Updated successfully.",
                Data = existingItem
            };
        }


        // ✅ Delete Fee Item
        public async Task<GeneralResponse> DeleteFeeItemAsync(int Id)
        {
            var item = await _context.FeeItem.FindAsync(Id);

            if (item == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "Fee item not found."
                };
            }

            if (item.IsSystemDefined)
            {
                return new GeneralResponse
                {
                    StatusCode = 403,
                    Message = "System-defined fee items cannot be deleted."
                };
            }

            _context.FeeItem.Remove(item);
            await _context.SaveChangesAsync();

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Deleted successfully."
            };
        }


        // ✅ Add Fee Items to a Semester Schedule
        // This will create FeeRules linking FeeItems to a Semester & Session context
        public async Task<GeneralResponse> AddFeeItemToSemesterScheduleAsync(List<FeeItemDto> models)
        {
            if (models == null || models.Count == 0)
            {
                return new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "No fee items provided."
                };
            }

            var createdRules = new List<FeeRule>();

            foreach (var dto in models)
            {
                var feeItem = await _context.FeeItem
                    .FirstOrDefaultAsync(x => x.Name == dto.Name &&
                                              x.InstitutionShortName == dto.InstitutionShortName);

                if (feeItem == null)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 404,
                        Message = $"FeeItem '{dto.Name}' not found for institution {dto.InstitutionShortName}."
                    };
                }

                // Build a FeeRule
                var rule = new FeeRule
                {
                    FeeItemId = feeItem.Id,
                    InstitutionShortName = dto.InstitutionShortName,
                    ProgrammeCode = dto.FeeRules?.FirstOrDefault()?.ProgrammeCode,
                    DepartmentCode = dto.FeeRules?.FirstOrDefault()?.DepartmentCode,
                    LevelName = dto.FeeRules?.FirstOrDefault()?.LevelName,
                    ClassCode = dto.FeeRules?.FirstOrDefault()?.ClassCode,
                    SessionId = dto.FeeRules?.FirstOrDefault()?.SessionId,
                    SemesterId = dto.FeeRules?.FirstOrDefault()?.SemesterId,
                    Amount = dto.FeeRules?.FirstOrDefault()?.Amount ?? 0,
                    IsRecurring = dto.FeeRules?.FirstOrDefault()?.IsRecurring ?? false,
                    RecurrenceType = dto.FeeRules?.FirstOrDefault()?.RecurrenceType ?? FeeRecurrenceType.OneTime,
                    ApplicabilityScope = dto.FeeRules?.FirstOrDefault()?.ApplicabilityScope ?? FeeApplicabilityScope.InstitutionWide,
                    EffectiveFrom = dto.FeeRules?.FirstOrDefault()?.EffectiveFrom ?? DateTime.Now,
                    EffectiveTo = dto.FeeRules?.FirstOrDefault()?.EffectiveTo,
                    CreatedBy = dto.CreatedBy,
                    ActiveStatus = dto.ActiveStatus
                };

                _context.FeeRule.Add(rule);
                createdRules.Add(rule);
            }

            await _context.SaveChangesAsync();

            return new GeneralResponse
            {
                StatusCode = 201,
                Message = "Fee items added to semester schedule successfully.",
                Data = createdRules
            };
        }
    }
}
