using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduReg.Services.Repositories
{
    public class StudentFeeScheduleRepository : IStudentFeeSchedule
    {
         private readonly ApplicationDbContext _context;
        private readonly RequestContext _requestContext;
        public StudentFeeScheduleRepository(ApplicationDbContext context, RequestContext requestContext)
        {
            _context = context;
            _requestContext = requestContext;
        }
        public async Task<GeneralResponse> GenerateStudentFeeSchedulesAsync(string institutionShortName, AcademicContextDto model )
        {
            var programmeSchedule = await _context.ProgrammeFeeSchedule
                .Include(p => p.FeeItem)
                .FirstOrDefaultAsync(p =>
                    p.InstitutionShortName == institutionShortName &&
                    p.ProgrammeCode == model.ProgrammeCode &&
                    p.SessionId == model.SessionId);

            if (programmeSchedule == null)
                return new GeneralResponse { StatusCode = 404, Message = "Programme fee schedule not found." };


            // Fetch all students in this programme
            var students = await _context.Students
                .Where(s => s.InstitutionShortName == institutionShortName &&
                            s.ProgrammeCode == model.ProgrammeCode)
                .ToListAsync();

            if (!students.Any())
                return new GeneralResponse { StatusCode=404, Message= "No students found for this programme." };

            foreach (var student in students)
            {
                // Ensure no duplicate fee schedule for same MatricNumber & Session
                bool exists = await _context.StudentFeeSchedule
                    .AnyAsync(s => s.MatricNumber == student.MatricNumber && s.SessionId == model.SessionId);

                if (exists) continue;

                var studentSchedule = new StudentFeeSchedule
                {
                    InstitutionShortName = institutionShortName,
                    MatricNumber = student.MatricNumber, // UNIQUE IDENTIFIER
                    ProgrammeCode = model.ProgrammeCode,
                    SessionId = model.SessionId,
                    Level = student.CurrentLevel,
                    StudentFeeItems = programmeSchedule.FeeItem.Select(fi => new StudentFeeItem
                    {
                        FeeItemName = fi.Name!,
                        Amount = fi.Amount,
                        IsPaid = false
                    }).ToList()
                };

                studentSchedule.TotalAmount = studentSchedule.StudentFeeItems.Sum(i => i.Amount);
                await _context.StudentFeeSchedule.AddAsync(studentSchedule);
            }

            await _context.SaveChangesAsync();
            return new GeneralResponse { StatusCode = 200, Message = "Individual student fee schedules generated successfully." };
        }

       
    }
}
