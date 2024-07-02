using Microsoft.EntityFrameworkCore;
using Prism.Ioc;
using Restful.Core.Context;
using Restful.Core.Login;
using Restful.Global.Exceptions;
using Restful.Tests.Shared.Base;
using Restful.Tests.Shared.Database;

namespace Restful.Core.Tests.LoginBLTests
{
    [TestClass]
    public class LoginBLTests : TestBase
    {

        private readonly ILoginBL _requestBL;
        private readonly IDbContextFactory<RestfulDbContext> _dbContextFactory;
        public LoginBLTests()
        {
            _requestBL = _containerProvider.Resolve<ILoginBL>();
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
            //var newUser = ModelCreationHelper.GenerateUser();

            //newUser = await _registrationBL.RegisterNewUserAsync(newUser);

            //Assert.IsNotNull(newUser);

            //newUser.TempPassword = "MonkeyDBanana";

            //var response = await _loginBL.LoginUserAsync(ModelCreationHelper.GenerateLoginRequestDto());

            //Assert.IsNotNull(response);
            //Assert.IsNotNull(response.User);
            //Assert.AreEqual(string.Empty, response.User.TempPassword);
        }

        [TestMethod]
        public async Task LoginCurrentUserAsync_InvalidPassword_Throws_InvalidPasswordException_Success()
        {
            InvalidPasswordException? passwordException = null;
            try
            {
                //var newUser = ModelCreationHelper.GenerateUserDto();

                //newUser = await _registrationBL.RegisterNewUserAsync(newUser);

                //Assert.IsNotNull(newUser);

                //var loginRequestDto = ModelCreationHelper.GenerateLoginRequestDto();
                //loginRequestDto.TempPassword = "BananaDMonkey";

                //var response = await _loginBL.LoginUserAsync(loginRequestDto);
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
