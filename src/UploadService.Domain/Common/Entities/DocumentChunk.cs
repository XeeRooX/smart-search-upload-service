using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadService.Domain.Common.Enums;

namespace UploadService.Domain.Common.Entities
{
    public class DocumentChunk
    {
        public Guid DocumentId { get; set; }
        public string DocumentName { get; set; } = null!;
        public string DocumentUrl { get; set; } = null!;
        public DocumentType DocumentType { get; set; }
        public string Text { get; set; } = null!;
    }
}
