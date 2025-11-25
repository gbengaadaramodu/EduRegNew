using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduReg.Services.Repositories
{
    public class StudentFeePaymentServiceRepository : IStudentFeePaymentService
    {
        private readonly ApplicationDbContext _context;

        public StudentFeePaymentServiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GeneralResponse> UpdateStudentPaymentAsync(string matricNumber, List<int> paidItemIds)
        {
            var schedule = await _context.StudentFeeSchedule
                .Include(s => s.StudentFeeItems)
                .FirstOrDefaultAsync(s => s.MatricNumber == matricNumber);

            if (schedule == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = $"No fee schedule found for student {matricNumber}"
                };
            }

            if (schedule.StudentFeeItems == null || !schedule.StudentFeeItems.Any())
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = $"No fee items found for student {matricNumber}"
                };
            }

            // Mark selected items as paid
            foreach (var item in schedule.StudentFeeItems.Where(i => paidItemIds.Contains(i.Id)))
            {
                item.IsPaid = true;
            }

            // Recalculate totals
            schedule.AmountPaid = schedule.StudentFeeItems
                .Where(i => i.IsPaid)
                .Sum(i => i.Amount);

            // No need to manually set OutstandingAmount (it’s [NotMapped])
            await _context.SaveChangesAsync();

            var outstanding = schedule.TotalAmount - schedule.AmountPaid;

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Payment updated successfully.",
                Data = new
                {
                    schedule.MatricNumber,
                    schedule.TotalAmount,
                    schedule.AmountPaid,
                    OutstandingAmount = outstanding,
                    IsFullyPaid = outstanding <= 0,
                    Items = schedule.StudentFeeItems.Select(i => new
                    {
                        i.Id,
                        i.FeeItemName,
                        i.Amount,
                        i.IsPaid
                    })
                }
            };
        }
    }
}
