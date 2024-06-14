using Microsoft.EntityFrameworkCore;
using Restful.Core.Context;
using Restful.Core.Login.Models;

namespace Restful.Core.Login
{
    public class LoginService : ILoginService
    {
        private readonly IDbContextFactory<RestfulDbContext> _contextFactory;
        public LoginService(IDbContextFactory<RestfulDbContext> contextFactory)
        {
           _contextFactory = contextFactory;
        }
        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
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
