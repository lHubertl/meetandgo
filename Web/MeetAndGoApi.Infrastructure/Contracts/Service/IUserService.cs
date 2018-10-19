using MeetAndGo.Shared.BusinessLogic.Responses;
using MeetAndGoApi.Infrastructure.Dto;
using System.Threading.Tasks;

namespace MeetAndGoApi.Infrastructure.Contracts.Service
{
    public interface IUserService
    {
        Task<IResponseData<ApplicationUser>> GetUserAsync(string userId);
    }
}
