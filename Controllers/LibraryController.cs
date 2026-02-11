using EduReg.Common;
using EduReg.Managers;
using EduReg.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EduReg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly LibraryManager _manager;

        public LibraryController(LibraryManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        [Route("CreateELibrary")]
        public async Task<IActionResult> CreateELibraryAsync([FromBody] ELibraryDto model)
        {
            var response = await _manager.CreateELibraryAsync(model);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("GetELibraryById/{Id}")]
        public async Task<IActionResult> GetELibraryByIdAsync(long Id)
        {
            var response = await _manager.GetELibraryByIdAsync(Id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("GetAllELibrary")]
        public async Task<IActionResult> GetAllELibraryAsync([FromQuery] PagingParameters paging, [FromQuery] string? institutionShortName, [FromQuery] string? courseCode)
        {
            var response = await _manager.GetAllELibraryAsync(paging, institutionShortName, courseCode);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        [Route("UpdateELibrary/{Id}")]
        public async Task<IActionResult> UpdateELibraryAsync(long Id, [FromBody] ELibraryDto model)
        {
            var response = await _manager.UpdateELibraryAsync(Id, model);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete]
        [Route("DeleteELibrary/{Id}")]
        public async Task<IActionResult> DeleteELibraryAsync(long Id)
        {
            var response = await _manager.DeleteELibraryAsync(Id);
            return StatusCode(response.StatusCode, response);
        }
    }
}