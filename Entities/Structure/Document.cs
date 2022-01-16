using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Entities.Enums;

namespace Entities.Structure
{
    public class Document
    {
        [Key]
        public int Id { get; set; }

        public DocumentType Type { get; set; }

        public int SourceSystemId { get; set; }

        public int RecNo { get; set; }

        [MaxLength(20)]
        public string OrgNoClient { get; set; }

        [MaxLength(50)]
        public string Kid { get; set; }

        public DateTime Inserted { get; set; }

        public DateTime Updated { get; set; }

        public List<Reference> References { set; get; }
    }
}
