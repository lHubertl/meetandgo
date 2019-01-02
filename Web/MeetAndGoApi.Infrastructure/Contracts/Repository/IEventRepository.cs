using System.Collections.Generic;
using System.Threading.Tasks;
using MeetAndGo.Shared.BusinessLogic.Responses;
using MeetAndGo.Shared.Models;

namespace MeetAndGoApi.Infrastructure.Contracts.Repository
{
    public interface IEventRepository
    {
        Task<IResponse> CreateAsync(EventModel eventModel, string userId);
        Task<IResponseData<IEnumerable<EventModel>>> ReadAsync(IEnumerable<PointModel> directions);
        Task<IResponseData<IEnumerable<EventModel>>> ReadParticipatedEventsAsync(string userId);
    }
}
