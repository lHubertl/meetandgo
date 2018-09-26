using System.Collections.Generic;
using MeetAndGo.Shared.Enums;
using MeetAndGo.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace MeetAndGoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<EventModel> Get()
        {
            return new[] {new EventModel {Transport = Transports.Taxi}};
        }
    }
}