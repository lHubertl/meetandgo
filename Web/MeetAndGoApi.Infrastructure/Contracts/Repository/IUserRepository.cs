using MeetAndGo.Shared.BusinessLogic.Responses;
using MeetAndGo.Shared.Models;
using MeetAndGoApi.Infrastructure.Dto;
using System.Threading.Tasks;

namespace MeetAndGoApi.Infrastructure.Contracts.Repository
{
    public interface IUserRepository
    {
        Task<IResponseData<ApplicationUser>> GetAppUserAsync(string id);
        Task<IResponseData<UserModel>> GetUserModelAsync(string id);
    }
}
