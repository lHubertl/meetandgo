using System.Collections.Generic;
using System.Threading.Tasks;
using MeetAndGo.Shared.BusinessLogic.Responses;
using MeetAndGo.Shared.Models;

namespace MeetAndGoApi.BusinessLayer.Contracts
{
    public interface IEventRepository
    {
        Task<IResponse> CreateAsync(EventModel eventModel);
        Task<IResponseData<IEnumerable<EventModel>>> ReadAsync(IEnumerable<PointModel> directions);
    }
}
