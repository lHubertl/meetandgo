using System.Collections.Generic;
using System.Threading.Tasks;
using MeetAndGo.Shared.Models;

namespace MeetAndGoApi.BusinessLayer.Contracts
{
    public interface IEventRepository
    {
        Task<bool> CreateAsync(EventModel eventModel);
        Task<IEnumerable<EventModel>> ReadAsync(IEnumerable<PointModel> directions);
    }
}
