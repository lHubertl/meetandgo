using System.Threading.Tasks;
using MeetAndGo.Shared.BusinessLogic.Responses;
using MeetAndGoApi.Infrastructure.Contracts.Repository;
using MeetAndGoApi.Infrastructure.Contracts.Service;
using MeetAndGoApi.Infrastructure.Dto;
using MeetAndGoApi.Infrastructure.Resources;
using Microsoft.Extensions.Localization;

namespace MeetAndGoApi.Infrastructure.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository, IStringLocalizer<Strings> localizer) 
            : base(localizer)
        {
            _userRepository = userRepository;
        }

        public Task<IResponseData<ApplicationUser>> GetUserAsync(string userId)
        {
            return _userRepository.GetUserAsync(userId);
        }
    }
}
