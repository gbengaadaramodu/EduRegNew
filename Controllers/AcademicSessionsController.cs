using EduReg.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduReg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcademicSessionsController : ControllerBase
    {
        private readonly AcademicsManager _manager;
        public AcademicSessionsController(AcademicsManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        [Route("GetAllAcademicSessions")]
        public async Task<IActionResult> GetAllAcademicSessions()
        {
            try
            {
                var res = await _manager.GetAllAcademicSessionsAsync();
                return Ok(new Common.GeneralResponse()
                {
                    Data = res.Data,
                    Message = res.Message,
                    StatusCore = 200
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Common.GeneralResponse
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
                var res = await _manager.GetAcademicSessionByIdAsync(id);
                return Ok(new Common.GeneralResponse()
                {
                    Data = res.Data,
                    Message = res.Message,
                    StatusCore = 200
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Common.GeneralResponse
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
                var res = await _manager.CreateAcademicSessionAsync(model);
                return Ok(new Common.GeneralResponse()
                {
                    Data = res.Data,
                    Message = res.Message,
                    StatusCore = 200
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Common.GeneralResponse
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
                var res = await _manager.DeleteAcademicSessionAsync(id);
                return Ok(new Common.GeneralResponse()
                {
                    Data = res.Data,
                    Message = res.Message,
                    StatusCore = 200
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Common.GeneralResponse
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
                var res = await _manager.UpdateAcademicSessionAsync(id, model);
                return Ok(new Common.GeneralResponse()
                {
                    Data = res.Data,
                    Message = res.Message,
                    StatusCore = 200
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Common.GeneralResponse
                {
                    Data = null,
                    Message = !string.IsNullOrEmpty(ex.Message) ? ex.Message : "An Error occurred, please try again after some time.",
                    StatusCore = 400
                });
            }
        }
    }
}
