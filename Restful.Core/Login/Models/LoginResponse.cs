using Restful.Core.Users.Models;

namespace Restful.Core.Login.Models
{
    public class LoginResponse
    {
        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public User? User { get; set; }

        public LoginResponse() { }
        public LoginResponse(User user, bool isSuccessful, string errorMessage = "")
        {
            User = user;
            IsSuccessful = isSuccessful;
            ErrorMessage = errorMessage;
        }
    }
}
