﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MeetAndGo.Shared.BusinessLogic.Responses;
using MeetAndGo.Shared.Models;
using MeetAndGoApi.Infrastructure.Contracts.Repository;
using MeetAndGoApi.Infrastructure.Dal.Context;
using MeetAndGoApi.Infrastructure.Dal.Dto;
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
        private readonly Context.DbContext _dbContext;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor
        
        public EventRepository(Context.DbContext dbContext, ILogger<EventRepository> logger, IMapper mapper, IStringLocalizer<Strings> localizer)
        {
            _localizer = localizer;
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
                return new Response(ResponseCode.ParameterIsNull, _localizer.GetString(Strings.V_ParameterCanNotBeNull));
            }

            var resultApplicationUser = await GetCurrentUserAsync();
            if (!resultApplicationUser.IsSuccess)
            {
                return resultApplicationUser;
            }

            var ApplicationUser = resultApplicationUser.Data;

            var eventDto = _mapper.Map<EventDto>(model);

            eventDto.EventUsers = new List<EventUser> {new EventUser {ApplicationUser = ApplicationUser, EventDto = eventDto } };
            
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
                ThenInclude(x => x.ApplicationUser).
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

        private async Task<IResponseData<ApplicationUser>> GetCurrentUserAsync()
        {
            // TODO: rework it to get current user

            // TODO: SHOULD BE REAL USER
            var user = await _dbContext.Users.FirstOrDefaultAsync();

            if (user != null)
            {
                return new ResponseData<ApplicationUser>(user, ResponseCode.Ok);
            }

            return new ResponseData<ApplicationUser>(ResponseCode.ServerError, _localizer.GetString(Strings.V_CanNotFindData));
        }
        
        #endregion
    }
}
