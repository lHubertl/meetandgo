﻿using System.Collections.Generic;
using System.Threading.Tasks;
using MeetAndGo.Shared.BusinessLogic.Responses;
using MeetAndGo.Shared.Models;
using MeetAndGoApi.BusinessLayer.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace MeetAndGoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        #region Private fields

        private readonly IEventRepository _eventRepository;

        #endregion

        #region Constructor

        public EventController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        #endregion

        #region Api methods

        [HttpGet]
        public async Task<IResponseData<IEnumerable<EventModel>>> GetAsync()
        {
            return await _eventRepository.ReadAsync(null);
        }

        [HttpPost]
        public async Task<IResponse> CreateAsync([FromBody] EventModel eventModel)
        {
            return await _eventRepository.CreateAsync(eventModel);
        }
        
        #endregion
    }
}