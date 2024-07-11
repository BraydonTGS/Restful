using Restful.Core.Base;
using Restful.Entity.Entities;

namespace Restful.Core.Headers
{
    public interface IHeaderRepository : IBaseRepository<HeaderEntity, Guid>
    {
        Task<ICollection<HeaderEntity>> GetAllHeadersByRequestIdAsync(Guid requestId);
    }
}