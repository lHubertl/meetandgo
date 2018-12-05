using System.Threading;
using System.Threading.Tasks;
using MeetAndGo.Shared.BusinessLogic.Responses;
using MeetAndGo.Shared.Models.Authorization;

namespace MeetAndGoMobile.Services.FakeServices
{
    class FakeAccountService : IAccountService
    {
        public async Task<IResponse> ConfirmPhoneNumberAsync(MessageConfirmModel model, CancellationToken token)
        {
            return new Response(ResponseCode.Ok);
        }

        public async Task<IResponse> ConfirmSmsCodeAsync(MessageConfirmModel model, CancellationToken token)
        {
            return new Response(ResponseCode.Ok);
        }

        public async Task<IResponseData<string>> RegisterAsync(RegisterModel model, CancellationToken token)
        {
            return new ResponseData<string>("1234");
        }

        public async Task<IResponseData<string>> SignInAsync(LoginModel model, CancellationToken token)
        {
            return new ResponseData<string>("1234");
        }
    }
}
