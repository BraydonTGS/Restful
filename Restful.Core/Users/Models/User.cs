using Restful.Core.Base;
using System.Diagnostics;

namespace Restful.Core.Users.Models
{
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class User : ModelBase<Guid>
    {

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string TempPassword { get; set; } = string.Empty;

        public User() { }
        public User(
            string firstName,
            string lastName,
            string email,
            string username,
            string tempPassword)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Username = username;
            TempPassword = tempPassword;
        }

        private string GetDebuggerDisplay() => $"FullName: {FirstName} {LastName}";
    }
}
