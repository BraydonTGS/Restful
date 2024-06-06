using CommunityToolkit.Mvvm.ComponentModel;
using Restful.Core.Base;

namespace Restful.Core.Users.Models
{
    /// <summary>
    /// Application's Current User
    /// </summary>
    public partial class ApplicationUser : ModelBase<Guid>
    {
        [ObservableProperty]
        private string _username = string.Empty;

        [ObservableProperty]
        private string _email = string.Empty;

        public ApplicationUser(Guid guid, string username, string email)
        {
            Id = guid;
            Username = username;
            Email = email;
        }
    }
}
