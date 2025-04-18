using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadService.Domain.Common;
using UploadService.Domain.Readers.Converters;

namespace UploadService.Domain.Readers
{
    public class PptDocumentReader : IDocumentReader
    {
        public string? Read(Stream stream)
        {
            var converter = new PptToPptx();
            var memorySream = converter.Convert(stream);

            IDocumentReader pptxReader = new PptxDocumentReader();
            var result = pptxReader.Read(memorySream);
            return result;  
        }
    }
}
