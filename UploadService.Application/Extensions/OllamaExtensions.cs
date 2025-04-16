#pragma warning disable SKEXP0070
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;

namespace UploadService.Application.Extensions
{
    public static class OllamaExtensions
    {
        public static void ConfigureOllama(this IServiceCollection services, IConfiguration configuration)
        {
            var modelName = configuration["ModelInfo:Name"]!;
            var hostUri = new Uri(configuration["ModelInfo:HostUri"]!);

            services.AddOllamaTextEmbeddingGeneration(modelName, hostUri);
        }
    }
}
