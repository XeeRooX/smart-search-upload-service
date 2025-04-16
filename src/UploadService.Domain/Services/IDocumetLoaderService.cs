using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploadService.Domain.Services
{
    public interface IDocumetLoaderService
    {
        Task<byte[]?> LoadByUrlAsync(string documentUrl);
    }
}
