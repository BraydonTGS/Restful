using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using Restful.Core.Context;
using Restful.Core.Passwords;
using Restful.Core.Passwords.Model;
using Restful.Entity.Entities;

namespace Restful.Tests.Shared.Database
{
    public static class DatabaseSeeder
    {

        private static Guid _userId;
        private static Guid _secondUserId;
        private static Guid _requestId;
        private static Guid _collectionId;
        private static Guid _headerId;
        private static Guid _parameterId;

        public static Guid GetUserId() => _userId;
        public static Guid GetSecondUserId() => _secondUserId;
        public static Guid GetRequestId() => _requestId;
        public static Guid GetCollectionId() => _collectionId;
        public static Guid GetHeaderId() => _headerId;
        public static Guid GetParameterId() => _parameterId;

        public static async Task Seed(
            IDbContextFactory<RestfulDbContext> contextFactory)
        {
            using var context = await contextFactory.CreateDbContextAsync();

            // User //
            UserEntity user, secondUser;
            GenerateUserEntities(out user, out secondUser);

            await context.Users.AddAsync(user);
            await context.Users.AddAsync(secondUser);

            // Collection //
            CollectionEntity collection;
            GenerateCollectionEntity(user.Id, out collection);

            await context.AddAsync(collection);

            // Requests //
            RequestEntity requestOne, requestTwo;
            GenerateRequestEntities(user.Id, collection.Id, out requestOne, out requestTwo);

            await context.AddAsync(requestOne);
            await context.AddAsync(requestTwo);

            // Headers //
            HeaderEntity headerOne, headerTwo, headerThree;
            GenerateHeaderEntities(requestOne.Id, out headerOne, out headerTwo, out headerThree);

            await context.AddAsync(headerOne);
            await context.AddAsync(headerTwo);
            await context.AddAsync(headerThree);

            // Parameters //
            ParameterEntity parameterOne, parameterTwo, parameterThree;
            GenerateParameterEntities(requestOne.Id, out  parameterOne, out parameterTwo, out parameterThree);

            await context.AddAsync(parameterOne);
            await context.AddAsync(parameterTwo);
            await context.AddAsync(parameterThree);

            // Password //
            PasswordEntity password;
            GeneratePasswordEntity(user.Id, out password);

            await context.AddAsync(password);

            // Set Ids //
            _userId = user.Id;
            _secondUserId = secondUser.Id;
            _requestId = requestOne.Id;
            _collectionId = collection.Id;
            _headerId = headerOne.Id;
            _parameterId = parameterOne.Id;

            // Save Changes //
            await context.SaveChangesAsync();
        }

        #region Clear
        /// <summary>
        /// Clear the Database
        /// </summary>
        /// <param name="contextFactory"></param>
        /// <returns></returns>
        public static async Task Clear(IDbContextFactory<RestfulDbContext> contextFactory)
        {
            using var context = await contextFactory.CreateDbContextAsync();

            context.Users.RemoveRange();
            context.Collections.RemoveRange();
            context.Responses.RemoveRange();
            context.Headers.RemoveRange();

            await context.SaveChangesAsync();
        }
        #endregion

        #region Generate Entities For Seeding
        private static void GenerateCollectionEntity(
            Guid userId,
            out CollectionEntity entity)
        {
            entity = new CollectionEntity()
            {
                Title = "LOTR API",
                Description = "A Restful Collection for testing the LOTR API",
                UserId = userId
            };
        }

        private static void GenerateUserEntities(
            out UserEntity user,
            out UserEntity secondUser)
        {
            user = new UserEntity()
            {
                FirstName = "Daniel",
                LastName = "Aguirre",
                Email = "RedRain@gmail.com",
                UserName = "RedxRain",
                Description = "JarJar of Unit Tests",

            };
            secondUser = new UserEntity()
            {
                FirstName = "Braydon",
                LastName = "Sutherland",
                Email = "BrayDog@gmail.com",
                UserName = "Geo",
                Description = "App Admin",
            };
        }

        static void GenerateRequestEntities(
            Guid userId,
            Guid collectionId,
            out RequestEntity requestOne,
            out RequestEntity requestTwo)
        {
            requestOne = new RequestEntity()
            {
                Name = "GetAllCharacters",
                Description = "Get All LOTR Characters",
                HttpMethod = Global.Enums.HttpMethod.GET,
                Url = @"https://the-one-api.dev/v2/characters",
                CollectionId = collectionId
            };
            requestTwo = new RequestEntity()
            {
                Name = "GetAllBooks",
                Description = "Get All LOTR Books",
                HttpMethod = Global.Enums.HttpMethod.GET,
                Url = @"https://the-one-api.dev/v2/books",
                UserId = userId
            };
        }

        private static void GenerateHeaderEntities(
            Guid requestId,
            out HeaderEntity headerOne,
            out HeaderEntity headerTwo,
            out HeaderEntity headerThree)
        {
            headerOne = new HeaderEntity()
            {
                Key = "Accept",
                Value = "application/json",
                Enabled = true,
                RequestId = requestId
            };
            headerTwo = new HeaderEntity()
            {
                Key = "Content-Type",
                Value = "*/*",
                Enabled = true,
                RequestId = requestId
            };
            headerThree = new HeaderEntity()
            {
                Key = "Connection",
                Value = "keep-alive",
                Enabled = true,
                RequestId = requestId
            };
        }

        private static void GenerateParameterEntities(
            Guid requestId,
            out ParameterEntity parameterOne,
            out ParameterEntity parameterTwo,
            out ParameterEntity parameterThree)
        {
            parameterOne = new ParameterEntity()
            {
                Key = "Id",
                Value = "1251",
                Enabled = true,
                RequestId = requestId
            };
            parameterTwo = new ParameterEntity()
            {
                Key = "Name",
                Value = "Frodo",
                Enabled = true,
                RequestId = requestId
            };
            parameterThree = new ParameterEntity()
            {
                Key = "Book",
                Value = "ReturnOfTheKing",
                Enabled = true,
                RequestId = requestId
            };
        }

        private static void GeneratePasswordEntity(
            Guid userId,
            out PasswordEntity password)
        {
            IPasswordHasher<Password> passwordHasher = new PasswordHasher();

            var (hash, salt) = passwordHasher.HashPassword("YodaIsMyMentor");

            password = new PasswordEntity(userId, hash, salt);
        }
        #endregion
    }
}
