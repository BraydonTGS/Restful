using CommunityToolkit.Mvvm.ComponentModel;
using Restful.Core.Base;
using Restful.Entity.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;
using Restful.Core.Requests.Models;
using System.Diagnostics;

namespace Restful.Core.Users.Models
{
    /// <summary>
    /// Application's Current User
    /// </summary>
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public partial class ApplicationUser : ModelBase<Guid>
    {
        [ObservableProperty]
        private string _username = string.Empty;

        [ObservableProperty]
        private string _email = string.Empty;

        public ObservableCollection<Request>? Requests { get; set; }

        public ApplicationUser(Guid guid, string username, string email)
        {
            Id = guid;
            Username = username;
            Email = email;
        }

        private string GetDebuggerDisplay() => $"Username: {Username}";
    }
}
