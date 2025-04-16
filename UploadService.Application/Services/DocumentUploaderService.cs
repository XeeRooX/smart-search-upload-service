#pragma warning disable SKEXP0001
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel.Embeddings;
using UploadService.Application.Models;
using UploadService.Domain.Common.Entities;
using UploadService.Domain.Services;

namespace UploadService.Application.Services
{
    public class DocumentUploaderService(IChunkingService chunkingService, IDocumentReaderService documentReader,
        IVectorStore vectorStore, ITextEmbeddingGenerationService textEmbeddingGenerationService, IConfiguration configuration,
        IDocumetLoaderService documetLoaderService) : IDocumentUploaderService
    {
        public async Task UploadAsync(DocumentInfo documentInfo, DocumentListInfo listInfo)
        {
            var bytesContent = await documetLoaderService.LoadByUrlAsync(documentInfo.Url);
            if (bytesContent == null)
                return;

            var stringContent = documentReader.Read(bytesContent, documentInfo.Type);
            if (stringContent == null)
                return;

            var chunks = chunkingService.ChunkWithOverlap(stringContent);

            foreach (var chunk in chunks)
            {
                await GenerateEmbeddingsAndUpload(documentInfo, listInfo, chunk);
            }
        }

        async Task<DocumentItemChunk> GenerateEmbeddingsAndUpload(DocumentInfo documentInfo, DocumentListInfo listInfo, string chunk)
        {
            var documentListPrefix = configuration["VectorStorage:SharePoint:ListNaming:DocumentListPrefix"]!;

            var collectionName = documentListPrefix + listInfo.Id.ToString();
            var collection = vectorStore.GetCollection<Guid, DocumentItemChunk>(collectionName);

            await collection.CreateCollectionIfNotExistsAsync();
            var embedding = await textEmbeddingGenerationService.GenerateEmbeddingAsync(chunk);

            var item = new DocumentItemChunk()
            {
                Id = documentInfo.Id,
                Content = chunk,
                DocumentName = documentInfo.Name,
                DocumentUrl = documentInfo.Url,
                TextEmbedding = embedding
            };

            await collection.UpsertAsync(item);

            return item;
        }
    }
}
