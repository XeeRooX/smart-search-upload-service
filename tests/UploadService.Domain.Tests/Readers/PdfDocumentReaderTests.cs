using UploadService.Domain.Common;
using UploadService.Domain.Readers;

namespace UploadService.Domain.Tests.Readers
{
    public class PdfDocumentReaderTests
    {
        [Fact]
        public async Task ReadPdfParagraphsTest()
        {
            var pdfPath = "Files\\Pdf\\paragraphs.pdf";
            using var fileStream = new FileStream(pdfPath, FileMode.Open);
            var reference = await File.ReadAllTextAsync("Files\\References\\paragraphs.txt");

            IDocumentReader pdfReader = new PdfDocumentReader();
            var pdfContent = pdfReader.Read(fileStream);

            Assert.Equal(reference, pdfContent);
        }

        [Fact]
        public async Task ReadPdfTablesTest()
        {
            var pdfPath = "Files\\Pdf\\tables.pdf";
            using var fileStream = new FileStream(pdfPath, FileMode.Open);
            var reference = await File.ReadAllTextAsync("Files\\References\\docTables.txt");

            IDocumentReader pdfReader = new PdfDocumentReader();
            var pdfContent = pdfReader.Read(fileStream);

            Assert.Equal(reference, pdfContent);
        }

        [Fact]
        public async Task ReadPdfListsTest()
        {
            var pdfPath = "Files\\Pdf\\lists.pdf";
            using var fileStream = new FileStream(pdfPath, FileMode.Open);
            var reference = await File.ReadAllTextAsync("Files\\References\\Pdf\\lists.txt");

            IDocumentReader pdfReader = new PdfDocumentReader();
            var pdfContent = pdfReader.Read(fileStream);

            Assert.Equal(reference, pdfContent);
        }
    }
}
