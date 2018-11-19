using System.Threading;
using System.Threading.Tasks;
using MeetAndGo.Shared.BusinessLogic.Responses;
using MeetAndGo.Shared.Models.Authorization;

namespace MeetAndGoMobile.Services
{
    public interface IAccountService
    {
        Task<IResponse> ConfirmPhoneNumberAsync(MessageConfirmModel model, CancellationToken token);
        Task<IResponse> ConfirmSmsCodeAsync(MessageConfirmModel model, CancellationToken token);
        Task<IResponseData<string>> RegisterAsync(RegisterModel model, CancellationToken token);
        Task<IResponseData<string>> SignInAsync(LoginModel model, CancellationToken token);
    }
}
