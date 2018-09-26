using System.Collections.Generic;
using MeetAndGo.Shared.BusinessLogic.Responses;
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
        public IResponseData<IEnumerable<EventModel>> Get()
        {
            return new ResponseData<IEnumerable<EventModel>>(new[] {new EventModel {Transport = Transports.Taxi}}, ResponseCode.Ok);
        }
    }
}