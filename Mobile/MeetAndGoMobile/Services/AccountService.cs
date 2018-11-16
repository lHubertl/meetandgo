using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MeetAndGo.Shared.Models.Authorization;
using MeetAndGoMobile.Constants;
using MeetAndGoMobile.Infrastructure.BusinessLogic;
using MeetAndGoMobile.Infrastructure.BusinessLogic.Responses;
using Newtonsoft.Json;

namespace MeetAndGoMobile.Services
{
    internal class AccountService : HttpBaseService, IAccountService
    {
        public async Task<IResponse> ConfirmPhoneNumberAsync(MessageConfirmModel model, CancellationToken token)
        {
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json,
                Encoding.UTF8,
                WebApiConstants.ContentTypeJson);

            var result = await PostAsync(new Uri(WebApiConstants.AccountConfirmPhone), content: content);

            if (result.IsSuccess)
            {
                return GetResponseFromJson<IResponse>(result.Data);
            }

            return new Response(result.Code, result.ErrorMessage);
        }

        public async Task<IResponse> ConfirmSmsCodeAsync(MessageConfirmModel model, CancellationToken token)
        {
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json,
                Encoding.UTF8,
                WebApiConstants.ContentTypeJson);

            var result = await PostAsync(new Uri(WebApiConstants.AccountConfirmCode), content: content);

            if (result.IsSuccess)
            {
                return GetResponseFromJson<IResponse>(result.Data);
            }

            return new Response(result.Code, result.ErrorMessage);
        }

        public async Task<IResponseData<string>> RegisterAsync(RegisterModel model, CancellationToken token)
        {
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json,
                Encoding.UTF8,
                WebApiConstants.ContentTypeJson);

            var result = await PostAsync(new Uri(WebApiConstants.AccountRegister), content: content);

            if (result.IsSuccess)
            {
                return GetResponseDataFromJson<string>(result.Data);
            }

            return new ResponseData<string>(result.Code, result.ErrorMessage);
        }

        public async Task<IResponseData<string>> SignInAsync(LoginModel model, CancellationToken token)
        {
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json,
                Encoding.UTF8,
                WebApiConstants.ContentTypeJson);

            var result = await PostAsync(new Uri(WebApiConstants.AccountSignIn), content: content);

            if (result.IsSuccess)
            {
                return GetResponseDataFromJson<string>(result.Data);
            }

            return new ResponseData<string>(result.Code, result.ErrorMessage);
        }
    }
}
