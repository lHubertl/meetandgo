using MeetAndGo.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using MeetAndGo.Shared.BusinessLogic.Responses;

namespace MeetAndGoApi.Infrastructure.Contracts.Service
{
    public interface IEventService
    {
        Task<IResponse> CreateAsync(EventModel eventModel);
        Task<IResponseData<IEnumerable<EventModel>>> ReadAsync(IEnumerable<PointModel> directions);
    }
}
