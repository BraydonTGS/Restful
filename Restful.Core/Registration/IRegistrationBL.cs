using Restful.Core.Registration.Models;
using Restful.Core.Users.Models;

namespace Restful.Core.Registration
{
    public interface IRegistrationBL
    {
        Task<RegistrationResponse> RegisterNewUserAsync(RegistrationRequest registrationRequest);
    }
}