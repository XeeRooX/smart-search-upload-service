using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploadService.Data.Models
{
    public class DocumentInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime LastUploadDate { get; set; }

        public List<ChunkInfo> ChunkInfos { get; set; } = new();
        public int ListInfoId { get; set; }
        public ListInfo ListInfo { get; set; } = null!;
    }
}
