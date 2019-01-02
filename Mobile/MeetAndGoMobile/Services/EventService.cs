using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MeetAndGo.Shared.BusinessLogic.Responses;
using MeetAndGo.Shared.Models;
using MeetAndGoMobile.Constants;
using MeetAndGoMobile.Infrastructure.BusinessLogic;
using MeetAndGoMobile.Infrastructure.BusinessLogic.Repositories;

namespace MeetAndGoMobile.Services
{
    internal class EventService : HttpBaseService, IEventService
    {
        private readonly IDataRepository _dataRepository;

        public EventService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }
        public async Task<IResponseData<List<EventModel>>> GetEventsHistoryAsync(CancellationToken token)
        {
            var authToken = _dataRepository.Get<string>(DataType.Token);
            var headers = new Dictionary<string, object>
            {
                { WebApiConstants.Authorization, $"Bearer {authToken}" }
            };

            var result = await GetAsync(new Uri(WebApiConstants.EventHistory), token, headers);

            if (result.Success)
            {
                return GetResponseDataFromJson<List<EventModel>>(result.Data);
            }

            return new ResponseData<List<EventModel>>(result.Code, result.Message);
        }
    }
}
