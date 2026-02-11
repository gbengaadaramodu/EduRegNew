using EduReg.Common;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request;
using EduReg.Services.Interfaces;

namespace EduReg.Managers
{
    public class TicketingManager : ITicketing
    {
        private readonly ITicketing _ticketing;

        public TicketingManager(ITicketing ticketing)
        {
            _ticketing = ticketing;
        }

        public async Task<GeneralResponse> CreateTicketAsync(string institutionShortName, TicketDto dto)
        {
          return await  _ticketing.CreateTicketAsync(institutionShortName, dto);
        }

        public async Task<GeneralResponse> RespondToTicketAsync(long id, RespondToTicketDto dto) 
        {
            return await _ticketing.RespondToTicketAsync(id, dto);
        }

        public async Task<GeneralResponse> GetTicketByIdAsync(long id)
        {
            return await _ticketing.GetTicketByIdAsync(id);
        }

        public async Task<GeneralResponse> GetAllTicketsAsync(string institutionShortName, TicketingFilter filter, PagingParameters paging)
        {
            return await _ticketing.GetAllTicketsAsync(institutionShortName, filter, paging);
        }
    }
}
