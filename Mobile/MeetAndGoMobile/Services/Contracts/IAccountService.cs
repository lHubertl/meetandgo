using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MeetAndGo.Shared.BusinessLogic.Responses;
using MeetAndGo.Shared.Models;
using MeetAndGo.Shared.Models.Authorization;

namespace MeetAndGoMobile.Services.Contracts
{
    public interface IAccountService
    {
        Task<IResponse> ConfirmPhoneNumberAsync(MessageConfirmModel model, CancellationToken token);
        Task<IResponse> ConfirmSmsCodeAsync(MessageConfirmModel model, CancellationToken token);
        Task<IResponseData<string>> RegisterAsync(RegisterModel model, CancellationToken token);
        Task<IResponseData<string>> SignInAsync(LoginModel model, CancellationToken token);
        Task<IResponseData<UserModel>> GetUserModelAsync(CancellationToken token);
        Task<IResponse> UploadUserPhotoAsync(string userName, string name, string type, MemoryStream stream, CancellationToken token);
        Task<IResponse> UpdateUserInfoAsync(UserModel model, CancellationToken token);
    }
}
