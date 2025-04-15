using UploadService.Domain.Common.Enums;

namespace UploadService.Domain.Common.Entities
{
    public class DocumentFile
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Url { get; set; } = null!;
        public DocumentType Type { get; set; }
        public Stream Content { get; set; } = null!;
        // или так
        //public byte[] Content { get; set; } = null!;
    }
}
