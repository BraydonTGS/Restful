using Restful.RequestsModule.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Restful.RequestsModule.Api
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
 