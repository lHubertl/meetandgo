using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeetAndGo.Shared.BusinessLogic.Responses;
using MeetAndGo.Shared.Managers;
using MeetAndGo.Shared.Models;
using MeetAndGoApi.Infrastructure.Contracts.Repository;
using MeetAndGoApi.Infrastructure.Contracts.Service;
using MeetAndGoApi.Infrastructure.Resources;
using MeetAndGoApi.Infrastructure.Services;
using Microsoft.Extensions.Localization;

namespace MeetAndGoApi.Infrastructure.Domain.Services
{
    public class EventService : BaseService, IEventService
    {
        #region Private fields

        private readonly IEventRepository _repository;

        #endregion

        #region Constructor

        public EventService(IEventRepository repository, IStringLocalizer<Strings> localizer) 
            : base(localizer)
        {
            _repository = repository;
        }

        #endregion

        public async Task<IResponse> CreateAsync(EventModel eventModel, string userId)
        {
            var validator = ValidationManager.Create()
                .Validate(() => !string.IsNullOrWhiteSpace(userId), Localizer.GetString(Strings.UserNotFound))
                .Validate(() => eventModel != null, Localizer.GetString(Strings.ParameterCanNotBeNull))
                .Validate(() => eventModel?.Direction.Count >= 2, Localizer.GetString(Strings.DirectionPointless))
                .Validate(() => !string.IsNullOrWhiteSpace(eventModel?.Name),
                    Localizer.GetString(Strings.NameCanNotBeEmpty));

            if (!validator.IsValid)
            {
                return new Response(ResponseCode.ValidationError, validator.ToString());
            }

            return await _repository.CreateAsync(eventModel, userId);
        }

        public async Task<IResponseData<IEnumerable<EventModel>>> ReadAsync(IEnumerable<PointModel> directions)
        {
            var validator = ValidationManager.Create()
                .Validate(() => directions != null && directions.Count() > 1,
                    Localizer.GetString(Strings.DirectionPointless));

            if (!validator.IsValid)
            {
                return new ResponseData<IEnumerable<EventModel>>(ResponseCode.ValidationError, validator.ToString());
            }

            return await _repository.ReadAsync(directions);
        }
    }
}
