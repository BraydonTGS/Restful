using Restful.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Restful.Entity.Entities
{
    public class ResponseEntity : BaseEntity<Guid>
    {
        [Column("Result")]
        public string Result { get; set; } = string.Empty;

        [ForeignKey(nameof(Request))]
        public Guid RequestId { get; set; }

        public RequestEntity? Request { get; set; }
    }
}
