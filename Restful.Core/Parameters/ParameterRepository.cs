using Microsoft.EntityFrameworkCore;
using Restful.Core.Base;
using Restful.Core.Context;
using Restful.Entity.Entities;

namespace Restful.Core.Parameters
{
    /// <summary>
    /// Parameter Repository
    /// </summary>
    public class ParameterRepository : BaseRepository<ParameterEntity, Guid>, IParameterRepository
    {
        public ParameterRepository(IDbContextFactory<RestfulDbContext> contextFactory) : base(contextFactory) { }
    }
}
