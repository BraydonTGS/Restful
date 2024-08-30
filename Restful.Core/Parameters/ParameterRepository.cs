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
        private readonly IDbContextFactory<RestfulDbContext> _contextFactory;
        public ParameterRepository(IDbContextFactory<RestfulDbContext> contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        #region GetAllParametersByRequestId
        /// <summary>
        /// Get all of the Parameters for the Specified Request where the Parameter's RequestId is equal to the Id of the Given Request. 
        /// 
        /// Do Not Include any Parameters that are soft deleted. 
        /// 
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns>A Collection of Parameters for the Specified Request</returns>
        public async Task<ICollection<ParameterEntity>> GetAllParametersByRequestIdAsync(Guid requestId)
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            var parameters = await context.Parameters
                 .Where(x => x.RequestId == requestId && !x.IsDeleted)
                 .ToListAsync();

            return parameters;
        }
        #endregion
    }
}
