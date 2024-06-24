using Microsoft.EntityFrameworkCore;
using Restful.Core.Context;
using Restful.Core.Login.Models;

namespace Restful.Core.Login
{
    public class LoginBL : ILoginBL
    {
        private readonly IDbContextFactory<RestfulDbContext> _contextFactory;
        public LoginBL(IDbContextFactory<RestfulDbContext> contextFactory)
        {
           _contextFactory = contextFactory;
        }
        public async Task<LoginResponse?> LoginUserAsync(LoginRequest request)
        {
            if (request.Username?.ToLower().Trim() == "admin" && request.Password?.ToLower().Trim() == "password")
            {
                // Simulate Loading the User From the DB //
                await Task.Delay(200);
                return new LoginResponse()
                {
                    User = new Users.Models.User()
                    {
                        Id = Guid.NewGuid(),
                        Username = "ADMIN",
                        Email = "RestfulAdmin@gmail.com",
                    },
                    IsSuccessful = true,
                };

                // Login the User //
            }
            return null;
        }
    }
}
