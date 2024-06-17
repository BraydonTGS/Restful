using Microsoft.EntityFrameworkCore;
using Restful.Core.Base;
using Restful.Core.Context;
using Restful.Entity.Entities;

namespace Restful.Core.Requests
{
    public class RequestRepository : BaseRepository<RequestEntity, Guid>
    {
        private readonly IDbContextFactory<RestfulDbContext> _contextFactory;
        public RequestRepository(IDbContextFactory<RestfulDbContext> contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
