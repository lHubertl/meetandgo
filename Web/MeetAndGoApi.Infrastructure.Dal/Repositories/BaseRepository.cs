using AutoMapper;
using MeetAndGoApi.Infrastructure.Dal.Context;

namespace MeetAndGoApi.Infrastructure.Dal.Repositories
{
    public class BaseRepository
    {
        protected readonly DbContext DbContext;
        protected readonly IMapper Mapper;

        public BaseRepository(Context.DbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            Mapper = mapper;
        }
    }
}
