using EduReg.Managers;
using EduReg.Common;
using EduReg.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EduReg.Services.Interfaces;

namespace EduReg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SemestersController : ControllerBase
    {
        private readonly ISemesters _semester;

        public SemestersController(ISemesters semester)
        {
            _semester = semester;
        }

        [HttpGet]
        [Route("GetAllSemesters")]
        public async Task<IActionResult> GetAllSemesters()
        {
            try
            {
                var res = await _semester.GetAllSemestersAsync();
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
                    Message = "An Error occurred, please try again after some time.",
                    StatusCore = 400
                });
            }
        }

        [HttpGet]
        [Route("GetSemesterById/{id}")]
        public async Task<IActionResult> GetSemesterById([FromRoute] int id)
        {
            try
            {
                var res = await _semester.GetSemesterByIdAsync(id);
                if (res.Data == null)
                {
                    return NotFound(new GeneralResponse()
                    {
                        Data = null,
                        Message = res.Message,
                        StatusCore = 404
                    });
                }
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
                    Message = "An Error occurred, please try again after some time.",
                    StatusCore = 400
                });
            }
        }

        [HttpPost]
        [Route("CreateSemester")]
        public async Task<IActionResult> CreateSemester([FromBody] SemestersDto model)
        {
            try
            {
                var res = await _semester.CreateSemesterAsync(model);
                if (res.StatusCore == 404)
                {
                    return NotFound(new GeneralResponse()
                    {
                        Data = null,
                        Message = res.Message,
                        StatusCore = 404
                    });
                }
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
                    Message = "An Error occurred, please try again after some time.",
                    StatusCore = 400
                });
            }
        }

        [HttpPut]
        [Route("UpdateSemester/{id}")]
        public async Task<IActionResult> UpdateSemester([FromRoute]int Id, [FromBody] SemestersDto model)
        {
            try
            {
                var res = await _semester.UpdateSemesterAsync(Id, model);
                if (res.StatusCore == 404)
                {
                    return NotFound(new GeneralResponse()
                    {
                        Data = null,
                        Message = res.Message,
                        StatusCore = 404
                    });
                }
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
                    Message = "An Error occurred, please try again after some time.",
                    StatusCore = 400
                });
            }
        }

        [HttpDelete]
        [Route("DeleteSemester/{id}")]
        public async Task<IActionResult> DeleteSemester([FromRoute] int id)
        {
            try
            {
                var res = await _semester.DeleteSemesterAsync(id);
                if (res.StatusCore == 404)
                {
                    return NotFound(new GeneralResponse()
                    {
                        Data = null,
                        Message = res.Message,
                        StatusCore = 404
                    });
                }
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
                    Message = "An Error occurred, please try again after some time.",
                    StatusCore = 400
                });
            }
        }
    }
}
