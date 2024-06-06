using Restful.Core.Requests.Models;
using System.Net.Http;

namespace Restful.Core.Requests
{
    /// <summary>
    /// IRequestApiService
    /// </summary>
    public interface IRequestApiService
    {
        Task<string> ProcessRequestAsync(Request request);
        Task<string> ExecuteHttpRequestAsync(HttpRequestMessage requestMessage);
    }
}
