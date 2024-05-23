using Restful.UserModule.Models;
using System;
using System.Threading.Tasks;

namespace Restful.UserModule.Services
{
    public class AccountService : IAccountService
    {
        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            if (request.Username?.ToLower() == "admin" && request.Password?.ToLower() == "password")
            {
                // Simulate Loading the User From the DB //
                await Task.Delay(200);
                return new LoginResponse()
                {
                    UserGuid = Guid.NewGuid(),
                    Username = "ADMIN",
                    Email = "RestfulAdmin@gmail.com",
                    IsSuccessful = true,
                    StatusCode = 200,
                };
            }
            return null;
        }
    }
}
