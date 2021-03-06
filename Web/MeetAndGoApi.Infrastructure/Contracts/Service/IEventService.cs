﻿using MeetAndGo.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using MeetAndGo.Shared.BusinessLogic.Responses;

namespace MeetAndGoApi.Infrastructure.Contracts.Service
{
    public interface IEventService
    {
        Task<IResponse> CreateAsync(EventModel eventModel, string userId);
        Task<IResponseData<IEnumerable<EventModel>>> ReadAsync(IEnumerable<PointModel> directions);
        Task<IResponseData<IEnumerable<EventModel>>> ReadParticipatedEventsAsync(string userId);
    }
}
