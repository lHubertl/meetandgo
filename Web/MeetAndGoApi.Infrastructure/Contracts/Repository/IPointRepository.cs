using System.Collections.Generic;
using System.Threading.Tasks;
using MeetAndGo.Shared.BusinessLogic.Responses;
using MeetAndGo.Shared.Models;

namespace MeetAndGoApi.Infrastructure.Contracts.Repository
{
    public interface IPointRepository
    {
        Task<IResponseData<List<PointModel>>> GetUserLatestPointsAsync(string userId);
    }
}
