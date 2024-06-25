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

        [TestCleanup]
        public async Task TestCleanup()
        {
            await DatabaseSeeder.Clear(_dbContextFactory);
        }
    }
}
