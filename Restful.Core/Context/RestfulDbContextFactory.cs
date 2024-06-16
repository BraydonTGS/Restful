using Microsoft.EntityFrameworkCore;
using Restful.Core.Database;

namespace Restful.Core.Context
{
    /// <summary>
    /// Implements IDbContextFactory to provide a custom CreateDbContext method for RestfulDbContext.
    /// </summary>
    public class RestfulDbContextFactory : IDbContextFactory<RestfulDbContext>
    {
        public RestfulDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<RestfulDbContext>();

#if DEBUG
            optionsBuilder.UseSqlite(DatabaseInfo.TestDbConnection);

#endif
#if RELEASE
            optionsBuilder.UseSqlite(DatabaseInfo.ProdDbConnection);
#endif         
            return new RestfulDbContext(optionsBuilder.Options);
        }
    }
}