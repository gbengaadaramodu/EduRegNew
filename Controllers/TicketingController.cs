using EduReg.Common;
using EduReg.Common.Attributes;
using EduReg.Managers;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request;
using Microsoft.AspNetCore.Mvc;

namespace EduReg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [RequireInstitutionShortName]
    public class TicketingController : ControllerBase
    {
        private readonly TicketingManager _manager;

        public TicketingController(TicketingManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        [Route("CreateE-Ticket")]
        public async Task<IActionResult> CreateTicketAsync(string? institutionShortName,[FromBody] TicketDto dto)
        {
          var response = await _manager.CreateTicketAsync(institutionShortName, dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("RespondToTicketAsync")]
        public async Task<IActionResult> RespondToTicketAsync(long id, [FromBody] RespondToTicketDto dto)
        {
            var response = await _manager.RespondToTicketAsync(id, dto);
                return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("GetTicketByIdAsync")]
        public async Task<IActionResult> GetTicketByIdAsync(long id)
        {
            var response = await _manager.GetTicketByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("GetAllTicketsAsync")]
        public async Task<IActionResult> GetAllTicketsAsync(string? institutionShortName, [FromQuery]TicketingFilter filter, [FromQuery] PagingParameters paging)
        {
            var response = await _manager.GetAllTicketsAsync(institutionShortName, filter, paging);
            return StatusCode(response.StatusCode, response);
        }

    }
}
