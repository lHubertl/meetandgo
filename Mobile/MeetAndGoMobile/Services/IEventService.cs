using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MeetAndGo.Shared.BusinessLogic.Responses;
using MeetAndGo.Shared.Models;

namespace MeetAndGoMobile.Services
{
    public interface IEventService
    {
        Task<IResponseData<List<EventModel>>> GetEventsHistoryAsync(CancellationToken token);
    }
}
