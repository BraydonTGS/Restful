using Restful.Core.Models;

namespace Restful.Core.Interfaces
{
    public interface IApplicationUserService
    {
        public Guid GetApplicationUserGuid();
        string GetApplicationUsername();
        public ApplicationUser? GetApplicationUser();
        public void SetApplicationUser(Guid guid, string username, string email);
    }
}
