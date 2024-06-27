using Microsoft.EntityFrameworkCore;
using Restful.Core.Context;
using Restful.Entity.Entities;

namespace Restful.Tests.Shared.Database
{
    public static class DatabaseSeeder
    {

        private static Guid _userId;
        private static Guid _requestId;
        private static Guid _collectionGuid;


        public static async Task Seed(IDbContextFactory<RestfulDbContext> contextFactory)
        {
            using var context = await contextFactory.CreateDbContextAsync();

            var user = new UserEntity()
            {
                FirstName = "Braydon",
                LastName = "Sutherland",
                UserName = "Sweet_Teeth",
                Email = "BraydonSutherland@gmail.com",
            };

            await context.Users.AddAsync(user);

            var collection = new CollectionEntity()
            {
                Title = "LOTR API",
                Description = "A Restful Collection for testing the LOTR API"
            };

            collection.UserId = user.Id;

            await context.AddAsync(collection);

            var requestOne = new RequestEntity()
            {
                Name = "GetAllCharacters",
                Description = "Get All LOTR Characters",
                HttpMethod = Global.Enums.HttpMethod.GET,
                Url = @"https://the-one-api.dev/v2/characters",
                CollectionId = collection.Id
            };

            var requestTwo = new RequestEntity()
            {
                Name = "GetAllBooks",
                Description = "Get All LOTR Books",
                HttpMethod = Global.Enums.HttpMethod.GET,
                Url = @"https://the-one-api.dev/v2/books",
                UserId = user.Id
            };


            await context.AddAsync(requestOne);
            await context.AddAsync(requestTwo);

            _userId = user.Id;
            _requestId = requestOne.Id;
            _collectionGuid = collection.Id;

            await context.SaveChangesAsync();
        }

        public static async Task Clear(IDbContextFactory<RestfulDbContext> contextFactory)
        {
            using var context = await contextFactory.CreateDbContextAsync();

            context.Users.RemoveRange();
            context.Collections.RemoveRange();
            context.Responses.RemoveRange();

            await context.SaveChangesAsync();
        }

        public static Guid GetUserId() => _userId;
        public static Guid GetRequestId() => _requestId;
        public static Guid GetCollectionId() => _collectionGuid;
    }
}
