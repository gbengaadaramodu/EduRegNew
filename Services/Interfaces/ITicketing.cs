using EduReg.Common;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request;

namespace EduReg.Services.Interfaces
{
    public interface ITicketing
    {
       
        Task<GeneralResponse> CreateTicketAsync(string institutionShortName, TicketDto dto);

        // UPDATE: Admin responds to a specific ticket
        // Uses the "Clean Body" pattern: ID in URL, Response in Body
        Task<GeneralResponse> RespondToTicketAsync(long id, RespondToTicketDto dto);

        Task<GeneralResponse> GetTicketByIdAsync(long id);

        Task<GeneralResponse> GetAllTicketsAsync(string institutionShortName, TicketingFilter filter, PagingParameters paging);
    }
}
