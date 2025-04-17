using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploadService.Data.Models
{
    public class ChunkInfo
    {
        public Guid Id { get; set; }

        public UploadStatus UploadStatus { get; set; } = null!;
        public int UploadStatusId { get; set; }
        public DocumentInfo DocumentInfo { get; set; } = null!;
        public int DocumentInfoId { get; set; }
    }
}
