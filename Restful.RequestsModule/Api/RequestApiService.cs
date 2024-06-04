using Newtonsoft.Json;
using Restful.RequestsModule.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Restful.RequestsModule.Api
{
    /// <summary>
    /// Request Api Service
    /// </summary>
    public class RequestApiService : IRequestApiService
    {
        private readonly HttpClient _client;

        public RequestApiService(HttpClient client) => _client = client;

        #region ProcessRequestAsync
        /// <summary>
        /// Process the User's Request Async
        /// 
        /// Generate the Request Message
        /// 
        /// Execute the HTTP Request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string> ProcessRequestAsync(Request request)
        {
            try
            {
                var requestMessage = BuildRequestMessageFromRequest(request);
                var results = await ExecuteHttpRequestAsync(requestMessage);
                return results;
            }
            catch (Exception) { throw; }
        }
        #endregion

        #region BuildRequestMessageFromRequest
        /// <summary>
        /// Using the Request Object From the User, Build the HTTP Request Message
        /// that will be sent with the client
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private HttpRequestMessage BuildRequestMessageFromRequest(Request request)
        {
            try
            {
                var httpRequestMessage = new HttpRequestMessage(new HttpMethod(request.HttpMethod.ToString()), request.Url);

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
