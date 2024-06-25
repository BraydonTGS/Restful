using Restful.Entity.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using HttpMethod = Restful.Global.Enums.HttpMethod;

namespace Restful.Entity.Entities
{
    /// <summary>
    /// Entity Object for the Collection's Request
    /// </summary>
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class RequestEntity : BaseEntity<Guid>
    {
        [Required]
        [Column("Name")]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Column("Url")]
        public string? Url { get; set; }

        [Column("Body")]
        public string Body { get; set; } = string.Empty;

        [Column("TempResult")]
        public string TempResult { get; set; } = string.Empty;

        [Required]
        [Column("HttpMethod")]
        public HttpMethod HttpMethod { get; set; }

        [ForeignKey(nameof(CollectionEntity))]
        public Guid? CollectionId { get; set; }

        [ForeignKey(nameof(UserEntity))]
        public Guid? UserId { get; set; }

        public CollectionEntity? CollectionEntity { get; set; }

        public UserEntity? UserEntity { get; set; }

        public ICollection<HeaderEntity>? HeaderEntities { get; set; }

        public ICollection<ParameterEntity>? ParameterEntities { get; set; }

        private string GetDebuggerDisplay() => $"Request: {Name}";

    }
}
