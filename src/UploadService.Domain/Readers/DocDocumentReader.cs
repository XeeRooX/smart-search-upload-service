using UploadService.Domain.Common;
using UploadService.Domain.Readers.Converters;

namespace UploadService.Domain.Readers
{
    public class DocDocumentReader : IDocumentReader
    {
        public string? Read(Stream stream)
        {
            var docToDocx = new DocToDocx();
            var memoryStream = docToDocx.Convert(stream);
            var docxReader = new DocxFromDocDocumentReader();

            var result = docxReader.Read(memoryStream);
            return result;
        }
    }
}
