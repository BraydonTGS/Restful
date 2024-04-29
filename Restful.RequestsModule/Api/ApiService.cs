using Newtonsoft.Json;
using Restful.RequestsModule.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Restful.RequestsModule.Api
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _client;

        public ApiService(HttpClient client) => _client = client;

        #region ProcessRequestAsync
        public async Task<string> ProcessRequestAsync(Request request)
        {
            try
            {
                var requestMessage = BuildRequestMessageFromRequest(request);
                var results = await ExecuteHttpRequestAsync(requestMessage);
                return results;
            }
            catch (Exception ex) { throw; }
        }
        #endregion

        #region BuildRequestMessageFromRequest
        private HttpRequestMessage BuildRequestMessageFromRequest(Request request)
        {
            try
            {
                var httpRequestMessage = new HttpRequestMessage(new System.Net.Http.HttpMethod(request.HttpMethod.ToString()), request.Url);

                foreach (var header in request.Headers)
                    if (header.Enabled)
                        httpRequestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value);

                return httpRequestMessage;
            }
            catch (Exception) { throw; }
        }
        #endregion

        #region GetApiResponseAsync
        /// <summary>
        /// Get API Response Async Formatted as JSON
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<string> ExecuteHttpRequestAsync(HttpRequestMessage requestMessage)
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
            catch (Exception) { throw; }

        }
        #endregion
    }
}
