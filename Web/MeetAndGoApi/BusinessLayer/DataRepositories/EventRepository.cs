using System.Collections.Generic;
using System.Threading.Tasks;
using MeetAndGo.Shared.BusinessLogic.Responses;
using MeetAndGo.Shared.Models;
using MeetAndGoApi.BusinessLayer.Contracts;

namespace MeetAndGoApi.BusinessLayer.DataRepositories
{
    public class EventRepository : IEventRepository
    {
        #region Private fields

        

        #endregion

        #region Constuctor

        public EventRepository()
        {
            
        }

        #endregion

        #region Public methods

        public async Task<IResponse> CreateAsync(EventModel eventModel)
        {
            // TODO: real data
            return new Response(ResponseCode.Ok);
        }

        public async Task<IResponseData<IEnumerable<EventModel>>> ReadAsync(IEnumerable<PointModel> directions)
        {
            // TODO: real data

            var mockedItems = new List<EventModel>
            {
                new EventModel(),
                new EventModel(),
                new EventModel(),
            };

            return new ResponseData<IEnumerable<EventModel>>(mockedItems, ResponseCode.Ok);
        }

        #endregion
    }
}
