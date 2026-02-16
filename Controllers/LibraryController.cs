using EduReg.Common;
using EduReg.Common.Attributes;
using EduReg.Managers;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request;
using Microsoft.AspNetCore.Mvc;

namespace EduReg.Controllers
{
    [Route("api/[controller]")]
    [RequireInstitutionShortName]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly LibraryManager _manager;  // ← CHANGED
        private readonly RequestContext _requestContext;

        public LibraryController(LibraryManager manager, RequestContext requestContext)  // ← CHANGED
        {
            _manager = manager;
            _requestContext = requestContext;
        }

  
        [HttpPost]
        [Route("UploadResource")]
      
        public async Task<IActionResult> UploadResource([FromForm] CreateELibraryDto model)
        {
           // var institutionShortName = _requestContext.InstitutionShortName;
            var response = await _manager.CreateELibraryAsync(model);
            return StatusCode(response.StatusCode, response);
        }

       
        [HttpGet]
        [Route("GetAllResources")]
      
        public async Task<IActionResult> GetAllResources(
            [FromQuery] PagingParameters paging,
            [FromQuery] ELibraryFilter filter)
        {
            var response = await _manager.GetAllELibraryAsync(paging, filter);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("GetResource/{id}")]
   
        public async Task<IActionResult> GetResource(long id)
        {
            var response = await _manager.GetELibraryByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

      
        [HttpPut]
        [Route("UpdateResource/{id}")]
      
        public async Task<IActionResult> UpdateResource(long id, [FromForm] UpdateELibraryDto model)
        {
            var response = await _manager.UpdateELibraryAsync(id, model);
            return StatusCode(response.StatusCode, response);
        }

     
        [HttpDelete]
        [Route("DeleteResource/{id}")]
   
        public async Task<IActionResult> DeleteResource(long id)
        {
            var response = await _manager.DeleteELibraryAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}