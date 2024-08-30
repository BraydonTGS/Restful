using Restful.Core.Base;
using Restful.Core.Users.Models;
using System.Diagnostics;

namespace Restful.Core.Passwords.Model
{
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class Password : ModelBase<Guid>
    {
        public byte[] Hash { get; set; } = [];

        public byte[] Salt { get; set; } = [];

        public Guid UserId { get; set; }

        public ApplicationUser? ApplicationUser { get; set; }

        public Password() { }

        public Password(byte[] salt, byte[] hash)
        {
            Hash = hash;
            Salt = salt;    
        }

        private string GetDebuggerDisplay()
        {
            return $"Salt: {Salt} & Hash: {Hash}";
        }
    }
}
