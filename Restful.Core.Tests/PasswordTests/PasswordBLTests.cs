using Microsoft.EntityFrameworkCore;
using Prism.Ioc;
using Restful.Core.Context;
using Restful.Core.Passwords;
using Restful.Global.Enums;
using Restful.Global.Exceptions;
using Restful.Tests.Shared.Base;
using Restful.Tests.Shared.Database;

namespace Restful.Core.Tests.PasswordTests
{
    [TestClass]
    public class PasswordBLTests : TestBase
    {
        private readonly IPasswordBL _passwordBL;
        private readonly IDbContextFactory<RestfulDbContext> _dbContextFactory;
        public PasswordBLTests()
        {
            _passwordBL = _containerProvider.Resolve<IPasswordBL>();
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
            Assert.IsNotNull(_passwordBL);
        }

        [TestMethod]
        public async Task CreatePasswordForUserAsync_Success()
        {
            var password = await _passwordBL.CreatePasswordForUserAsync(DatabaseSeeder.GetSecondUserId(), "IAmGroot");

            Assert.IsNotNull(password);

            Assert.AreEqual(DatabaseSeeder.GetSecondUserId(), password.UserId);

            Assert.IsNotNull(password.Salt);
            Assert.IsNotNull(password.Hash);

            Assert.IsInstanceOfType(password.Hash, typeof(byte[]));
            Assert.IsInstanceOfType(password.Salt, typeof(byte[]));
        }

        [TestMethod]
        public async Task CreatePasswordForUserAsync_UserAlreadyHasPassword_Throws_PasswordAlreadyExistsException_Success()
        {
            PasswordAlreadyExistsException? passwordException = null;
            try
            {
                _ = await _passwordBL.CreatePasswordForUserAsync(DatabaseSeeder.GetUserId(), "IAmGroot");
            }
            catch (PasswordAlreadyExistsException ex)
            {
                passwordException = ex;
            }
            Assert.IsNotNull(passwordException);
            Assert.IsNotNull(passwordException.Message);

            Assert.IsNotNull("A password for this user already exists.", passwordException.Message);
        }

        [TestMethod]
        public async Task VerifyUserPasswordAsync_Success()
        {
            var results = await _passwordBL.VerifyUserPasswordAsync(DatabaseSeeder.GetUserId(), "YodaIsMyMentor");

            Assert.IsNotNull(results);
            Assert.AreEqual(PasswordVerificationResults.Success, results);
        }

        [TestMethod]
        public async Task VerifyUserPasswordAsync_WrongPasswordProducesFailure_Success()
        {
            var results = await _passwordBL.VerifyUserPasswordAsync(DatabaseSeeder.GetUserId(), "YodaIsMySon");

            Assert.IsNotNull(results);
            Assert.AreEqual(PasswordVerificationResults.Failed, results);
        }


        [TestCleanup]
        public async Task TestCleanup()
        {
            await DatabaseSeeder.Clear(_dbContextFactory);
        }
    }
}
