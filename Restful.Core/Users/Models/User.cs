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
        private string GetDebuggerDisplay() => $"FullName: {FirstName} {LastName}";
    }
}
