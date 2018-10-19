using System.Threading.Tasks;
using MeetAndGo.Shared.BusinessLogic.Responses;
using MeetAndGoApi.Infrastructure.Contracts.Repository;
using MeetAndGoApi.Infrastructure.Dto;
using MeetAndGoApi.Infrastructure.Resources;
using Microsoft.Extensions.Localization;

namespace MeetAndGoApi.Infrastructure.Dal.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IStringLocalizer<Strings> _localizer;
        private readonly Context.DbContext _dbContext;

        public UserRepository(
            Context.DbContext dbContext,
            IStringLocalizer<Strings> localizer)
        {
            _dbContext = dbContext;
            _localizer = localizer;
        }

        public async Task<IResponseData<ApplicationUser>> GetUserAsync(string userId)
        {
            // TODO: check it
            var result = await _dbContext.Users.FindAsync(userId);

            if (result is null)
            {
                new Response(ResponseCode.RequestError, _localizer.GetString(Strings.UserNotFound));
            }

            return new ResponseData<ApplicationUser>(result);
        }
    }
}
