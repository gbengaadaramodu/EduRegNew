using EduReg.Common;
using EduReg.Managers;
using EduReg.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduReg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcademicSessionsController : ControllerBase
    {
        private readonly IAcademicSessions _service;
        public AcademicSessionsController(IAcademicSessions service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("GetAllAcademicSessions")]
        public async Task<IActionResult> GetAllAcademicSessions()
        {
            try
            {
                var res = await _service.GetAllAcademicSessionsAsync();
                return Ok(new GeneralResponse()
                {
                    Data = res.Data,
                    Message = res.Message,
                    StatusCore = 200
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new GeneralResponse
                {
                    Data = null,
                    Message = !string.IsNullOrEmpty(ex.Message) ? ex.Message : "An Error occurred, please try again after some time.",
                    StatusCore = 400
                });
            }
        }

        [HttpGet]
        [Route("GetAcademicSessionById/{id}")]
        public async Task<IActionResult> GetAcademicSessionById([FromRoute] int id)
        {
            try
            {
                var res = await _service.GetAcademicSessionByIdAsync(id);
                return Ok(new GeneralResponse()
                {
                    Data = res.Data,
                    Message = res.Message,
                    StatusCore = 200
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new GeneralResponse
                {
                    Data = null,
                    Message = !string.IsNullOrEmpty(ex.Message) ? ex.Message : "An Error occurred, please try again after some time.",
                    StatusCore = 400
                });
            }
        }

        [HttpPost]
        [Route("CreateAcademicSession")]
        public async Task<IActionResult> CreateAcademicSession([FromBody] Models.Dto.AcademicSessionsDto model)
        {
            try
            {
                var res = await _service.CreateAcademicSessionAsync(model);
                return Ok(new GeneralResponse()
                {
                    Data = res.Data,
                    Message = res.Message,
                    StatusCore = 200
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new GeneralResponse
                {
                    Data = null,
                    Message = !string.IsNullOrEmpty(ex.Message) ? ex.Message : "An Error occurred, please try again after some time.",
                    StatusCore = 400
                });
            }
        }

        [HttpDelete]
        [Route("DeleteAcademicSession/{id}")]
        public async Task<IActionResult> DeleteAcademicSession([FromRoute] int id)
        {
            try
            {
                var res = await _service.DeleteAcademicSessionAsync(id);
                return Ok(new GeneralResponse()
                {
                    Data = res.Data,
                    Message = res.Message,
                    StatusCore = 200
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new GeneralResponse
                {
                    Data = null,
                    Message = !string.IsNullOrEmpty(ex.Message) ? ex.Message : "An Error occurred, please try again after some time.",
                    StatusCore = 400
                });
            }
        }

        [HttpPut]
        [Route("UpdateAcademicSession/{id}")]
        public async Task<IActionResult> UpdateAcademicSession([FromRoute] int id, [FromBody] Models.Dto.AcademicSessionsDto model)
        {
            try
            {
                var res = await _service.UpdateAcademicSessionAsync(id, model);
                return Ok(new GeneralResponse()
                {
                    Data = res.Data,
                    Message = res.Message,
                    StatusCore = 200
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new GeneralResponse
                {
                    Data = null,
                    Message = !string.IsNullOrEmpty(ex.Message) ? ex.Message : "An Error occurred, please try again after some time.",
                    StatusCore = 400
                });
            }
        }
    }
}
