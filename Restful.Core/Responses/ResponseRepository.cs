using Microsoft.EntityFrameworkCore;
using Restful.Core.Base;
using Restful.Core.Context;
using Restful.Entity.Entities;

namespace Restful.Core.Responses
{
    public class ResponseRepository : BaseRepository<ResponseEntity, Guid>, IResponseRepository
    {
        public ResponseRepository(IDbContextFactory<RestfulDbContext> contextFactory) : base(contextFactory) { }
    }
}
