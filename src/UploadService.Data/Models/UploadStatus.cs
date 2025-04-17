using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploadService.Data.Models
{
    public class UploadStatus
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Desription { get; set; }

        public List<ChunkInfo> ChunkInfos { get; set; } = new();
    }
}
