using MeetAndGo.Shared.BusinessLogic.Responses;
using MeetAndGoApi.Infrastructure.Dto;
using System.Threading.Tasks;

namespace MeetAndGoApi.Infrastructure.Contracts.Repository
{
    public interface IUserRepository
    {
        Task<IResponseData<ApplicationUser>> GetUserAsync(string userId);
    }
}
