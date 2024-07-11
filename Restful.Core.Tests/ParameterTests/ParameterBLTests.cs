using Microsoft.EntityFrameworkCore;
using Prism.Ioc;
using Restful.Core.Context;
using Restful.Core.Parameters;
using Restful.Tests.Shared.Base;
using Restful.Tests.Shared.Database;

namespace Restful.Core.Tests.ParameterTests
{
    [TestClass]
    public class ParameterBLTests : TestBase
    {
        private readonly IParameterBL _parameterBL;
        private readonly IDbContextFactory<RestfulDbContext> _dbContextFactory;
        public ParameterBLTests()
        {
            _parameterBL = _containerProvider.Resolve<IParameterBL>();
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
            Assert.IsNotNull(_parameterBL);
        }

        [TestMethod]
        public async Task GetAllParametersAsync_Success()
        {
            var results = await _parameterBL.GetAllAsync();

            Assert.AreEqual(3, results.Count());
        }

        [TestMethod]
        public async Task GetParameterByIdAsync_Success()
        {
            var parameter = await _parameterBL.GetByIdAsync(DatabaseSeeder.GetParameterId());

            Assert.IsNotNull(parameter);
            Assert.AreEqual("Id", parameter.Key);
            Assert.AreEqual("1251", parameter.Value);
            Assert.AreEqual(true, parameter.Enabled);

        }

        [TestMethod]
        public async Task CreateParameterAsync_Success()
        {
            var parameter = ModelCreationHelper.GenerateParameter();

            parameter = await _parameterBL.CreateAsync(parameter);

            Assert.IsNotNull(parameter);
            Assert.AreEqual("Id", parameter.Key);
            Assert.AreEqual("134", parameter.Value);
            Assert.AreEqual(true, parameter.Enabled);
        }

        [TestMethod]
        public async Task UpdateToDoItemAsync_Success()
        {
            var parameter = ModelCreationHelper.GenerateParameter();

            parameter = await _parameterBL.CreateAsync(parameter);

            Assert.IsNotNull(parameter);
            Assert.AreEqual("Id", parameter.Key);
            Assert.AreEqual("134", parameter.Value);
            Assert.AreEqual(true, parameter.Enabled);

            parameter.Key = "Name";
            parameter.Value = "Frodo";
            parameter.Enabled = false;

            parameter = await _parameterBL.UpdateAsync(parameter);

            Assert.IsNotNull(parameter);
            Assert.AreEqual("Name", parameter.Key);
            Assert.AreEqual("Frodo", parameter.Value);
            Assert.AreEqual(false, parameter.Enabled);
        }

        [TestMethod]
        public async Task SoftDeleteToDoItemAsync_Success()
        {
            var parameter = ModelCreationHelper.GenerateParameter();

            parameter = await _parameterBL.CreateAsync(parameter);

            Assert.IsNotNull(parameter);

            var allParameters = await _parameterBL.GetAllAsync();

            Assert.AreEqual(4, allParameters.Count());

            var results = await _parameterBL.SoftDeleteAsync(parameter.Id);

            Assert.IsNotNull(results);
            Assert.IsTrue(results);

            allParameters = await _parameterBL.GetAllAsync();

            Assert.AreEqual(3, allParameters.Count());
        }

        [TestMethod]
        public async Task HardDeleteToDoItemAsync_Success()
        {
            var parameter = ModelCreationHelper.GenerateParameter();

            parameter = await _parameterBL.CreateAsync(parameter);

            Assert.IsNotNull(parameter);

            var results = await _parameterBL.HardDeleteAsync(parameter.Id);

            Assert.IsTrue(results);
        }

        [TestMethod]
        public async Task RestoreToDoItemAsync_Success()
        {
            var parameter = ModelCreationHelper.GenerateParameter();

            parameter = await _parameterBL.CreateAsync(parameter);

            Assert.IsNotNull(parameter);

            await _parameterBL.SoftDeleteAsync(parameter.Id);

            var allParameters = await _parameterBL.GetAllAsync();

            Assert.AreEqual(3, allParameters.Count());

            var results = await _parameterBL.RestoreAsync(parameter.Id);

            Assert.IsTrue(results);

            allParameters = await _parameterBL.GetAllAsync();

            Assert.AreEqual(4, allParameters.Count());
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            await DatabaseSeeder.Clear(_dbContextFactory);
        }
    }
}
