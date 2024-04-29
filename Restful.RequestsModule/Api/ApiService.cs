using CommunityToolkit.Mvvm.Messaging.Messages;
using Newtonsoft.Json;
using Restful.RequestsModule.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using HttpMethod = Restful.Core.Enums.HttpMethod;

namespace Restful.RequestsModule.Api
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _client;

        public ApiService(HttpClient client) => _client = client;

        #region ProcessRequestAsync
        public async Task<string> ProcessRequestAsync(Request request)
        {
            string results;
            // Build the HTTP Message From the Request //
            var requestMessage = BuildRequestMessageFromRequest(request);
            switch (request.HttpMethod)
            {
                case HttpMethod.GET:
                    results = await GetApiResponseAsync(requestMessage);
                    break;
                case HttpMethod.POST:
                    results = await PostApiResponseAsync(requestMessage);
                    break;
                case HttpMethod.PUT:
                    results = await PutApiResponseAsync(requestMessage);
                    break;
                case HttpMethod.DELETE:
                    results = await DeleteApiResponseAsync(requestMessage);
                    break;
                case HttpMethod.PATCH:
                    results = await PatchApiResponseAsync(requestMessage);
                    break;
                default:
                    results = "Unsupported HTTP method";
                    break;
            }

            return results;
        }
        #endregion

        #region BuildRequestMessageFromRequest
        private HttpRequestMessage BuildRequestMessageFromRequest(Request request)
        {
            var httpRequestMessage = new HttpRequestMessage(new System.Net.Http.HttpMethod(request.HttpMethod.ToString()), request.Url);

            foreach (var header in request.Headers)
                if (header.Enabled)
                    httpRequestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value);

            return httpRequestMessage;
        }
        #endregion

        #region GetApiResponseAsync
        /// <summary>
        /// Get API Response Async Formatted as JSON
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<string> GetApiResponseAsync(HttpRequestMessage requestMessage)
        {
            try
            {
                var response = await _client.SendAsync(requestMessage);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                var jObject = JsonConvert.DeserializeObject(responseBody);
                var formattedJson = JsonConvert.SerializeObject(jObject, Formatting.Indented);

                return formattedJson;
            }
            catch (HttpRequestException e)
            {
                return $"HttpRequestException: {e.Message}";
            }
            catch (Exception e)
            {
                return $"Unexpected error: {e.Message}";
            }
        }
        #endregion

        #region PostApiResponseAsync
        public Task<string> PostApiResponseAsync(HttpRequestMessage requestMessage)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region PutApiResponseAsync
        public Task<string> PutApiResponseAsync(HttpRequestMessage requestMessage)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region DeleteApiResponseAsync
        public Task<string> DeleteApiResponseAsync(HttpRequestMessage requestMessage)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region PatchApiResponseAsync
        public Task<string> PatchApiResponseAsync(HttpRequestMessage requestMessage)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
