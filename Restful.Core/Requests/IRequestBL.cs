using Restful.Core.Base;
using Restful.Core.Requests.Models;
using Restful.Entity.Entities;

namespace Restful.Core.Requests
{
    public interface IRequestBL : IBaseBL<Request, RequestEntity, Guid>
    {
    }
}
