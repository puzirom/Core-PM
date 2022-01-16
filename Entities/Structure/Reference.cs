using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Enums;

namespace Entities.Structure
{
    public class Reference
    {
        [Key]
        public int Id { get; set; }

        public ReferenceType Type { get; set; }

        [Required]
        [MaxLength(100)]
        public string Value { get; set; }

        public int DocumentId { get; set; } // foreign key

        [ForeignKey("DocumentId")]
        public Document Document { get; set; } // navigation property
    }
}
