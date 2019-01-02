using System.Collections.Generic;
using System.Threading.Tasks;
using MeetAndGo.Shared.BusinessLogic.Responses;
using MeetAndGo.Shared.Models;
using MeetAndGoApi.Extensions;
using MeetAndGoApi.Infrastructure.Contracts.Service;
using MeetAndGoApi.Infrastructure.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace MeetAndGoApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class EventController : ControllerBase
    {
        #region Private fields

        private readonly IStringLocalizer<Strings> _localizer;
        private readonly IEventService _service;
        private readonly ILogger _logger;

        #endregion

        #region Constructor

        public EventController(
            IEventService service,
            IStringLocalizer<Strings> localizer,
            ILogger<AccountController> logger)
        {
            _service = service;
            _logger = logger;
            _localizer = localizer;
        }

        #endregion

        #region Api methods

        [HttpPost]
        public Task<IResponseData<IEnumerable<EventModel>>> GetNearestEvents(IEnumerable<PointModel> directions)
        {
            return _service.ReadAsync(directions);
        }

        [HttpPost]
        public Task<IResponse> Create([FromBody] EventModel eventModel)
        {
            return _service.CreateAsync(eventModel, User.CurrentUserId());
        }

        [HttpGet]
        public Task<IResponseData<IEnumerable<EventModel>>> GetEventsHistory()
        {
            return _service.ReadParticipatedEventsAsync(User.CurrentUserId());
        }
        
        #endregion
    }
}