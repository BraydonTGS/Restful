using Microsoft.EntityFrameworkCore;
using Restful.Core.Base;
using Restful.Core.Context;
using Restful.Entity.Entities;

namespace Restful.Core.Headers
{
    /// <summary>
    /// Request Repository
    /// </summary>
    public class HeaderRepository : BaseRepository<HeaderEntity, Guid>, IHeaderRepository
    {
        public HeaderRepository(IDbContextFactory<RestfulDbContext> contextFactory) : base(contextFactory) { }
    }
}
