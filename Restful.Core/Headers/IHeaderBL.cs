using Restful.Core.Base;
using Restful.Core.Headers.Models;
using Restful.Entity.Entities;

namespace Restful.Core.Headers
{
    public interface IHeaderBL : IBaseBL<Header, HeaderEntity, Guid>
    {
        Task<ICollection<Header>> GetAllHeadersByRequestIdAsync(Guid requestId);
    }
}