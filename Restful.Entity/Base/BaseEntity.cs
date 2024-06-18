using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Restful.Entity.Base
{
    /// <summary>
    /// Base Entity Class for all Entities
    /// </summary>
    public abstract class BaseEntity<TKey> : IEntity
    {
        [Key]
        [NotNull]
        [Required]
        [Column("Id")]
        public TKey? Id { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsDirty { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public string Description { get; set; } = string.Empty;
        public bool IsEntityRegistered { get; set; } = true;
    }
}
