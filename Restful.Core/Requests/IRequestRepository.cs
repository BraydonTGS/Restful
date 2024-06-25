using Restful.Core.Base;
using Restful.Core.Requests.Models;
using Restful.Entity.Entities;

namespace Restful.Core.Requests
{
    public interface IRequestRepository : IBaseRepository<RequestEntity, Guid>
    {
        Task<ICollection<RequestEntity>> GetAllRequestsByCollectionIdAsync(Guid collectionId);
        Task<ICollection<RequestEntity>> GetAllRequestsByUserIdAsync(Guid userId);
    }
}