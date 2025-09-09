using EduReg.Managers;
using EduReg.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduReg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcademicLevelController : ControllerBase
    {
        private readonly AcademicsManager _manager;   

        public AcademicLevelController(AcademicsManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        [Route("CreateAcademicLevel")]
        public async Task<IActionResult> CreateAcademicLevel(AcademicLevelsDto model)
        {
            var response = await _manager.CreateAcademicLevelAsync(model);
            return StatusCode(response.StatusCore, response);
        }
       
        [HttpPut("UpdateAcademicLevel/{Id}")]
        public async Task<IActionResult> UpdateAcademicLevel(int Id, AcademicLevelsDto model)
        {
            var response = await _manager.UpdateAcademicLevelAsync(Id, model);
            return StatusCode(response.StatusCore, response);
        }

       
       [HttpDelete("DeleteAcademicLevel/{Id}")]
       public async Task<IActionResult> DeleteAcademicLevel(int Id)
       {
           var response = await _manager.DeleteAcademicLevelAsync(Id);
           return StatusCode(response.StatusCore, response);
       }



       [HttpGet]
       [Route("GetAllAcademicLevels")]
       public async Task<IActionResult> GetAllAcademicLevels()
       {
           var response = await _manager.GetAllAcademicLevelsAsync();
           return StatusCode(response.StatusCore, response);
       }

       [HttpGet("GetAcademicLevelById/{Id}")]
       public async Task<IActionResult> GetAcademicLevelById(int Id)
       {
           var response = await _manager.GetAcademicLevelByIdAsync(Id);
           return StatusCode(response.StatusCore, response);
       }
       
    }
}
