using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using EduReg.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EduReg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstitutionsController : ControllerBase
    {
        private readonly Managers.InstitutionsManager _manager;
        public InstitutionsController(Managers.InstitutionsManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        [Route("CreateInstitution")]
        // [HttpPost("CreateInstitution")]
        public async Task<IActionResult> CreateInstitution(InstitutionsDto model)
        {
            var response = await _manager.CreateInstitutionAsync(model);
            return StatusCode(response.StatusCore, response);
        }
    }
}
