using Prism.Ioc;
using Restful.Core.Passwords;
using Restful.Core.Passwords.Model;
using Restful.Global.Enums;
using Restful.Tests.Shared.Base;

namespace Restful.Core.Tests.PasswordTests
{
    [TestClass]
    public class PasswordHasherTests : TestBase
    {

        private readonly IPasswordHasher<Password> _passwordHasher;
        public PasswordHasherTests()
        {
            _passwordHasher = _containerProvider.Resolve<IPasswordHasher<Password>>();
        }

        [TestMethod]
        public void ConstructorNotNull_Success()
        {
            Assert.IsNotNull(_passwordHasher);
        }

        [TestMethod]
        [DataRow("PASSWORD")]
        [DataRow("passWord")]
        [DataRow("PaSsWord")]
        [DataRow("12345678z")]
        [DataRow("ABCDEFG")]
        [DataRow("CatsAndDogs")]
        [DataRow("TestingIsFun")]
        [DataRow("StarWars")]
        [DataRow("YoloBaggins")]
        [DataRow("WhatsUp")]
        public void HashPassword_Success(string input)
        {
            var (salt, hash) = _passwordHasher.HashPassword(input);

            Assert.IsNotNull(salt);
            Assert.IsNotNull(hash);
            Assert.AreEqual(64, salt.Length);
            Assert.AreEqual(16, hash.Length);
        }

        [TestMethod]
        [DataRow("PASSWORD")]
        [DataRow("passWord")]
        [DataRow("PaSsWord")]
        [DataRow("12345678z")]
        [DataRow("ABCDEFG")]
        [DataRow("CatsAndDogs")]
        [DataRow("TestingIsFun")]
        [DataRow("StarWars")]
        [DataRow("YoloBaggins")]
        [DataRow("WhatsUp")]
        public void VerifyGivenPassword_Success(string input)
        {
            var (hash, salt) = _passwordHasher.HashPassword(input);

            var password = new Password(salt, hash);

            var isValid = _passwordHasher.VerifyHashedPassword(password, input);

            Assert.AreEqual(PasswordVerificationResults.Success, isValid);
        }

        [TestMethod]
        [DataRow("PASSWORD")]
        [DataRow("passWord")]
        [DataRow("PaSsWord")]
        [DataRow("12345678z")]
        [DataRow("ABCDEFG")]
        [DataRow("CatsAndDogs")]
        [DataRow("TestingIsFun")]
        [DataRow("StarWars")]
        [DataRow("YoloBaggins")]
        [DataRow("WhatsUp")]
        public void VerifyGivenPassword_IsNotCorrect_Success(string input)
        {
            var invalidPassword = "Invalid";

            var (hash, salt) = _passwordHasher.HashPassword(input);
            var password = new Password(salt, hash);

            var isValid = _passwordHasher.VerifyHashedPassword(password, invalidPassword);

            Assert.AreEqual(PasswordVerificationResults.Failed, isValid);
        }
    }
}
