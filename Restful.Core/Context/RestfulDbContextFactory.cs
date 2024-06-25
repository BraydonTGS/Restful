using Microsoft.EntityFrameworkCore;
using Restful.Core.Database;

namespace Restful.Core.Context
{
    /// <summary>
    /// Implements IDbContextFactory to provide a custom CreateDbContext method for RestfulDbContext.
    /// </summary>
    public class RestfulDbContextFactory : IDbContextFactory<RestfulDbContext>
    {
        private readonly string _dbName;
        public RestfulDbContextFactory(string dbName)
        {
            _dbName = dbName;
        }
        public RestfulDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<RestfulDbContext>();

            optionsBuilder.UseSqlite($"Data Source={DatabaseInfo.DbDirectory}{_dbName}");

            return new RestfulDbContext(optionsBuilder.Options);
        }
    }
}