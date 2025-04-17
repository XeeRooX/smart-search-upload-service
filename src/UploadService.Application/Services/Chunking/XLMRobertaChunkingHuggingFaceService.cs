using b2xtranslator.OfficeGraph;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tokenizers.DotNet;
using UploadService.Domain.Services;

namespace UploadService.Application.Services.Chunking
{
    public class XLMRobertaChunkingHuggingFaceService : IChunkingService
    {
        private readonly string tokenizerPath;
        private readonly int tokensInChunk;
        private readonly int overlapChunkPercent;
        private readonly IConfiguration _configuration;

        public XLMRobertaChunkingHuggingFaceService(IConfiguration configuration)
        {
            _configuration = configuration;
            tokenizerPath = configuration["ModelInfo:TokenizerPath"]!;
            tokensInChunk = int.Parse(configuration["ModelInfo:ChunkSize"]!);
            overlapChunkPercent = int.Parse(configuration["ModelInfo:OverlapSizePercent"]!);
        }

        public IEnumerable<string> ChunkText(string text)
        {
            var tokenizer = new Tokenizer(tokenizerPath);
            var tokens = tokenizer.Encode(text);

            var chunks = new List<string>();
            int currentTokenIndex = 0;

            while (currentTokenIndex < tokens.Count())
            {
                var chunkTokens = tokens.Skip(currentTokenIndex).Take(tokensInChunk);
                var chunk = tokenizer.Decode(chunkTokens.ToArray());
                chunks.Add(chunk);
                currentTokenIndex += tokensInChunk;
            }

            return chunks;
        }

        public IEnumerable<string> ChunkWithOverlap(string text)
        {
            var f = Directory.GetCurrentDirectory();

            var overlapTokens = (int)(tokensInChunk * (overlapChunkPercent / 100d));
            var tokenizer = new Tokenizer(tokenizerPath);
            var tokens = tokenizer.Encode(text);

            var chunks = new List<string>();
            int currentTokenIndex = 0;


            while (currentTokenIndex - overlapTokens < tokens.Count())
            {
                var chunkTokens = new List<uint>();
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

                var chunk = tokenizer.Decode(chunkTokens.ToArray());
                chunks.Add(chunk);
            }

            return chunks;
        }
    }
}
