using Restful.Core.Base;
using Restful.Entity.Entities;

namespace Restful.Core.Requests
{
    public interface IRequestRepository : IBaseRepository<RequestEntity, Guid>
    {
    }
}