using EduReg.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduReg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseScheduleController : ControllerBase
    {
        private readonly CourseScheduleManager _manager;
        public CourseScheduleController(CourseScheduleManager manager)
        {
            _manager = manager;
        }




    }
}
