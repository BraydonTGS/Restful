using Microsoft.EntityFrameworkCore;
using Prism.Ioc;
using Restful.Core.Context;
using Restful.Core.Users;
using Restful.Global.Exceptions;
using Restful.Tests.Shared.Base;
using Restful.Tests.Shared.Database;

namespace Restful.Core.Tests.UserTests
{
    [TestClass]
    public class UserBLTests : TestBase
    {

        private readonly IUserBL _userBL;
        private readonly IDbContextFactory<RestfulDbContext> _dbContextFactory;
        public UserBLTests()
        {
            _userBL = _containerProvider.Resolve<IUserBL>();
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
            Assert.IsNotNull(_userBL);
        }

        [TestMethod]
        public async Task GetAllUsersAsync_Success()
        {
            var results = await _userBL.GetAllAsync();

            Assert.IsNotNull(results);

            Assert.AreEqual(2, results.Count());
        }

        [TestMethod]
        public async Task GetUserByIdAsync_Success()
        {
            var results = await _userBL.CreateAsync(ModelCreationHelper.GenerateUser());

            Assert.IsNotNull(results);

            var user = await _userBL.GetByIdAsync(results.Id);

            Assert.IsNotNull(user);
            Assert.AreEqual("Braydon", user.FirstName);
            Assert.AreEqual("Sutherland", user.LastName);
            Assert.AreEqual("BraydonTGS@gmail.com", user.Email);
            Assert.AreEqual(false, user.IsDeleted);
        }

        [TestMethod]
        public async Task CreateUserAsync_Success()
        {
            var user = await _userBL.CreateAsync(ModelCreationHelper.GenerateUser());

            Assert.IsNotNull(user);
            Assert.AreEqual("Braydon", user.FirstName);
            Assert.AreEqual("Sutherland", user.LastName);
            Assert.AreEqual("BraydonTGS@gmail.com", user.Email);
            Assert.AreEqual(false, user.IsDeleted);
        }

        [TestMethod]
        public async Task CreateUserAsync_UserAlreadyHasARegisteredEmail_Throws_EmailAlreadyRegisteredException_Success()
        {
            EmailAlreadyRegisteredException? emailException = null;

            var user = await _userBL.CreateAsync(ModelCreationHelper.GenerateUser());
            Assert.IsNotNull(user);

            try
            {
                _ = await _userBL.CreateAsync(user);
            }
            catch (EmailAlreadyRegisteredException ex)
            {
                emailException = ex;
            }

            Assert.IsNotNull(emailException);
            Assert.IsNotNull(emailException.Message);

            Assert.IsNotNull("The Specified Email is already Registered", emailException.Message);
        }

        [TestMethod]
        public async Task UpdateUserAsync_Success()
        {
            var results = await _userBL.CreateAsync(ModelCreationHelper.GenerateUser());

            Assert.IsNotNull(results);

            results.FirstName = "Braydon";
            results.LastName = "Sutherland";
            results.Username = "Geo-Matics";
            results.Email = "BraydonTGSuds@gmail.com";

            results = await _userBL.UpdateAsync(results);

            Assert.IsNotNull(results);
            Assert.AreEqual("Braydon", results.FirstName);
            Assert.AreEqual("Sutherland", results.LastName);
            Assert.AreEqual("BraydonTGSuds@gmail.com", results.Email);
            Assert.AreEqual(false, results.IsDeleted);
        }

        [TestMethod]
        public async Task SoftDeleteUserAsync_Success()
        {
            var user = await _userBL.CreateAsync(ModelCreationHelper.GenerateUser());

            Assert.IsNotNull(user);

            var results = await _userBL.SoftDeleteAsync(user.Id);

            Assert.IsNotNull(results);
            Assert.IsTrue(results);

            var allUsers = await _userBL.GetAllAsync();

            Assert.IsNotNull(allUsers);
            Assert.AreEqual(2, allUsers.Count());
        }

        [TestMethod]
        public async Task HardDeleteUserAsync_Success()
        {
            var user = await _userBL.CreateAsync(ModelCreationHelper.GenerateUser());

            Assert.IsNotNull(user);

            var results = await _userBL.HardDeleteAsync(user.Id);

            Assert.IsNotNull(results);
            Assert.IsTrue(results);
        }

        [TestMethod]
        public async Task RestoreUserAsync_Success()
        {
            var user = await _userBL.CreateAsync(ModelCreationHelper.GenerateUser());

            Assert.IsNotNull(user);

            await _userBL.SoftDeleteAsync(user.Id);

            var results = await _userBL.RestoreAsync(user.Id);

            Assert.IsNotNull(results);
            Assert.IsTrue(results);

            var allUsers = await _userBL.GetAllAsync();

            Assert.IsNotNull(allUsers);
            Assert.AreEqual(3, allUsers.Count());
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            await DatabaseSeeder.Clear(_dbContextFactory);
        }
    }
}
