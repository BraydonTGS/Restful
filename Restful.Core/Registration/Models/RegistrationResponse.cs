using Restful.Core.Users.Models;

namespace Restful.Core.Registration.Models
{
    public class RegistrationResponse
    {
        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public User? User { get; set; }

        public RegistrationResponse() { }

        public RegistrationResponse(bool isSuccessful, string errorMessage = "")
        {
            IsSuccessful = isSuccessful;
            ErrorMessage = errorMessage;
        }
        public RegistrationResponse(User user, bool isSuccessful, string errorMessage = "")
        {
            User = user;
            IsSuccessful = isSuccessful;
            ErrorMessage = errorMessage;
        }
    }
}
