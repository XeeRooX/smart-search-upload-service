using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadService.Domain.Services;

namespace UploadService.Application.Services
{
    public class DocumentLoaderService(IHttpClientFactory httpClientFactory) : IDocumetLoaderService
    {
        public async Task<byte[]?> LoadByUrlAsync(string documentUrl)
        {
            var httpClient = httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync(documentUrl);

            if(response.IsSuccessStatusCode)
                return await response.Content.ReadAsByteArrayAsync();

            return null;
        }
    }
}
