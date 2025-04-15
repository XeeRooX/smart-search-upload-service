using UploadService.Domain.Common.Entities;

namespace UploadService.Domain.Services
{
    public interface IReaderDocumentService
    {
        string Read(DocumentFile document);
    }
}
