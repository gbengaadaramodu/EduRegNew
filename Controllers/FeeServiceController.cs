using EduReg.Common;
using EduReg.Models.Dto;
using EduReg.Managers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EduReg.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeeServiceController : ControllerBase
    {
        private readonly FeeServiceManager _manager;

        public FeeServiceController(FeeServiceManager manager)
        {
            _manager = manager;
        }

        // ============================================
        // FEE ITEM ENDPOINTS
        // ============================================

        [HttpPost]
        [Route("CreateFeeItem")]
        public async Task<IActionResult> CreateFeeItemAsync([FromBody] FeeItemDto model)
        {
            var result = await _manager.CreateFeeItemAsync(model);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [Route("GetAllFeeItems")]
        public async Task<IActionResult> GetAllFeeItemsAsync()
        {
            var result = await _manager.GetAllFeeItemsAsync();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [Route("GetFeeItemById/{id}")]
        public async Task<IActionResult> GetFeeItemByIdAsync(int id)
        {
            var result = await _manager.GetFeeItemByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        [Route("UpdateFeeItem/{id}")]
        public async Task<IActionResult> UpdateFeeItemAsync(int id, [FromBody] FeeItemDto model)
        {
            var result = await _manager.UpdateFeeItemAsync(id, model);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete]
        [Route("DeleteFeeItem/{id}")]
        public async Task<IActionResult> DeleteFeeItemAsync(int id)
        {
            var result = await _manager.DeleteFeeItemAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [Route("AddFeeItemToSemesterSchedule")]
        public async Task<IActionResult> AddFeeItemToSemesterScheduleAsync([FromBody] List<FeeItemDto> model)
        {
            var result = await _manager.AddFeeItemToSemesterScheduleAsync(model);
            return StatusCode(result.StatusCode, result);
        }

        // ============================================
        // FEE RULE ENDPOINTS
        // ============================================

        [HttpPost]
        [Route("CreateFeeRule/{institutionShortName}")]
        public async Task<IActionResult> CreateFeeRuleAsync(string institutionShortName, [FromBody] FeeRuleDto model)
        {
            var result = await _manager.CreateFeeRuleAsync(model, institutionShortName);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [Route("GetAllFeeRules/{institutionShortName}")]
        public async Task<IActionResult> GetAllFeeRulesAsync(string institutionShortName)
        {
            var result = await _manager.GetAllFeeRuleAsync(institutionShortName);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [Route("GetFeeRuleById/{id}/{institutionShortName}")]
        public async Task<IActionResult> GetFeeRuleByIdAsync(int id, string institutionShortName)
        {
            var result = await _manager.GetFeeRuleByIdAsync(id, institutionShortName);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        [Route("UpdateFeeRule/{id}/{institutionShortName}")]
        public async Task<IActionResult> UpdateFeeRuleAsync(int id, string institutionShortName, [FromBody] FeeRuleDto model)
        {
            var result = await _manager.UpdateFeeRuleAsync(id, model, institutionShortName);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete]
        [Route("DeleteFeeRule/{id}/{institutionShortName}")]
        public async Task<IActionResult> DeleteFeeRuleAsync(int id, string institutionShortName)
        {
            var result = await _manager.DeleteFeeRuleAsync(id, institutionShortName);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [Route("ApplyFeeRuleToSemesterSchedule/{institutionShortName}")]
        public async Task<IActionResult> ApplyFeeRuleToSemesterScheduleAsync(string institutionShortName, [FromBody] List<FeeRuleDto> models)
        {
            var result = await _manager.ApplyFeeRuleToSemesterScheduleAsync(models, institutionShortName);
            return StatusCode(result.StatusCode, result);
        }
    }
}
