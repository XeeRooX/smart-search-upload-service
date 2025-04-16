using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using UploadService.Application.Services;
using UploadService.Application.Services.Chunking;
using UploadService.Domain.Services;

namespace UploadService.Application.Extensions
{
    public static class ApplicationExtensions
    {
        public static void ConfigureApplication(this IServiceCollection services, IConfiguration configuration)
        {
            // Регистрация пользовательских сервисов
            services.AddScoped<IChunkingService, XLMRobertaChunkingService>();
            services.AddScoped<IDocumentReaderService, DocumentReaderService>();
            services.AddScoped<IDocumentUploaderService, DocumentUploaderService>();
            services.AddScoped<IDocumetLoaderService, DocumentLoaderService>();

            services.AddHttpClient();

            // Конфигурация векторной БД
            services.ConfigureQdrant(configuration);
            // Конфигурация модели
            services.ConfigureOllama(configuration);
        }
    }
}
