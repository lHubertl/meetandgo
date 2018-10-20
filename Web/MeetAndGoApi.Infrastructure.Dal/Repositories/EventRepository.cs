using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MeetAndGo.Shared.BusinessLogic.Responses;
using MeetAndGo.Shared.Enums;
using MeetAndGo.Shared.Models;
using MeetAndGoApi.Infrastructure.Contracts.Repository;
using MeetAndGoApi.Infrastructure.Dto;
using MeetAndGoApi.Infrastructure.Extensions;
using MeetAndGoApi.Infrastructure.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace MeetAndGoApi.Infrastructure.Dal.Repositories
{
    public class EventRepository : IEventRepository
    {
        #region Private fields

        private readonly IStringLocalizer<Strings> _localizer;
        private readonly IUserRepository _userRepository;
        private readonly Context.DbContext _dbContext;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public EventRepository(
            Context.DbContext dbContext, 
            ILogger<EventRepository> logger, 
            IMapper mapper, 
            IStringLocalizer<Strings> localizer,
            IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _localizer = localizer;
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        #endregion

        #region Interface Implementation

        public async Task<IResponse> CreateAsync(EventModel model, string userId)
        {
            if (model == null)
            {
                return new Response(ResponseCode.ParameterIsNull, _localizer.GetString(Strings.ParameterCanNotBeNull));
            }

            var userResult = await _userRepository.GetUserAsync(userId);
            if (!userResult.Success)
            {
                return userResult;
            }

            var user = userResult.Data;
            var eventDto = _mapper.Map<EventDto>(model);

            eventDto.EventUsers = new List<EventUser> {new EventUser {ApplicationUser = user, EventDto = eventDto } };
            
            await _dbContext.Events.AddAsync(eventDto);
            await _dbContext.SaveChangesAsync();
            return new Response(ResponseCode.Ok);

        }

        public async Task<IResponseData<IEnumerable<EventModel>>> ReadAsync(IEnumerable<PointModel> directions)
        {
            // TODO: need to optimize, because we do need only 
            // nearest event to point, this logic just return to us all active events
            // this is performance hit
            
            var activeEventDtos = await _dbContext.Events
                .Where(eventDto => eventDto.EventState == EventStates.Formation)
                .Include(x => x.PointDtos)
                .Include(x => x.CommentDtos)
                .Include(x => x.EventUsers)
                .ThenInclude(x => x.ApplicationUser)
                .ToListAsync();

            var eventDtos = activeEventDtos
                .Where(eventDto => IsNear(eventDto.PointDtos, directions));

            var eventModels = _mapper.Map<List<EventModel>>(eventDtos);
            return new ResponseData<IEnumerable<EventModel>>(eventModels, ResponseCode.Ok);
        }

        #endregion

        #region Private methods

        // TODO: need to impelemt finding only near 1 km events
        private bool IsNear(IEnumerable<PointDto> eventPoints, IEnumerable<PointModel> requestPoints)
        {
            // Event and request should consist with more than one point 
            if (eventPoints?.Count() < 2 && requestPoints?.Count() < 2)
            {
                return false;
            }

            /* Currently we decided to search between start and end points
             * And events will be generated with start and end points
             * In future maybe we will implement searching end adding between more than two point*/

            var eventStartPoint = eventPoints.First();
            var eventEndPoint = eventPoints.Last();

            var requestStartPoint = requestPoints.First();
            var requestEndPoint = requestPoints.Last();

            var distanceStartPoint = GeolocationHelper.DistanceTo(
                eventStartPoint.Lat,
                eventStartPoint.Long,
                requestStartPoint.Lat,
                requestStartPoint.Long);

            if (distanceStartPoint > ApplicationSettings.SearchDistance)
            {
                return false;
            }

            var distanceEndPoint = GeolocationHelper.DistanceTo(
                eventEndPoint.Lat,
                eventEndPoint.Long,
                requestEndPoint.Lat,
                requestEndPoint.Long);

            if (distanceEndPoint > ApplicationSettings.SearchDistance)
            {
                return false;
            }
            
            return true;
        }

        #endregion
    }
}
