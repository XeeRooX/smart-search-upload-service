using UploadService.Domain.Common.Entities;

namespace UploadService.Domain.Services
{
    public interface IChunkingService
    {
        IEnumerable<string> ChunkText(string text);
        IEnumerable<DocumentChunk> ChunkFile(DocumentFile document);
    }
}
