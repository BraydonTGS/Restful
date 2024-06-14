using Microsoft.EntityFrameworkCore;
using Restful.Core.Connection;

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
            optionsBuilder.UseSqlite(Hidden.GetConnectionString());
            return new RestfulDbContext(optionsBuilder.Options);
        }
    }
}
