using Prism.Ioc;
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
        private readonly DatabaseSeeder _databaseSeeder;
        public RequestBLTests()
        {
            _requestBL = _containerProvider.Resolve<IRequestBL>();
            _databaseSeeder = _containerProvider.Resolve<DatabaseSeeder>();
        }

        [TestInitialize]
        public async Task TestInitialize()
        {
            await _databaseSeeder.Seed();
        }

        [TestMethod]
        public async Task CreateRequest_ShouldCreateRequest()
        {
            var request = await _requestBL.CreateAsync(new Request(true) { CollectionId = _databaseSeeder.SetCollectionId()});
            Assert.IsNotNull(request);
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            await _databaseSeeder.Clear();
        }
    }
}
