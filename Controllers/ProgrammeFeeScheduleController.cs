using EduReg.Common;
using EduReg.Managers;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request;
using EduReg.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EduReg.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProgrammeFeeScheduleController : ControllerBase
    {
        private readonly ProgrammeFeeScheduleManager _manager;

        public ProgrammeFeeScheduleController(ProgrammeFeeScheduleManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        [Route("CreateProgrammeFeeSchedule")]
        public async Task<IActionResult> CreateProgrammeFeeScheduleAsync([FromBody] ProgrammeFeeScheduleDto model)
        {
            var result = await _manager.CreateProgrammeFeeScheduleAsync(model);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete]
        [Route("DeleteProgrammeFeeSchedule/{id}/{institutionShortName}")]
        public async Task<IActionResult> DeleteProgrammeFeeScheduleAsync(int id, string institutionShortName)
        {
            var result = await _manager.DeleteProgrammeFeeScheduleAsync(id, institutionShortName);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [Route("GenerateProgrammeFeeSchedules/{institutionShortName}")]
        public async Task<IActionResult> GenerateProgrammeFeeSchedulesAsync(
            string institutionShortName,
            [FromBody] AcademicContextDto model)
        {
            var result = await _manager.GenerateProgrammeFeeSchedulesAsync(institutionShortName, model);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [Route("GetAllProgrammeFeeSchedules/{institutionShortName}")]
        public async Task<IActionResult> GetAllProgrammeFeeSchedulesAsync(string institutionShortName, [FromQuery] PagingParameters paging, [FromQuery] ProgrammeFeeScheduleFilter filter)
        {
            var result = await _manager.GetAllProgrammeFeeSchedulesAsync(institutionShortName,paging, filter);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [Route("GetProgrammeFeeScheduleById/{id}/{institutionShortName}")]
        public async Task<IActionResult> GetProgrammeFeeScheduleByIdAsync(int id, string institutionShortName)
        {
            var result = await _manager.GetProgrammeFeeScheduleByIdAsync(id, institutionShortName);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [Route("GetProgrammeFeeSchedulesByFeeItem/{institutionShortName}/{feeItemId}")]
        public async Task<IActionResult> GetProgrammeFeeSchedulesByFeeItemAsync(string institutionShortName, int feeItemId)
        {
            var result = await _manager.GetProgrammeFeeSchedulesByFeeItemAsync(institutionShortName, feeItemId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [Route("GetProgrammeFeeSchedulesByProgramme/{institutionShortName}/{programmeCode}")]
        public async Task<IActionResult> GetProgrammeFeeSchedulesByProgrammeAsync(string institutionShortName, string programmeCode)
        {
            var result = await _manager.GetProgrammeFeeSchedulesByProgrammeAsync(institutionShortName, programmeCode);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        [Route("UpdateProgrammeFeeSchedule/{id}")]
        public async Task<IActionResult> UpdateProgrammeFeeScheduleAsync(int id, [FromBody] ProgrammeFeeScheduleDto model)
        {
            var result = await _manager.UpdateProgrammeFeeScheduleAsync(id, model);
            return StatusCode(result.StatusCode, result);
        }
    }
}
