using CommunityToolkit.Mvvm.ComponentModel;
using Restful.Core.Interfaces;
using Restful.Core.Models;

namespace Restful.Core.Services
{
    public partial class ApplicationUserService : ObservableObject, IApplicationUserService
    {
        private ApplicationUser? _applicationUser { get; set; }
        public ApplicationUser? GetApplicationUser()
        {
            if (_applicationUser is not null)
                return _applicationUser;

            return null;
        }

        public Guid GetApplicationUserGuid()
        {
            if (_applicationUser is not null)
                return _applicationUser.Id;

            return Guid.Empty;
        }

        public void SetApplicationUser(Guid guid, string username, string email)
        {
            _applicationUser = new ApplicationUser(guid, username, email);
        }
    }
}
