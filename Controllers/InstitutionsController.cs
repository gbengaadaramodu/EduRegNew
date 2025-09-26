using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EduReg.Models.Dto;
using EduReg.Managers;

namespace EduReg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstitutionsController : ControllerBase
    {
        private readonly InstitutionsManager _manager;

        public InstitutionsController(InstitutionsManager manager)
        {
            _manager = manager;
        }

        // POST: api/Institutions/CreateInstitution
        [HttpPost("CreateInstitution")]
        public async Task<IActionResult> CreateInstitution([FromBody] InstitutionsDto model)
        {
            var response = await _manager.CreateInstitutionAsync(model);
            return StatusCode(response.StatusCore, response);
        }

        // PUT: api/Institutions/UpdateInstitution/5
        [HttpPut("UpdateInstitution/{id}")]
        public async Task<IActionResult> UpdateInstitution(int id, [FromBody] InstitutionsDto model)
        {
            var response = await _manager.UpdateInstitutionAsync(id, model);
            return StatusCode(response.StatusCore, response);
        }

        // DELETE: api/Institutions/DeleteInstitution/5
        [HttpDelete("DeleteInstitution/{id}")]
        public async Task<IActionResult> DeleteInstitution(int id)
        {
            var response = await _manager.DeleteInstitutionAsync(id);
            return StatusCode(response.StatusCore, response);
        }

        // GET: api/Institutions/GetInstitutionById/5
        [HttpGet("GetInstitutionById/{id}")]
        public async Task<IActionResult> GetInstitutionById(int id)
        {
            var response = await _manager.GetInstitutionByIdAsync(id);
            return StatusCode(response.StatusCore, response);
        }

        // GET: api/Institutions/GetInstitutionByShortName/ABC123
        [HttpGet("GetInstitutionByShortName/{shortName}")]
        public async Task<IActionResult> GetInstitutionByShortName(string shortName)
        {
            var response = await _manager.GetInstitutionByShortNameAsync(shortName);
            return StatusCode(response.StatusCore, response);
        }

        // GET: api/Institutions/GetAllInstitutions
        [HttpGet("GetAllInstitutions")]
        public async Task<IActionResult> GetAllInstitutions()
        {
            var response = await _manager.GetAllInstitutionAsync();
            return StatusCode(response.StatusCore, response);
        }
    }
}
