using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MeetAndGoMobile.Infrastructure.BusinessLogic.Responses;
using MeetAndGoMobile.Infrastructure.Resources;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Essentials;
using Xamarin.Forms.Internals;

namespace MeetAndGoMobile.Infrastructure.BusinessLogic
{
    public abstract class HttpBaseService
    {
        #region Protected methods

        protected Task<IResponseData<string>> GetAsync(Uri url, Dictionary<string, object> headers = null) =>
            GetAsync(url, CancellationToken.None, headers);

        protected async Task<IResponseData<string>> GetAsync(Uri url, CancellationToken token, Dictionary<string, object> headers = null)
        {
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                return new ResponseData<string>(ResponseCode.NoConnection, Strings.E_ConnectionFailed);
            }

            using (var httpClient = CreateHttpClient(headers))
            {
                try
                {
                    var responseCode = ResponseCode.ServerError;

                    var getResult = await httpClient.GetAsync(url, token);
                    var stringContent = await getResult.Content.ReadAsStringAsync();

                    if (getResult.IsSuccessStatusCode)
                    {
                        responseCode = ResponseCode.Ok;

                        return new ResponseData<string>(stringContent, responseCode);
                    }

                    var message = GetResponseMessage(getResult);

                    if (getResult.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        responseCode = ResponseCode.Unauthorized;
                    }

                    return new ResponseData<string>(stringContent, responseCode, message);
                }

                catch (UnauthorizedAccessException unauthorizedAccessException)
                {
                    //_logger.Error(unauthorizedAccessException.Message, unauthorizedAccessException);
                    return new ResponseData<string>(ResponseCode.Unauthorized, unauthorizedAccessException.Message);
                }

                catch (HttpRequestException httpRequestException)
                {
                    var innermostException = httpRequestException.GetBaseException();
                    //_logger.Error(innermostException.Message, httpRequestException);
                    return new ResponseData<string>(ResponseCode.Exception, innermostException.Message);
                }

                catch (TaskCanceledException)
                {
                    // prevent crash
                    throw;
                }

                catch (Exception exception)
                {
                    //_logger.Error(exception, exception.Message);
                    return new ResponseData<string>(ResponseCode.Unknown, exception.Message);
                }
            }
        }

        protected Task<IResponseData<string>> PostAsync(Uri url, Dictionary<string, object> headers = null, HttpContent content = null) =>
            PostAsync(url, CancellationToken.None, headers, content);

        protected async Task<IResponseData<string>> PostAsync(Uri url, CancellationToken token, Dictionary<string, object> headers, HttpContent content)
        {
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                return new ResponseData<string>(ResponseCode.NoConnection, Strings.E_ConnectionFailed);
            }

            using (var httpClient = CreateHttpClient(headers))
            {
                try
                {
                    var responseCode = ResponseCode.ServerError;

                    var postResult = await httpClient.PostAsync(url, content, token);
                    var stringContent = await postResult.Content.ReadAsStringAsync();

                    if (postResult.IsSuccessStatusCode)
                    {
                        responseCode = ResponseCode.Ok;

                        return new ResponseData<string>(stringContent, responseCode);
                    }

                    var message = GetResponseMessage(postResult);

                    if (postResult.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        responseCode = ResponseCode.Unauthorized;
                    }

                    return new ResponseData<string>(stringContent, responseCode, message);
                }

                catch (UnauthorizedAccessException unauthorizedAccessException)
                {
                    //_logger.Error(unauthorizedAccessException.Message, unauthorizedAccessException);
                    return new ResponseData<string>(ResponseCode.Unauthorized, unauthorizedAccessException.Message);
                }

                catch (HttpRequestException httpRequestException)
                {
                    var innermostException = httpRequestException.GetBaseException();
                    //_logger.Error(innermostException.Message, httpRequestException);
                    return new ResponseData<string>(ResponseCode.Exception, innermostException.Message);
                }

                catch (Exception exception)
                {
                    //_logger.Error(exception, exception.Message);
                    return new ResponseData<string>(ResponseCode.Unknown, exception.Message);
                }
            }
        }

        protected IResponseData<T> GetResponseDataFromJson<T>(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return new ResponseData<T>(ResponseCode.JsonFail, Strings.E_JsonCanNotBeEmpy);
            }

            try
            {
                var tValue = JsonConvert.DeserializeObject<IResponseData<T>>(json);
                return tValue;
            }

            catch (NullReferenceException e)
            {
                //_logger.Error(e, string.Empty);
                return new ResponseData<T>(ResponseCode.Exception, e.Message);
            }

            catch (Exception e)
            {
                //_logger.Fatal(e, string.Empty);
                return new ResponseData<T>(ResponseCode.Unknown, e.Message);
            }
        }

        protected IResponse GetResponseFromJson<T>(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return new ResponseData<T>(ResponseCode.JsonFail, Strings.E_JsonCanNotBeEmpy);
            }

            try
            {
                var tValue = JsonConvert.DeserializeObject<IResponse>(json);
                return tValue;
            }

            catch (NullReferenceException e)
            {
                //_logger.Error(e, string.Empty);
                return new Response(ResponseCode.Exception, e.Message);
            }

            catch (Exception e)
            {
                //_logger.Fatal(e, string.Empty);
                return new Response(ResponseCode.Unknown, e.Message);
            }
        }

        #endregion

        #region Private methods

        private HttpClient CreateHttpClient(Dictionary<string, object> headers)
        {
            var httpClient = new HttpClient();
            headers?.ForEach(x => httpClient.DefaultRequestHeaders.Add(x.Key, x.Value?.ToString()));
            return httpClient;
        }

        private string GetResponseMessage(HttpResponseMessage responseMessage)
        {
            return $"Status code: {(int)responseMessage.StatusCode}" +
                   $"\nReason: {responseMessage.ReasonPhrase}";
        }

        #endregion
    }
}
