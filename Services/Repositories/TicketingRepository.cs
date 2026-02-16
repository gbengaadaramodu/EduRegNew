using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace EduReg.Services.Repositories
{
    public class TicketingRepository : ITicketing
    {
        private readonly ApplicationDbContext _context;
        private readonly RequestContext _requestContext;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public TicketingRepository(ApplicationDbContext context, RequestContext requestContext, IEmailService emailService, IMapper mapper)
        {
            _context = context;
            _requestContext = requestContext;
            _emailService = emailService;
            _mapper = mapper;
        }

        public async Task<GeneralResponse> CreateTicketAsync(string institutionShortName, TicketDto dto)
        {
            var student = await _context.Students.AsNoTracking().FirstOrDefaultAsync(a => a.MatricNumber == dto.MatricNumber && a.InstitutionShortName == _requestContext.InstitutionShortName);

            if (dto == null)
            {
                return new GeneralResponse { StatusCode = 400, Message = "Request data is empty." };
            }

            if (string.IsNullOrWhiteSpace(dto.MessageBody))
            {
                return new GeneralResponse { StatusCode = 400, Message = "Message body is empty." };
            }

            if (student == null)
            {
                return new GeneralResponse { StatusCode = 404, Message = "Student with this Matric Number not found." };
            }



            //. 1. Generate the Reference Number
            string refNumber = $"TIC-{DateTime.Now.Year}-{Guid.NewGuid().ToString().Substring(0, 5).ToUpper()}";


            string fullName = $"{student.FirstName} {student.LastName}";



            // 2. Map DTO to Entity
            var ticket = new Ticketing
            {
                MatricNumber = dto.MatricNumber,
              //  StudentId = dto.StudentId,
                InstitutionShortName = _requestContext.InstitutionShortName,
                StudentName = fullName,
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
                string subject = "Ticket Received: " + refNumber;
                string body = $"Hello {ticket.StudentName}, your ticket '{dto.Title}' has been received. Your Ref is {refNumber}.";

                await _emailService.SendEmailAsync(student.Email, subject, body);


                var ticketDto = _mapper.Map<TicketDto>(ticket);
                return new GeneralResponse
                {
                    StatusCode = 201,
                    Message = $"Ticket created successfully. Reference: {refNumber}",
                    Data = ticketDto 
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

            if (ticket.TicketStatus == "Closed")
            {
                return new GeneralResponse { StatusCode = 400, Message = "Ticket is already Closed" };
            }

            var student = await _context.Students.AsNoTracking().FirstOrDefaultAsync(a => a.MatricNumber == dto.MatricNumber && a.InstitutionShortName == _requestContext.InstitutionShortName);

            // Update fields
            ticket.ResponseBody = dto.ResponseBody;
            ticket.TicketStatus = "Closed"; 
            ticket.DateOfResponse = DateTime.Now;

            await _context.SaveChangesAsync();

            //4. TODO: Email Notification of Response
             string subject = "Update on Ticket: " + ticket.ReferenceNumber;
             string body = $"Hello {ticket.StudentName}, an admin has responded: {dto.ResponseBody}";
            await _emailService.SendEmailAsync(student.Email, subject, body);

            var Dto = _mapper.Map<RespondToTicketDto>(ticket);  
            
            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Response sent and ticket closed.",
                Data = Dto
            };
        }

        public async Task<GeneralResponse> GetTicketByIdAsync(long id)
        {
            var ticket = await _context.Ticketing.FindAsync(id);
            if (ticket == null) return new GeneralResponse { StatusCode = 404, Message = "Ticket not found." };

            var Dto = _mapper.Map<GetTicketDto>(ticket);
            return new GeneralResponse { StatusCode = 200, Data = Dto };
        }

        public async Task<GeneralResponse> GetAllTicketsAsync(string institutionShortName, TicketingFilter filter, PagingParameters paging)
        {
            var query = _context.Ticketing
                .Where(x => x.InstitutionShortName == _requestContext.InstitutionShortName)
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

            var totalRecords = await query.CountAsync();
            var items = await query
                .OrderByDescending(x => x.Created)
                .Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize)
                .ToListAsync();

            var itemsDto = _mapper.Map<List<GetTicketDto>>(items);
            

            return new GeneralResponse
            {
                StatusCode = 200,
                Data = itemsDto,
                Meta = new
                {
                        paging.PageNumber,
                        paging.PageSize,
                        TotalRecords = totalRecords,
                        TotalPages = totalRecords == 0
                            ? 0
                            : (int)Math.Ceiling(totalRecords / (double)paging.PageSize)
                }
            };
        }
    }
}
