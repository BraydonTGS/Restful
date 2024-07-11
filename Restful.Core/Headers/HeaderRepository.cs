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
        private readonly IDbContextFactory<RestfulDbContext> _contextFactory;
        public HeaderRepository(IDbContextFactory<RestfulDbContext> contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        #region GetAllHeadersByRequestId
        /// <summary>
        /// Get all of the Headers for the Specified Request where the Header's RequestId is equal to the Id of the Given Request. 
        /// 
        /// Do Not Include any Headers that are soft deleted. 
        /// 
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns>A Collection of Headers for the Specified Request</returns>
        public async Task<ICollection<HeaderEntity>> GetAllHeadersByRequestIdAsync(Guid requestId)
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            var headers = await context.Headers
                 .Where(x => x.RequestId == requestId && !x.IsDeleted)
                 .ToListAsync();

            return headers;
        }
        #endregion
    }
}
