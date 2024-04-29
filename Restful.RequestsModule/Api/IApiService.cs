using Restful.RequestsModule.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Restful.RequestsModule.Api
{
    public interface IApiService
    {
        Task<string> ProcessRequestAsync(Request request);
        Task<string> GetApiResponseAsync(HttpRequestMessage requestMessage);
        Task<string> PostApiResponseAsync(HttpRequestMessage requestMessage);
        Task<string> PutApiResponseAsync(HttpRequestMessage requestMessage);
        Task<string> DeleteApiResponseAsync(HttpRequestMessage requestMessage);
        Task<string> PatchApiResponseAsync(HttpRequestMessage requestMessage);
    }
}
 