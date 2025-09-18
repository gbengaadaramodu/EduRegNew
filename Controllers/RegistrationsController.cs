using EduReg.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduReg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationsController : ControllerBase
    {
        private readonly RegistrationsManager _manager;
        public RegistrationsController(RegistrationsManager manager)
        {
            _manager = manager;
        }


    }
}
