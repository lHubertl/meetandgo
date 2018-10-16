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
            var c = new CommentModel();
            var res = _mapper.Map<CommentDto>(c);

            var dto = _mapper.Map<EventDto>(model);
            
            // TODO: SHOULD BE REAL USER
            var user = await _dbContext.Users.FirstOrDefaultAsync();
            if (user != null)
            {
                dto.EventUsers = new List<EventUser>
                    {new EventUser {UserDto = user, EventDtoId = user.UserDtoId}};
            }

            await _dbContext.Events.AddAsync(dto);
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
    }
}
