using Microsoft.EntityFrameworkCore;
using Restful.Core.Context;

namespace Restful.Tests.Shared.Database
{
    public class DatabaseSeeder
    {
        private readonly IDbContextFactory<RestfulDbContext> _contextFactory;

        public DatabaseSeeder(IDbContextFactory<RestfulDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public async Task Seed()
        {
        }

        public async Task Clear()
        {
        }
    }
}
