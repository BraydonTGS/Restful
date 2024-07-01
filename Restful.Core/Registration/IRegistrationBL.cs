using Restful.Core.Users.Models;

namespace Restful.Core.Registration
{
    public interface IRegistrationBL
    {
        Task<User?> RegisterNewUserAsync(User user);
    }
}