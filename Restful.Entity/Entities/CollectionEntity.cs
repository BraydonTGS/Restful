using Restful.Entity.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace Restful.Entity.Entities
{
    /// <summary>
    /// Entity Object for a User's Collection
    /// </summary>
    [Table("Collections")]
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class CollectionEntity : BaseEntity<Guid>
    {
        [Required]
        [Column("Title")]
        [MaxLength(150)]
        public string Title { get; set; } = string.Empty;

        public ICollection<RequestEntity>? Requests { get; set; }

        [ForeignKey(nameof(UserEntity))]
        public Guid UserId { get; set; }

        public UserEntity? UserEntity { get; set; }

        private string GetDebuggerDisplay() => $"Collection: {Title}";
    }
}
