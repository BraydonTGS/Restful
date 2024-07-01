using Restful.Entity.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace Restful.Entity.Entities
{
    /// <summary>
    /// Entity Object for the Application User
    /// </summary>
    [Table("Users")]
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class UserEntity : BaseEntity<Guid>
    {
        [Required]
        [Column("FirstName")]
        [MinLength(1)]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [Column("LastName")]
        [MinLength(1)]
        [MaxLength(150)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [Column("Email")]
        [MinLength(6)]
        [MaxLength(250)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Column("UserName")]
        [MinLength(8)]
        [MaxLength(50)]
        public string UserName { get; set; } = string.Empty;

        public ICollection<CollectionEntity>? CollectionEntities { get; set; }

        public ICollection<RequestEntity>? RequestEntities { get; set; }

        private string GetDebuggerDisplay()
        {
            return $"FirstName: {FirstName}, LastName: {LastName}";
        }
    }
}
