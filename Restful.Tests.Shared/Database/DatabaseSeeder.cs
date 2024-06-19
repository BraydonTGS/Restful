using Microsoft.EntityFrameworkCore;
using Restful.Core.Context;
using Restful.Entity.Entities;

namespace Restful.Tests.Shared.Database
{
    public class DatabaseSeeder
    {
        internal Guid _collectionGuid;

        private readonly IDbContextFactory<RestfulDbContext> _contextFactory;

        public DatabaseSeeder(IDbContextFactory<RestfulDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public async Task Seed()
        {
            var context = await _contextFactory.CreateDbContextAsync();

            var user = new UserEntity();
            await context.Users.AddAsync(user);

            var collection = new CollectionEntity();
            collection.UserId = user.Id;

            await context.AddAsync(collection);

            _collectionGuid = collection.Id;

            await context.SaveChangesAsync();
        }

        public async Task Clear()
        {
        }

        public Guid SetCollectionId() => _collectionGuid;
    }
}
