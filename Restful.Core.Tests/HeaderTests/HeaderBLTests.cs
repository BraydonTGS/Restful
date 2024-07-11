using Microsoft.EntityFrameworkCore;
using Prism.Ioc;
using Restful.Core.Context;
using Restful.Core.Headers;
using Restful.Core.Headers.Models;
using Restful.Tests.Shared.Base;
using Restful.Tests.Shared.Database;

namespace Restful.Core.Tests.HeaderTests
{
    [TestClass]
    public class HeaderBLTests : TestBase
    {
        private readonly IHeaderBL _headerBL;
        private readonly IDbContextFactory<RestfulDbContext> _dbContextFactory;
        public HeaderBLTests()
        {
            _headerBL = _containerProvider.Resolve<IHeaderBL>();
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
            Assert.IsNotNull(_headerBL);
        }

        [TestMethod]
        public async Task GetAllHeadersAsync_Success()
        {
            var results = await _headerBL.GetAllAsync();

            Assert.AreEqual(3, results.Count());
        }


        [TestMethod]
        public async Task GetHeaderByIdAsync_Success()
        {
            var header = await _headerBL.GetByIdAsync(DatabaseSeeder.GetHeaderId());

            Assert.IsNotNull(header);

        }

        [TestMethod]
        public async Task CreateHeaderAsync_Success()
        {
            var header = new Header()
            {
                Key = "Content-Type",
                Value = "application/json", 
                Enabled = true,
                RequestId = DatabaseSeeder.GetRequestId()
            };

            header = await _headerBL.CreateAsync(header);

            Assert.IsNotNull(header);
        }

        [TestMethod]
        public async Task UpdateToDoItemAsync_Success()
        {
            var header = new Header()
            {
                Key = "Content-Type",
                Value = "application/json",
                Enabled = true,
                RequestId = DatabaseSeeder.GetRequestId()
            };

            header = await _headerBL.CreateAsync(header);

            Assert.IsNotNull(header);

            header = await _headerBL.UpdateAsync(header);

            Assert.IsNotNull(header);
        }

        [TestMethod]
        public async Task SoftDeleteToDoItemAsync_Success()
        {
            var header = new Header()
            {
                Key = "Content-Type",
                Value = "application/json",
                Enabled = true,
                RequestId = DatabaseSeeder.GetRequestId()
            };

            header = await _headerBL.CreateAsync(header);

            Assert.IsNotNull(header);

            var allHeaders = await _headerBL.GetAllAsync();

            Assert.AreEqual(4, allHeaders.Count());

            var results = await _headerBL.SoftDeleteAsync(header.Id);

            Assert.IsNotNull(results);
            Assert.IsTrue(results);

            allHeaders = await _headerBL.GetAllAsync();

            Assert.AreEqual(3, allHeaders.Count());
        }

        [TestMethod]
        public async Task HardDeleteToDoItemAsync_Success()
        {
            var header = new Header()
            {
                Key = "Content-Type",
                Value = "application/json",
                Enabled = true,
                RequestId = DatabaseSeeder.GetRequestId()
            };

            header = await _headerBL.CreateAsync(header);

            Assert.IsNotNull(header);

            var results = await _headerBL.HardDeleteAsync(header.Id);

            Assert.IsTrue(results);
        }

        [TestMethod]
        public async Task RestoreToDoItemAsync_Success()
        {
            var header = new Header()
            {
                Key = "Content-Type",
                Value = "application/json",
                Enabled = true,
                RequestId = DatabaseSeeder.GetRequestId()
            };

            header = await _headerBL.CreateAsync(header);

            Assert.IsNotNull(header);

            await _headerBL.SoftDeleteAsync(header.Id);

            var allHeaders = await _headerBL.GetAllAsync();

            Assert.AreEqual(3, allHeaders.Count());

            var results = await _headerBL.RestoreAsync(header.Id);

            Assert.IsTrue(results);

            allHeaders = await _headerBL.GetAllAsync();

            Assert.AreEqual(4, allHeaders.Count());
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            await DatabaseSeeder.Clear(_dbContextFactory);
        }
    }
}
