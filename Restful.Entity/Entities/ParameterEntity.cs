using Restful.Entity.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace Restful.Entity.Entities
{
    /// <summary>
    /// Entity Object for a Request's Parameters
    /// </summary>
    [Table("Parameters")]
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class ParameterEntity : BaseEntity<Guid>
    {
        [Column("Enabled")]
        public bool Enabled { get; set; }

        [Required]
        [Column("Key")]
        [MaxLength(100)]
        public string Key { get; set; } = string.Empty;

        [Required]
        [Column("Value")]
        [MaxLength(100)]
        public string Value { get; set; } = string.Empty;

        [ForeignKey(nameof(Request))]
        public Guid RequestId { get; set; }

        public RequestEntity? Request { get; set; }

        private string GetDebuggerDisplay() => $"Key: {Key}, Value: {Value}";
    }
}
