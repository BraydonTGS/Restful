using Restful.Core.Base;
using Restful.Entity.Entities;

namespace Restful.Core.Responses
{
    public interface IResponseRepository : IBaseRepository<ResponseEntity, Guid> { }
}