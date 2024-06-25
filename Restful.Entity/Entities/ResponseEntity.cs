using Restful.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Restful.Entity.Entities
{
    /// <summary>
    /// Entity object for the Request's Response
    /// </summary>
    [Table("Results")]
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class ResponseEntity : BaseEntity<Guid>
    {
        [Column("Result")]
        public string? Result { get; set; } = string.Empty;

        [ForeignKey(nameof(Request))]
        public Guid RequestId { get; set; }

        public RequestEntity? Request { get; set; }

        private string GetDebuggerDisplay() => $"ResultId: {Id}"; 
    }
}