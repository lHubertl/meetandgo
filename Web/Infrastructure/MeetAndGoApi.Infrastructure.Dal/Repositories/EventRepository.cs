using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MeetAndGo.Shared.BusinessLogic.Responses;
using MeetAndGo.Shared.Models;
using MeetAndGoApi.Infrastructure.Contracts.Repository;
using MeetAndGoApi.Infrastructure.Dal.Context;
using MeetAndGoApi.Infrastructure.Dal.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MeetAndGoApi.Infrastructure.Dal.Repositories
{
    public class EventRepository : IEventRepository
    {
        #region Private fields
        
        private readonly DatabaseContext _dbContext;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor
        
        public EventRepository(DatabaseContext dbContext, ILogger<EventRepository> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        #endregion

        #region Interface Implementation

        public async Task<IResponse> CreateAsync(EventModel model)
        {
            if (model == null)
            {
                // TODO move message to language resource
                return new Response(ResponseCode.ParameterIsNull, "Parameter can not be null");
            }

            var resultUserDto = await GetCurrentUserAsync();
            if (!resultUserDto.IsSuccess)
            {
                return resultUserDto;
            }

            var userDto = resultUserDto.Data;

            var eventDto = _mapper.Map<EventDto>(model);

            eventDto.EventUsers = new List<EventUser> {new EventUser {UserDto = userDto, EventDto = eventDto } };
            
            await _dbContext.Events.AddAsync(eventDto);
            await _dbContext.SaveChangesAsync();
            return new Response(ResponseCode.Ok);

        }

        public async Task<IResponseData<IEnumerable<EventModel>>> ReadAsync()
        {
            var eventDtos = await _dbContext.Events.
                Include(x => x.CommentDtos).
                Include(x => x.PointDtos).
                Include(x => x.EventUsers).
                ThenInclude(x => x.UserDto).
                ToListAsync();

            var eventModels = _mapper.Map<List<EventModel>>(eventDtos);
            return new ResponseData<IEnumerable<EventModel>>(eventModels, ResponseCode.Ok);
        }

        public Task<IResponseData<IEnumerable<EventModel>>> ReadAsync(IEnumerable<PointModel> directions)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private methods

        private async Task<IResponseData<UserDto>> GetCurrentUserAsync()
        {
            // TODO: rework it to get current user

            // TODO: SHOULD BE REAL USER
            var user = await _dbContext.Users.FirstOrDefaultAsync();

            if (user != null)
            {
                return new ResponseData<UserDto>(user, ResponseCode.Ok);
            }
            //TODO GET ERROR MESSAGE FROM RESOURCE FILE
            return new ResponseData<UserDto>(ResponseCode.ServerError, "Can not find the data");
        }
        
        #endregion
    }
}
