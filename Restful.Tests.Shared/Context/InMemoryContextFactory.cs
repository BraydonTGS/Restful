using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Restful.Core.Context;

namespace Restful.Tests.Shared.Context
{
    /// <summary>
    /// In Memory DB Context Factory for SQLITE In-Memory
    /// </summary>
    public class InMemoryContextFactory : IDbContextFactory<RestfulDbContext>, IDisposable
    {
        private readonly SqliteConnection _connection;
        public InMemoryContextFactory()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            using var context = CreateDbContext();
            context.Database.Migrate();
        }

        public RestfulDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<RestfulDbContext>()
                .UseSqlite(_connection)
                .Options;

            return new RestfulDbContext(options);
        }

        public void Dispose() => _connection?.Dispose();
    }
}
