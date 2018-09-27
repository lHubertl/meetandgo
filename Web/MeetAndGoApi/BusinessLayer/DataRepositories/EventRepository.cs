using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MeetAndGo.Shared.BusinessLogic.Responses;
using MeetAndGo.Shared.Models;
using MeetAndGoApi.BusinessLayer.Contracts;
using MeetAndGoApi.BusinessLayer.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MeetAndGoApi.BusinessLayer.DataRepositories
{
    public class EventRepository : IEventRepository
    {
        #region Private fields

        private readonly MeetAndGoContext _dbContext;
        private readonly ILogger _logger;

        #endregion

        #region Constuctor

        public EventRepository(MeetAndGoContext dbContext, ILogger<Program> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        #endregion

        #region Public methods

        public async Task<IResponse> CreateAsync(EventModel eventModel)
        {
            // Fill up new Guid
            eventModel.EventModelId = Guid.NewGuid();
            eventModel.Direction.ForEach(x => x.PointModelId = Guid.NewGuid());

            // TODO: SHOULD BE REAL USER
            var user = await _dbContext.Members.FirstOrDefaultAsync();
            if (user != null)
            {
                eventModel.Members = new List<MemberModel> {user};
            }

            await _dbContext.Events.AddAsync(eventModel);
            await _dbContext.SaveChangesAsync();
            return new Response(ResponseCode.Ok);
        }

        public async Task<IResponseData<IEnumerable<EventModel>>> ReadAsync(IEnumerable<PointModel> directions)
        {
            // TODO Get real collection by direction
            var events = await _dbContext.Events.ToListAsync();
            return new ResponseData<IEnumerable<EventModel>>(events, ResponseCode.Ok);
        }

        #endregion
    }
}
