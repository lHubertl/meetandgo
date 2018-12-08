using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MeetAndGo.Shared.BusinessLogic.Responses;
using MeetAndGo.Shared.Models;
using MeetAndGo.Shared.Models.Authorization;
using MeetAndGoMobile.Constants;
using MeetAndGoMobile.Infrastructure.BusinessLogic;
using MeetAndGoMobile.Infrastructure.BusinessLogic.Repositories;
using Newtonsoft.Json;

namespace MeetAndGoMobile.Services
{
    internal class AccountService : HttpBaseService, IAccountService
    {
        private readonly IDataRepository _dataRepository;

        public AccountService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public async Task<IResponse> ConfirmPhoneNumberAsync(MessageConfirmModel model, CancellationToken token)
        {
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json,
                Encoding.UTF8,
                WebApiConstants.ContentTypeJson);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await PostAsync(new Uri(WebApiConstants.AccountConfirmPhone), content: content);

            if (result.Success)
            {
                return GetResponseFromJson<IResponse>(result.Data);
            }

            return new Response(result.Code, result.Message);
        }

        public async Task<IResponse> ConfirmSmsCodeAsync(MessageConfirmModel model, CancellationToken token)
        {
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json,
                Encoding.UTF8,
                WebApiConstants.ContentTypeJson);

            var result = await PostAsync(new Uri(WebApiConstants.AccountConfirmCode), content: content);

            if (result.Success)
            {
                return GetResponseFromJson<IResponse>(result.Data);
            }

            return new Response(result.Code, result.Message);
        }

        public async Task<IResponseData<UserModel>> GetUserModelAsync(CancellationToken token)
        {
            var authToken = _dataRepository.Get<string>(DataType.Token);
            var headers = new Dictionary<string, object>
            {
                { WebApiConstants.Authorization, $"Bearer {authToken}" }
            };

            var result = await GetAsync(new Uri(WebApiConstants.AccountUserModel), headers);

            if (result.Success)
            {
                return GetResponseDataFromJson<UserModel>(result.Data);
            }

            return new ResponseData<UserModel>(result.Code, result.Message);
        }

        public async Task<IResponseData<string>> RegisterAsync(RegisterModel model, CancellationToken token)
        {
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json,
                Encoding.UTF8,
                WebApiConstants.ContentTypeJson);

            var result = await PostAsync(new Uri(WebApiConstants.AccountRegister), content: content);

            if (result.Success)
            {
                return GetResponseDataFromJson<string>(result.Data);
            }

            return new ResponseData<string>(result.Code, result.Message);
        }

        public async Task<IResponseData<string>> SignInAsync(LoginModel model, CancellationToken token)
        {
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json,
                Encoding.UTF8,
                WebApiConstants.ContentTypeJson);

            var result = await PostAsync(new Uri(WebApiConstants.AccountSignIn), content: content);

            if (result.Success)
            {
                return GetResponseDataFromJson<string>(result.Data);
            }

            return new ResponseData<string>(result.Code, result.Message);
        }
    }
}
