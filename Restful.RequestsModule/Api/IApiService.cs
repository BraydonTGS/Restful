using Restful.RequestsModule.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Restful.RequestsModule.Api
{
    public interface IApiService
    {
        Task<string> ProcessRequestAsync(Request request);
        Task<string> ExecuteHttpRequestAsync(HttpRequestMessage requestMessage);
    }
}
 