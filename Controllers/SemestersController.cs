using EduReg.Managers;
using EduReg.Common;
using EduReg.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduReg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SemestersController : ControllerBase
    {
        private readonly AcademicsManager _manager;

        public SemestersController(AcademicsManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        [Route("GetAllSemesters")]
        public async Task<IActionResult> GetAllSemesters()
        {
            try
            {
                var res = await _manager.GetAllSemestersAsync();
                return Ok(new GeneralResponse()
                {
                    Data = res.Data,
                    Message = res.message,
                    StatusCore = 200
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new GeneralResponse
                {
                    Data = null,
                    Message = !string.IsNullOrEmpty(ex.Message) ? ex.Message : "An Error occurred, please try again after some time.",
                    StatusCode = 400
                });
            }
        }

        [HttpGet]
        [Route("GetSemesterById/{id}")]
        public async Task<IActionResult> GetSemesterById([FromRoute] int id)
        {
            try
            {
                var res = await _manager.GetSemesterByIdAsync(id);
                return Ok(new GeneralResponse()
                {
                    Data = res.Data,
                    Message = res.message,
                    StatusCore = 200
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new GeneralResponse
                {
                    Data = null,
                    Message = !string.IsNullOrEmpty(ex.Message) ? ex.Message : "An Error occurred, please try again after some time.",
                    StatusCode = 400
                });
            }
        }

        [HttpPost]
        [Route("CreateSemester")]
        public async Task<IActionResult> CreateSemester([FromBody] SemestersDto model)
        {
            try
            {
                var res = await _manager.CreateSemesterAsync(model);
                return Ok(new GeneralResponse()
                {
                    Data = res.Data,
                    Message = res.message,
                    StatusCore = 200
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new GeneralResponse
                {
                    Data = null,
                    Message = !string.IsNullOrEmpty(ex.Message) ? ex.Message : "An Error occurred, please try again after some time.",
                    StatusCode = 400
                });
            }
        }

        [HttpPut]
        [Route("UpdateSemester/{id}")]
        public async Task<IActionResult> UpdateSemester([FromRoute]int Id, [FromBody] SemestersDto model)
        {
            try
            {
                var res = await _manager.UpdateSemesterAsync(id, model);
                return Ok(new GeneralResponse()
                {
                    Data = res.Data,
                    Message = res.message,
                    StatusCore = 200
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new GeneralResponse
                {
                    Data = null,
                    Message = !string.IsNullOrEmpty(ex.Message) ? ex.Message : "An Error occurred, please try again after some time.",
                    StatusCode = 400
                });
            }
        }

        [HttpDelete]
        [Route("DeleteSemester/{id}")]
        public async Task<IActionResult> DeleteSemester([FromRoute] int id)
        {
            try
            {
                var res = await _manager.DeleteSemesterAsync(id);
                return Ok(new GeneralResponse()
                {
                    Data = res.Data,
                    Message = res.message,
                    StatusCore = 200
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new GeneralResponse
                {
                    Data = null,
                    Message = !string.IsNullOrEmpty(ex.Message) ? ex.Message : "An Error occurred, please try again after some time.",
                    StatusCode = 400
                });
            }
        }
    }
}
