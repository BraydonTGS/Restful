using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using Prism.Ioc;
using Restful.Core.Context;
using Restful.Core.Login;
using Restful.Core.Registration;
using Restful.Global.Exceptions;
using Restful.Tests.Shared.Base;
using Restful.Tests.Shared.Database;

namespace Restful.Core.Tests.LoginBLTests
{
    [TestClass]
    public class LoginBLTests : TestBase
    {
        private readonly ILoginBL _loginBL;
        private readonly IRegistrationBL _registrationBL;  
        private readonly IDbContextFactory<RestfulDbContext> _dbContextFactory;
        public LoginBLTests()
        {
            _loginBL = _containerProvider.Resolve<ILoginBL>();
            _registrationBL = _containerProvider.Resolve<IRegistrationBL>();
            _dbContextFactory = _containerProvider.Resolve<IDbContextFactory<RestfulDbContext>>();
        }

        [TestInitialize]
        public async Task TestInitialize()
        {
            await DatabaseSeeder.Seed(_dbContextFactory);
        }

        [TestMethod]
        public async Task LoginCurrentUserAsync_Success()
        {
            var registrationRequest = ModelCreationHelper.GenerateRegistrationRequest();

            var registrationResponse = await _registrationBL.RegisterNewUserAsync(registrationRequest);

            Assert.IsNotNull(registrationResponse);
            Assert.IsTrue(registrationResponse.IsSuccessful);
            Assert.IsNotNull(registrationResponse.User);

            registrationResponse.User.TempPassword = "MonkeyDBanana";

            var loginResponse = await _loginBL.LoginUserAsync(ModelCreationHelper.GenerateLoginRequest(registrationResponse.User.Username, registrationResponse.User.TempPassword));

            Assert.IsNotNull(loginResponse);
            Assert.IsNotNull(loginResponse.User);
            Assert.AreEqual(string.Empty, loginResponse.User.TempPassword);
        }

        [TestMethod]
        public async Task LoginCurrentUserAsync_InvalidPassword_Throws_InvalidPasswordException_Success()
        {
            InvalidPasswordException? passwordException = null;
            try
            {
                var registrationRequest = ModelCreationHelper.GenerateRegistrationRequest();

                var registrationResponse = await _registrationBL.RegisterNewUserAsync(registrationRequest);

                Assert.IsNotNull(registrationResponse);
                Assert.IsTrue(registrationResponse.IsSuccessful);
                Assert.IsNotNull(registrationResponse.User);

                registrationResponse.User.TempPassword = "MonkeyDBanana";

                var loginRequestDto = ModelCreationHelper.GenerateLoginRequest(registrationResponse.User.Username, "BananaDMonkey");

                var response = await _loginBL.LoginUserAsync(loginRequestDto);
            }
            catch (InvalidPasswordException ex)
            {
                passwordException = ex;
            }
            Assert.IsNotNull(passwordException);
            Assert.IsNotNull(passwordException.Message);

            Assert.IsNotNull("Password Verification Failure for the Specified User.", passwordException.Message);
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            await DatabaseSeeder.Clear(_dbContextFactory);
        }
    }
}
