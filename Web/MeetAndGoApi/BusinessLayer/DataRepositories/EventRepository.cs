using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MeetAndGo.Shared.Models;
using MeetAndGoApi.BusinessLayer.Contracts;

namespace MeetAndGoApi.BusinessLayer.DataRepositories
{
    public class EventRepository : IEventRepository
    {
        public Task<bool> CreateAsync(EventModel eventModel)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EventModel>> ReadAsync(IEnumerable<PointModel> directions)
        {
            throw new NotImplementedException();
        }
    }
}
