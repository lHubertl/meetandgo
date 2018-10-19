using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MeetAndGo.Shared.BusinessLogic.Responses;
using MeetAndGo.Shared.Models;
using MeetAndGoApi.Infrastructure.Contracts.Repository;
using MeetAndGoApi.Infrastructure.Dto;
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
            var eventDtos = await _dbContext.Events.
                Include(x => x.CommentDtos).
                Include(x => x.PointDtos).
                Include(x => x.EventUsers).
                ThenInclude(x => x.ApplicationUser).
                ToListAsync();

            var eventModels = _mapper.Map<List<EventModel>>(eventDtos);
            return new ResponseData<IEnumerable<EventModel>>(eventModels, ResponseCode.Ok);
        }

        #endregion
    }
}
