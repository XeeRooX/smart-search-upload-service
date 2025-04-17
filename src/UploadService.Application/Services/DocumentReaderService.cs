using UploadService.Domain.Common;
using UploadService.Domain.Common.Entities;
using UploadService.Domain.Common.Enums;
using UploadService.Domain.Readers;
using UploadService.Domain.Services;

namespace UploadService.Application.Services
{
    public class DocumentReaderService : IDocumentReaderService
    {
        public string? Read(byte[] documetContent, DocumentType documentType)
        {
            using var memoryStream = new MemoryStream(documetContent);

            var reader = GetReaderInstance(documentType);
            var text = reader.Read(memoryStream);

            return text;
        }

        IDocumentReader GetReaderInstance(DocumentType type) => type switch
        {
            DocumentType.Docx => new DocxDocumentReader(),
            DocumentType.Ods => new OdsDocumentReader(),
            DocumentType.Doc => new DocDocumentReader(),
            DocumentType.Pdf => new PdfDocumentReader(),
            DocumentType.Rtf => new RtfDocumentReader(),
            DocumentType.Odt => new OdtDocumentReader(),
            DocumentType.Xls => new XlsDocumentReader(),
            DocumentType.Xlsx => new XlsxDocumentReader(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
