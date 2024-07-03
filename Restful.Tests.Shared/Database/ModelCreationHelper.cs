using Restful.Core.Login.Models;
using Restful.Core.Registration.Models;
using Restful.Core.Requests.Models;
using Restful.Core.Users.Models;

namespace Restful.Tests.Shared.Database
{
    public static class ModelCreationHelper
    {

        #region GenerateRequest
        public static Request GenerateRequest()
        {
            var request = new Request(true)
            {
                Name = "GetCharacterById",
                Description = "Get a specific character by id",
                HttpMethod = Global.Enums.HttpMethod.GET,
                Url = "https://the-one-api.dev/v2/characters/1",
                CollectionId = DatabaseSeeder.GetCollectionId()
            };

            return request;
        }
        #endregion

        #region GenerateUser
        public static User GenerateUser()
        {
            var user = new User()
            {
                FirstName = "Braydon",
                LastName = "Sutherland",
                Email = "BraydonTGS@gmail.com",
                TempPassword = "MonkeyDBanana",
                Username = "Geomatics",
                Description = "Application Owner"
            };
            return user;
        }
        #endregion

        #region GenerateUserAlreadyInDb
        public static User GenerateUserAlreadyInDb()
        {
            var user = new User()
            {
                FirstName = "Daniel",
                LastName = "Aguirre",
                Email = "RedRain@gmail.com",
                TempPassword = "YodaIsMyMentor",
                Username = "RedxRain",
                Description = "JarJar of Unit Tests",
            };
            return user;
        }
        #endregion

        #region GenerateLoginRequest
        public static LoginRequest GenerateLoginRequest(string username, string password)
        {
            var loginRequest = new LoginRequest()
            {
                Password = password,
                Username = username
            };
            return loginRequest;
        }
        #endregion

        #region GenerateLoginRequestUserAlreadyInDb
        public static LoginRequest GenerateLoginRequestUserAlreadyInDb()
        {
            var loginRequest = new LoginRequest()
            {
                Username = "RedxRain",
                Password = "YodaIsMyMentor"
            };
            return loginRequest;
        }
        #endregion

        public static RegistrationRequest GenerateRegistrationRequest()
        {
            return new RegistrationRequest()
            {
                FirstName = "Braydon",
                LastName = "Sutherland",
                Email = "BraydonTGS@gmail.com",
                Password = "MonkeyDBanana",
                ConfirmPassword = "MonkeyDBanana",
                Username = "Geomatics"
            };
        }
    }
}
