using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;

namespace UploadService.Application.Extensions
{
    public static class QdrantExtensions
    {
        public static void ConfigureQdrant(this IServiceCollection services, IConfiguration configuration)
        {
            var hostname = configuration["VectorStorage:Qdrant:Hostname"]!;
            var port = configuration["VectorStorage:Qdrant:Port"] == null ? 6334 : int.Parse(configuration["VectorStorage:Qdrant:Port"]!);
            var key = configuration["VectorStorage:Qdrant:ApiKey"];
            var isHttpsConnection = configuration["VectorStorage:Qdrant:IsHttpsConnection"] == null ? false 
                : bool.Parse(configuration["VectorStorage:Qdrant:IsHttpsConnection"]!);

            services.AddQdrantVectorStore(hostname, port, apiKey: key, https: isHttpsConnection);
        }
    }
}
