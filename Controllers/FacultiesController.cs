using EduReg.Common;
using EduReg.Managers;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EduReg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultiesController : ControllerBase
    {
        private readonly IFaculties _facultyService;

        public FacultiesController(IFaculties facultyService)
        {
            _facultyService = facultyService;
        }


        [HttpPost]
        [Route("createFaculty")]
        
        public async Task<IActionResult> CreateFaculty([FromBody] FacultiesDto model)
        {

                var resp = await _facultyService.CreateFacultyAsync(model);
            return resp.StatusCore switch
            {
                200 => Ok(resp),
                404 => NotFound(resp),
                400 => BadRequest(resp),
                _ => StatusCode(500, resp)
            };

        }

        [HttpPut("updateFaculty/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FacultiesDto model)
        {

            var resp = await _facultyService.UpdateFacultyAsync(id, model);
            return resp.StatusCore switch
            {
                200 => Ok(resp),
                404 => NotFound(resp),
                400 => BadRequest(resp),
                _ => StatusCode(500, resp)
            };
        }



        [HttpDelete("deleteFaculty/{id}")]

        public async Task<IActionResult> DeleteFacultyAsync(int id)
        {

                var resp = await _facultyService.DeleteFacultyAsync(id);
            return resp.StatusCore switch
            {
                200 => Ok(resp),
                404 => NotFound(resp),
                400 => BadRequest(resp),
                _ => StatusCode(500, resp)
            };
        }

        [HttpGet]
        [Route("getAllFaculty")]

        public async Task<IActionResult> GetAll()
        {
 
                var resp = await _facultyService.GetAllFacultiesAsync();
            return resp.StatusCore switch
            {
                200 => Ok(resp),
                404 => NotFound(resp),
                400 => BadRequest(resp),
                _ => StatusCode(500, resp)
            };
        }

        [HttpGet("getFacultyById/{id}")]

        public async Task<IActionResult> GetById(int id)
        {

                var resp = await _facultyService.GetFacultyByIdAsync(id);

            return resp.StatusCore switch
            {
                200 => Ok(resp),
                404 => NotFound(resp),
                400 => BadRequest(resp),
                _ => StatusCode(500, resp)
            };
        }
    }
}


