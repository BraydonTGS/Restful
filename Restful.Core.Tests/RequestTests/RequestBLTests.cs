using Microsoft.EntityFrameworkCore;
using Prism.Ioc;
using Restful.Core.Context;
using Restful.Core.Requests;
using Restful.Core.Requests.Models;
using Restful.Tests.Shared.Base;
using Restful.Tests.Shared.Database;

namespace Restful.Core.Tests.RequestTests
{
    [TestClass]
    public class RequestBLTests : TestBase
    {
        private readonly IRequestBL _requestBL;
        private readonly IDbContextFactory<RestfulDbContext> _dbContextFactory;
        public RequestBLTests()
        {
            _requestBL = _containerProvider.Resolve<IRequestBL>();
            _dbContextFactory = _containerProvider.Resolve<IDbContextFactory<RestfulDbContext>>();
        }

        [TestInitialize]
        public async Task TestInitialize()
        {
            await DatabaseSeeder.Seed(_dbContextFactory);
        }

        [TestMethod]
        public void ConstructorNotNull_Success()
        {
            Assert.IsNotNull(_requestBL);
        }

        [TestMethod]
        public async Task GetAllRequestsAsync_Success()
        {
            var results = await _requestBL.GetAllAsync();

            Assert.AreEqual(2, results.Count());
        }

        [TestMethod]
        public async Task GetAllRequestsByCollectionIdAsync_Success()
        {
            var results = await _requestBL.GetAllRequestsByCollectionIdAsync(DatabaseSeeder.GetCollectionId());

            Assert.AreEqual(1, results.Count);
        }

        [TestMethod]
        public async Task GetAllRequestsByUserIdAsync_Success()
        {
            var results = await _requestBL.GetAllRequestsByUserIdAsync(DatabaseSeeder.GetUserId());

            Assert.AreEqual(1, results.Count);
        }

        [TestMethod]
        public async Task GetRequestByIdAsync_Success()
        {
            var request = await _requestBL.GetByIdAsync(DatabaseSeeder.GetRequestId());

            Assert.IsNotNull(request);
            Assert.AreEqual("GetAllCharacters", request.Name);
            Assert.AreEqual("Get All LOTR Characters", request.Description);
            Assert.AreEqual(Global.Enums.HttpMethod.GET, request.HttpMethod);
            Assert.AreEqual("https://the-one-api.dev/v2/characters", request.Url);
            Assert.AreEqual(DatabaseSeeder.GetCollectionId(), request.CollectionId);
        }

        [TestMethod]
        public async Task CreateRequestAsync_Success()
        {
            var request = new Request(true)
            {
                Name = "GetCharacterById",
                Description = "Get a specific character by id",
                HttpMethod = Global.Enums.HttpMethod.GET,
                Url = "https://the-one-api.dev/v2/characters/1",
                CollectionId = DatabaseSeeder.GetCollectionId()
            };

            request = await _requestBL.CreateAsync(request);

            Assert.IsNotNull(request);
            Assert.AreEqual("GetCharacterById", request.Name);
            Assert.AreEqual("Get a specific character by id", request.Description);
            Assert.AreEqual(Global.Enums.HttpMethod.GET, request.HttpMethod);
            Assert.AreEqual("https://the-one-api.dev/v2/characters/1", request.Url);
            Assert.AreEqual(DatabaseSeeder.GetCollectionId(), request.CollectionId);
        }

        [TestMethod]
        public async Task UpdateToDoItemAsync_Success()
        {
            var request = new Request(true)
            {
                Name = "GetCharacterById",
                Description = "Get a specific character by id",
                HttpMethod = Global.Enums.HttpMethod.GET,
                Url = "https://the-one-api.dev/v2/characters/1",
                CollectionId = DatabaseSeeder.GetCollectionId()
            };

            request = await _requestBL.CreateAsync(request);

            Assert.IsNotNull(request);

            request.Name = "GetBookById";
            request.Description = "Get a specific book by id";
            request.HttpMethod = Global.Enums.HttpMethod.GET;
            request.Url = "https://the-one-api.dev/v2/books/1";

            request = await _requestBL.UpdateAsync(request);

            Assert.IsNotNull(request);
        }

        [TestMethod]
        public async Task SoftDeleteToDoItemAsync_Success()
        {
            var request = new Request(true)
            {
                Name = "GetCharacterById",
                Description = "Get a specific character by id",
                HttpMethod = Global.Enums.HttpMethod.GET,
                Url = "https://the-one-api.dev/v2/characters/1",
                CollectionId = DatabaseSeeder.GetCollectionId()
            };

            request = await _requestBL.CreateAsync(request);

            Assert.IsNotNull(request);

            var allToDoItems = await _requestBL.GetAllAsync();

            Assert.AreEqual(3, allToDoItems.Count());

            var results = await _requestBL.SoftDeleteAsync(request.Id);

            Assert.IsNotNull(results);
            Assert.IsTrue(results);

            allToDoItems = await _requestBL.GetAllAsync();

            Assert.AreEqual(2, allToDoItems.Count());
        }

        [TestMethod]
        public async Task HardDeleteToDoItemAsync_Success()
        {
            var request = new Request(true)
            {
                Name = "GetCharacterById",
                Description = "Get a specific character by id",
                HttpMethod = Global.Enums.HttpMethod.GET,
                Url = "https://the-one-api.dev/v2/characters/1",
                CollectionId = DatabaseSeeder.GetCollectionId()
            };

            request = await _requestBL.CreateAsync(request);

            Assert.IsNotNull(request);

            var results = await _requestBL.HardDeleteAsync(request.Id);

            Assert.IsNotNull(results);
            Assert.IsTrue(results);
        }

        [TestMethod]
        public async Task RestoreToDoItemAsync_Success()
        {
            var request = new Request(true)
            {
                Name = "GetCharacterById",
                Description = "Get a specific character by id",
                HttpMethod = Global.Enums.HttpMethod.GET,
                Url = "https://the-one-api.dev/v2/characters/1",
                CollectionId = DatabaseSeeder.GetCollectionId()
            };

            request = await _requestBL.CreateAsync(request);

            Assert.IsNotNull(request);

            await _requestBL.SoftDeleteAsync(request.Id);

            var allToDoItems = await _requestBL.GetAllAsync();

            Assert.AreEqual(2, allToDoItems.Count());

            var results = await _requestBL.RestoreAsync(request.Id);

            Assert.IsNotNull(results);
            Assert.IsTrue(results);

            allToDoItems = await _requestBL.GetAllAsync();

            Assert.AreEqual(3, allToDoItems.Count());
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            await DatabaseSeeder.Clear(_dbContextFactory);
        }
    }
}
