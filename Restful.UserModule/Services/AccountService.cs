using Restful.UserModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Restful.UserModule.Services
{
    public class AccountService : IAccountService
    {
        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            if(request.Username?.ToLower() == "test" && request.Password?.ToLower() == "password")
            {
                // Simulate Loading the User From the DB //
                await Task.Delay(200);
                return new LoginResponse()
                {
                    UserGuid = Guid.NewGuid(),
                    Username = "Test User", 
                    Email = "TestUser@gmail.com",
                    IsSuccessful = true,
                    StatusCode= 200,
                }; 
            }
            return null;
        }
    }
}
