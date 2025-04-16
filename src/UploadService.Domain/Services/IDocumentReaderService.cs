using UploadService.Domain.Common.Entities;
using UploadService.Domain.Common.Enums;

namespace UploadService.Domain.Services
{
    public interface IDocumentReaderService
    {
        string? Read(byte[] documetContent, DocumentType documentType);
    }
}
