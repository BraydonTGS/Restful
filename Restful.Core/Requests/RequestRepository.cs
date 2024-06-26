using Microsoft.EntityFrameworkCore;
using Restful.Core.Base;
using Restful.Core.Context;
using Restful.Entity.Entities;

namespace Restful.Core.Requests
{
    /// <summary>
    /// Request Repository
    /// </summary>
    public class RequestRepository : BaseRepository<RequestEntity, Guid>, IRequestRepository
    {
        private readonly IDbContextFactory<RestfulDbContext> _contextFactory;
        public RequestRepository(IDbContextFactory<RestfulDbContext> contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        #region GetAllRequestsByCollectionIdAsync
        /// <summary>
        /// Get all of the Requests for the Specified Collection where the Request's CollectionId is equal to the Id of the Given Collection. 
        /// 
        /// Do Not Include any Requests that are soft deleted. 
        /// 
        /// </summary>
        /// <param name="collectionId"></param>
        /// <returns>A Collection of Requests for the Specified User</returns>
        public async Task<ICollection<RequestEntity>> GetAllRequestsByCollectionIdAsync(Guid collectionId)
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            var results = await context.Set<RequestEntity>()
                .Where(x => x.CollectionId == collectionId && !x.IsDeleted)
                .ToListAsync();

            return results;
        }
        #endregion

        #region GetAllRequestsByUserIdAsync
        /// <summary>
        /// Get all of the Requests for the Specified User where the Request's UserId is equal to the Id of the Given User. 
        /// 
        /// Do Not Include any Requests that are part of a Collection
        /// 
        /// Do Not Include any Requests that are soft deleted. 
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>A Collection of Requests for the Specified User</returns>
        public async Task<ICollection<RequestEntity>> GetAllRequestsByUserIdAsync(Guid userId)
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            var results = await context.Set<RequestEntity>()
                .Where(x => x.UserId == userId && x.CollectionId == null && !x.IsDeleted)
                .ToListAsync();

            return results;
        }
        #endregion
    }
}
