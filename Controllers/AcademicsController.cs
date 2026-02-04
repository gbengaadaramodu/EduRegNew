using Azure;
using EduReg.Common;
using EduReg.Managers;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request;
using EduReg.Services.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduReg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcademicsController : ControllerBase
    {
        private readonly AcademicsManager _manager;

        public AcademicsController(AcademicsManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        [Route("CreateAcademicSession")]
        public async Task<IActionResult> CreateAcademicSessionAsync([FromBody] CreateAcademicSessionDto model)
        {
            var response = await _manager.CreateAcademicSessionAsync(model);
                         
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        [Route("CreateAdmissionBatch")]
        public async Task<IActionResult> CreateAdmissionBatchAsync([FromBody] AdmissionBatchesDto model)
        {
            var response = await _manager.CreateAdmissionBatchAsync(model);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        [Route("CreateAcademicLevel")]
        public async Task<IActionResult> CreateAcademicLevelAsync(AcademicLevelsDto model)
        {
            var response = await _manager.CreateAcademicLevelAsync(model);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        [Route("CreateSemester")]
        public async Task<IActionResult> CreateSemesterAsync([FromBody] SemestersDto model)
        {
            var response = await _manager.CreateSemesterAsync(model);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("CreateSessionSemester")]
        public async Task<IActionResult> CreateSessionSemesterAsync([FromBody]SessionSemesterDto dto)
        {
            var response = await _manager.CreateSessionSemesterAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
       [HttpDelete]
        [Route("DeleteAcademicSession/{Id}")]
        public async Task<IActionResult> DeleteAcademicSessionAsync(int Id)
        {
            var response = await _manager.DeleteAcademicSessionAsync(Id);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete]
        [Route("DeleteAdmissionBatch/{Id}")]
        public async Task<IActionResult> DeleteAdmissionBatchAsync(int Id)
        {
            var response = await _manager.DeleteAdmissionBatchAsync(Id);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete]
        [Route("DeleteAcademicLevel/{Id}")]
        public async Task<IActionResult> DeleteAcademicLevelAsync(int Id)
        {
            var response = await _manager.DeleteAcademicLevelAsync(Id);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete]
        [Route("DeleteSemester/{Id}")]
        public async Task<IActionResult> DeleteSemesterAsync(int Id)
        {
            var response = await _manager.DeleteSemesterAsync(Id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete]
        [Route("DeleteSessionSemester/{id}")]
        public async Task<IActionResult> DeleteSessionSemesterAsync(int id)
        {
            var response = await _manager.DeleteSessionSemesterAsync(id);
                return StatusCode(response.StatusCode, response);
        }


        [HttpGet]
        [Route("GetAcademicSessionById/{Id}")]
        public async Task<IActionResult> GetAcademicSessionByIdAsync(int Id)
        {
            var response = await _manager.GetAcademicSessionByIdAsync(Id);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet]
        [Route("GetAdmissionBatchById/{Id}")]
        public async Task<IActionResult> GetAdmissionBatchByIdAsync(int Id)
        {
            var response = await _manager.GetAcademicLevelByIdAsync(Id);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet]
        [Route("GetAcademicLevelById/{Id}")]
        public async Task<IActionResult> GetAcademicLevelByIdAsync(int Id)
        {
            var response = await _manager.GetAcademicLevelByIdAsync(Id);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet]
        [Route("GetAllAcademicSessions")]
        public async Task<IActionResult> GetAllAcademicSessionsAsync([FromQuery] PagingParameters paging, [FromQuery] AcademicSessionFilter filter)
        {
            var response = await _manager.GetAllAcademicSessionsAsync(paging, filter);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("GetAllAdmissionBatch")]
        public async Task<IActionResult> GetAllAdmissionBatchAsync([FromQuery] PagingParameters paging, [FromQuery] AcademicSessionFilter filter)
        {
            var response = await _manager.GetAllAdmissionBatchAsync(paging);
            return StatusCode(response.StatusCode, response);

        }

        [HttpGet]
        [Route("GetAllAcademicLevel")]
        public async Task<IActionResult> GetAllAcademicLevelsAsync([FromQuery] PagingParameters paging, [FromQuery] AcademicLevelFilter filter)
        {

            var response = await _manager.GetAllAcademicLevelsAsync(paging, filter);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet]
        [Route("GetAllSemesters")]
        public async Task<IActionResult> GetAllSemestersAsync([FromQuery] PagingParameters paging, [FromQuery] SemesterFilter filter)
        {
            var response = await _manager.GetAllSemestersAsync(paging, filter);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet]
        [Route("GetAllSessionSemesters")]
        public async Task<IActionResult> GetAllSessionSemesterAsync(string institutionShortName)
        {
            var response = await _manager.GetAllSessionSemesterAsync(institutionShortName);
            return StatusCode(response.StatusCode, response);
        }
        

       [HttpGet]
        [Route("GetSemesterById/{Id}")]
        public async Task<IActionResult> GetSemesterByIdAsync(int Id)
        {
            var response = await _manager.GetSemesterByIdAsync(Id);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet]
        [Route("GetSessionSemesterById/{Id}")]
        public async Task<IActionResult> GetSessionSemesterByIdAsync(int Id)
        {
            var response = await _manager.GetSessionSemesterByIdAsync(Id);
            return StatusCode(response.StatusCode, response);
        }


       [HttpPut]
        [Route("UpdateAcademicSession/{Id}")]
        public async Task<IActionResult> UpdateAcademicSessionAsync(int Id, [FromBody] UpdateAcademicSessionDto model)
        {
            var response = await _manager.UpdateAcademicSessionAsync(Id, model);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        [Route("UpdateAdmissionBatch/{Id}")]
        public async Task<IActionResult> UpdateAdmissionBatchAsync(int Id, [FromBody] UpdateAdmissionBatchesDto model)
        {
            var response = await _manager.UpdateAdmissionBatchAsync(Id, model);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut]
        [Route("UpdateAdmissionBatchByShortName/{shortName}")]
        public async Task<IActionResult> UpdateAdmissionBatchByShortNameAsync(string shortName, [FromBody] UpdateAdmissionBatchesDto model)
        {
            var response = await _manager.UpdateAdmissionBatchByShortNameAsync(shortName, model);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        [Route("UpdateAcademicLevel/{Id}")]
        public async Task<IActionResult> UpdateAcademicLevelAsync(int Id, [FromBody] AcademicLevelsDto model)
        {
            var response = await _manager.UpdateAcademicLevelAsync(Id, model);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        [Route("UpdateSemester/{Id}")]
        public async Task<IActionResult> UpdateSemesterAsync(int Id, [FromBody] SemestersDto model)
        {
            var response = await _manager.UpdateSemesterAsync(Id, model);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        [Route("UpdateSessionSemesterAsync/{Id}")]
        public async Task<IActionResult> UpdateSessionSemesterAsync(int Id, [FromBody] UpdateSessionSemesterDto dto) 
        {
            var response = await _manager.UpdateSessionSemesterAsync(Id, dto);
            return StatusCode(response.StatusCode, response);
        }

    }
}
