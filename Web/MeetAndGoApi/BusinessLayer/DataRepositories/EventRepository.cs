using System.Collections.Generic;
using System.Threading.Tasks;
using MeetAndGo.Shared.BusinessLogic.Responses;
using MeetAndGo.Shared.Models;
using MeetAndGoApi.BusinessLayer.Contracts;
using MeetAndGoApi.BusinessLayer.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace MeetAndGoApi.BusinessLayer.DataRepositories
{
    public class EventRepository : IEventRepository
    {
        #region Private fields

        private readonly MeetAndGoContext _dbContext;


        #endregion

        #region Constuctor

        public EventRepository(MeetAndGoContext dbContext)
        {
            _dbContext = dbContext;
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
            // TODO Get real collection
            var events = await _dbContext.Events.ToListAsync();

            return new ResponseData<IEnumerable<EventModel>>(events, ResponseCode.Ok);
        }

        #endregion
    }
}
