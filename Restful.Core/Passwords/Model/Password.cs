using Microsoft.VisualBasic.ApplicationServices;
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

        private string GetDebuggerDisplay()
        {
            return $"Salt: {Salt} & Hash: {Hash}";
        }
    }
}
