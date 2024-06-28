using Restful.Entity.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace Restful.Entity.Entities
{
    /// <summary>
    /// Entity Object for the User's Password
    /// </summary>
    [Table("Passwords")]
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class PasswordEntity : BaseEntity<Guid>
    {
        [Required]
        [Column("Hash")]
        [MaxLength(100)]
        public byte[] Hash { get; set; } = Array.Empty<byte>();

        [Required]
        [Column("Salt")]
        public byte[] Salt { get; set; } = Array.Empty<byte>();

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public UserEntity? User { get; set; }

        public PasswordEntity() { }

        public PasswordEntity(Guid userId, byte[] hash, byte[] salt)
        {
            UserId = userId;
            Hash = hash;
            Salt = salt;
        }

        private string GetDebuggerDisplay() => $"Salt: {Salt} & Hash: {Hash}";
    }
}
