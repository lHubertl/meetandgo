using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetAndGoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WellcomeController : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public string Get()
        {
            return "Hello mate";
        }
    }
}