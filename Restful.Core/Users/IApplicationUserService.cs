using Restful.Core.Users.Models;

namespace Restful.Core.Users
{
    public interface IApplicationUserService
    {
        public Guid GetApplicationUserGuid();
        string GetApplicationUsername();
        public ApplicationUser? GetApplicationUser();
        public void SetApplicationUser(Guid guid, string username, string email);
    }
}
