using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploadService.Domain.Common
{
    public interface IDocumentReader
    {
        string? Read(Stream stream);
    }
}
