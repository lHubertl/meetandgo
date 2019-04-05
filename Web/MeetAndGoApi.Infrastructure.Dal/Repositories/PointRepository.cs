using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MeetAndGo.Shared.BusinessLogic.Responses;
using MeetAndGo.Shared.Models;
using MeetAndGoApi.Infrastructure.Contracts.Repository;
using MeetAndGoApi.Infrastructure.Dal.Context;

namespace MeetAndGoApi.Infrastructure.Dal.Repositories
{
    public class PointRepository : BaseRepository, IPointRepository
    {
        public PointRepository(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {

        }

        public Task<IResponseData<List<PointModel>>> GetUserLatestPointsAsync(string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}
