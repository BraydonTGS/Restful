using Restful.Core.Requests.Models;

namespace Restful.Core.Requests
{
    public interface IRequestUpsertBL
    {
        Task<Request> UpsertRequestAsync(Request request);
    }
}