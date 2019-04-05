using System.Collections.Generic;
using System.Threading.Tasks;
using MeetAndGo.Shared.BusinessLogic.Responses;
using MeetAndGo.Shared.Models;
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
        private readonly IPointRepository _pointRepository;

        public UserService(
            IUserRepository userRepository, 
            IStringLocalizer<Strings> localizer,
            IPointRepository pointRepository) 
            : base(localizer)
        {
            _userRepository = userRepository;
            _pointRepository = pointRepository;
        }

        public Task<IResponseData<ApplicationUser>> GetAppUserAsync(string userId)
        {
            return _userRepository.GetAppUserAsync(userId);
        }

        public Task<IResponseData<UserModel>> GetUserModelAsync(string userId)
        {
            return _userRepository.GetUserModelAsync(userId);
        }

        public Task<IResponse> SetUserPhotoAsync(string userId, string name, string path)
        {
            return _userRepository.SetUserPhotoAsync(userId, name, path);
        }

        public Task<IResponseData<List<PointModel>>> GetUserLatestPointsAsync(string userId)
        {
            return _pointRepository.GetUserLatestPointsAsync(userId);
        }
    }
}
