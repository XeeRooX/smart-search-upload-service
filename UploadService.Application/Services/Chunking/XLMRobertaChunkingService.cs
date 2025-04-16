using Lokad.Tokenizers.Tokenizer;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using UploadService.Domain.Services;

namespace UploadService.Application.Services.Chunking
{
    public class XLMRobertaChunkingService : IChunkingService
    {
        private readonly string modelPath; 
        private readonly int tokensInChunk;
        private readonly int overlapChunkPercent;
        private readonly IConfiguration _configuration;

        public XLMRobertaChunkingService(IConfiguration configuration)
        {
            _configuration = configuration;
            modelPath = configuration["ModelInfo:TokenizerPath"]!;
            tokensInChunk = int.Parse(configuration["ModelInfo:ChunkSize"]!);
            overlapChunkPercent = int.Parse(configuration["ModelInfo:OverlapSizePercent"]!);
        }

        public IEnumerable<string> ChunkText(string text)
        {
            var dir = Directory.GetCurrentDirectory();
            // C:\projects\smart-search\console-playground\UploadService\UploadService.Application.Test\bin\Debug\net8.0
            var tokenizer = new XLMRobertaTokenizer(modelPath, false);
            var tokens = tokenizer.Tokenize(text);

            var chunks = new List<string>();
            int currentTokenIndex = 0;

            while (currentTokenIndex < tokens.Count)
            {
                var chunkTokens = tokens.Skip(currentTokenIndex).Take(tokensInChunk).ToList();
                var chunk = tokenizer.ConvertTokensToString(chunkTokens); // воо
                chunks.Add(chunk);
                currentTokenIndex += tokensInChunk;
            }

            return chunks;
        }

        public IEnumerable<string> ChunkWithOverlap(string text)
        {
            var overlapTokens = (int)(tokensInChunk * (overlapChunkPercent / 100d));
            var tokenizer = new XLMRobertaTokenizer(modelPath, false);
            var tokens = tokenizer.Tokenize(text);

            var chunks = new List<string>();
            int currentTokenIndex = 0;


            while (currentTokenIndex - overlapTokens < tokens.Count)
            {
                var chunkTokens = new List<string>();
                if (currentTokenIndex - overlapTokens < 0)
                {
                    chunkTokens = tokens.Skip(currentTokenIndex).Take(tokensInChunk).ToList();
                    currentTokenIndex += tokensInChunk;
                }
                else
                {
                    chunkTokens = tokens.Skip(currentTokenIndex - overlapTokens).Take(tokensInChunk).ToList();
                    currentTokenIndex += tokensInChunk - overlapTokens;
                }

                var chunk = tokenizer.ConvertTokensToString(chunkTokens);
                chunks.Add(chunk);
            }

            return chunks;
        }
    }
}
