using Restful.Core.Base;
using Restful.Entity.Entities;

namespace Restful.Core.Parameters
{
    public interface IParameterRepository : IBaseRepository<ParameterEntity, Guid>
    {
        Task<ICollection<ParameterEntity>> GetAllParametersByRequestIdAsync(Guid requestId);
    }
}