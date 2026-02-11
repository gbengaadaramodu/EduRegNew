using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduReg.Services.Repositories
{
    public class TicketingRepository : ITicketing
    {
        private readonly ApplicationDbContext _context;
        // private readonly IEmailService _emailService; // Ill Inject this when ready

        public TicketingRepository(ApplicationDbContext context )
        {
            _context = context;
        }

        public async Task<GeneralResponse> CreateTicketAsync(string institutionShortName, TicketDto dto)
        {
            //. 1. Generate the Reference Number
            string refNumber = $"TIC-{DateTime.Now.Year}-{Guid.NewGuid().ToString().Substring(0, 5).ToUpper()}";

            // 2. Map DTO to Entity
            var ticket = new Ticketing
            {
                InstitutionShortName = institutionShortName,
                StudentName = dto.StudentName,
                Title = dto.Title,
                MessageBody = dto.MessageBody,
                ReferenceNumber = refNumber,
                TicketStatus = "Open",
                Created = DateTime.Now,
                ActiveStatus = 1
            };

            try
            {
                _context.Ticketing.Add(ticket);
                await _context.SaveChangesAsync();

                // 3. TODO: Email Notification to Student
                //string subject = "Ticket Received: " + refNumber;
                //string body = $"Hello {dto.StudentName}, your ticket '{dto.Title}' has been received. Your Ref is {refNumber}.";

                //await _emailService.SendEmailAsync(studentEmail, subject, body);

                return new GeneralResponse
                {
                    StatusCode = 201,
                    Message = $"Ticket created successfully. Reference: {refNumber}",
                    Data = ticket
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse { StatusCode = 500, Message = "Error saving ticket." };
            }
        }

        public async Task<GeneralResponse> RespondToTicketAsync(long id, RespondToTicketDto dto)
        {
            var ticket = await _context.Ticketing.FindAsync(id);
            if (ticket == null)
                return new GeneralResponse { StatusCode = 404, Message = "Ticket not found." };

            // Update fields
            ticket.ResponseBody = dto.ResponseBody;
            ticket.TicketStatus = dto.TicketStatus; // Defaults to "Closed"
            ticket.DateOfResponse = DateTime.Now;

            await _context.SaveChangesAsync();

            // 4. TODO: Email Notification of Response
            // string subject = "Update on Ticket: " + ticket.ReferenceNumber;
            // string body = $"Hello {ticket.StudentName}, an admin has responded: {dto.ResponseBody}";
            // await _emailService.SendEmailAsync(studentEmail, subject, body);

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Response sent and ticket closed.",
                Data = ticket
            };
        }

        public async Task<GeneralResponse> GetTicketByIdAsync(long id)
        {
            var ticket = await _context.Ticketing.FindAsync(id);
            if (ticket == null) return new GeneralResponse { StatusCode = 404, Message = "Ticket not found." };

            return new GeneralResponse { StatusCode = 200, Data = ticket };
        }

        public async Task<GeneralResponse> GetAllTicketsAsync(string institutionShortName, TicketingFilter filter, PagingParameters paging)
        {
            var query = _context.Ticketing
                .Where(x => x.InstitutionShortName == institutionShortName)
                .AsNoTracking()
                .AsQueryable();

            // Filter by Status (Open/Closed)
            if (!string.IsNullOrWhiteSpace(filter.Status))
                query = query.Where(x => x.TicketStatus == filter.Status);

            // Search logic
            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                var search = filter.Search.ToLower();
                query = query.Where(x => x.StudentName.ToLower().Contains(search) ||
                                         x.ReferenceNumber.ToLower().Contains(search));
            }

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderByDescending(x => x.Created)
                .Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize)
                .ToListAsync();

            return new GeneralResponse
            {
                StatusCode = 200,
                Data = new { TotalCount = totalCount, Items = items }
            };
        }
    }
}
