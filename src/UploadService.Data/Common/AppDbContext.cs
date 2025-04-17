using Microsoft.EntityFrameworkCore;
using UploadService.Data.Models;

namespace UploadService.Data.Common
{
    public class AppDbContext : DbContext
    {
        public DbSet<ChunkInfo> ChunkInfos { get; set; }
        public DbSet<DocumentInfo> DocumentInfos { get; set; }
        public DbSet<ListInfo> ListInfos { get; set; }
        public DbSet<UploadStatus> UploadStatuses { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {  
        }
    }
}
