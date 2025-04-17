using Microsoft.Extensions.VectorData;

namespace UploadService.Application.Models
{
    public class DocumentItemChunk
    {
        [VectorStoreRecordKey]
        public required Guid Id { get; init; }
        [VectorStoreRecordData]
        public required string DocumentName { get; init; }
        [VectorStoreRecordData]
        public required string DocumentId { get; init; }
        [VectorStoreRecordData]
        public required string DocumentUrl { get; init; }
        [VectorStoreRecordData(IsFullTextSearchable = true)]
        public required string Content { get; init; }
        [VectorStoreRecordVector(768)]
        public ReadOnlyMemory<float> TextEmbedding { get; set; }
    }
}
