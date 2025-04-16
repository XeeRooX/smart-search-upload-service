using UploadService.Domain.Common.Enums;

namespace UploadService.Domain.Common.Entities
{
    public class DocumentInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Url { get; set; } = null!;
        public DocumentType Type { get; set; }  
    }
}
