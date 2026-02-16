using EduReg.Common;
using EduReg.Common.Attributes;
using EduReg.Managers;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

   
        [HttpPost("CreateInstitution")]
        public async Task<IActionResult> CreateInstitution([FromBody] InstitutionsDto model)
        {
            var response = await _manager.CreateInstitutionAsync(model);
            return StatusCode(response.StatusCode, response);
        }

       
        [HttpPut("UpdateInstitution/{id}")]
        public async Task<IActionResult> UpdateInstitution(int id, [FromBody] UpdateInstitutionsDto model)
        {
            var response = await _manager.UpdateInstitutionAsync(id, model);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("UpdateInstitutionByShortName/{shortName}")]
        public async Task<IActionResult> UpdateInstitutionByShortName(string shortName, [FromBody] UpdateInstitutionsDto model)
        {
            var response = await _manager.UpdateInstitutionByShortNameAsync(shortName, model);
            return StatusCode(response.StatusCode, response);
        }

        
        [HttpDelete("DeleteInstitution/{id}")]
        public async Task<IActionResult> DeleteInstitution(int id)
        {
            var response = await _manager.DeleteInstitutionAsync(id);
            return StatusCode(response.StatusCode, response);
        }

       
        [HttpGet("GetInstitutionById/{id}")]
        public async Task<IActionResult> GetInstitutionById(int id)
        {
            var response = await _manager.GetInstitutionByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

      
        [HttpGet("GetInstitutionByShortName/{shortName}")]
        public async Task<IActionResult> GetInstitutionByShortName(string shortName)
        {
            var response = await _manager.GetInstitutionByShortNameAsync(shortName);
            return StatusCode(response.StatusCode, response);
        }

      
        [HttpGet("GetAllInstitutions")]
        public async Task<IActionResult> GetAllInstitutions([FromQuery] PagingParameters paging, [FromQuery] InstitutionFilter filter)
        {
            var response = await _manager.GetAllInstitutionAsync(paging, filter);
            return StatusCode(response.StatusCode, response);
        }
    }
}
