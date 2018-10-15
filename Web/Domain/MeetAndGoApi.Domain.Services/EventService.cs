using System.Collections.Generic;
using System.Threading.Tasks;
using MeetAndGo.Shared.BusinessLogic.Responses;
using MeetAndGo.Shared.Models;
using MeetAndGoApi.Infrastructure.Contracts.Repository;
using MeetAndGoApi.Infrastructure.Contracts.Service;

namespace MeetAndGoApi.Domain.Services
{
    public class EventService : IEventService
    {
        #region Private fields

        private readonly IEventRepository _repository;

        #endregion

        #region Constructor

        public EventService(IEventRepository repository)
        {
            _repository = repository;
        }

        #endregion

        public Task<IResponse> CreateAsync(EventModel eventModel)
        {
            return _repository.CreateAsync(eventModel);
        }

        public Task<IResponseData<IEnumerable<EventModel>>> ReadAsync(IEnumerable<PointModel> directions)
        {
            return _repository.ReadAsync(directions);
        }
    }
}
