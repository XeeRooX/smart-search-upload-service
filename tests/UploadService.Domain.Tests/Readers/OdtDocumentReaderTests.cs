using UploadService.Domain.Common;
using UploadService.Domain.Readers;

namespace UploadService.Domain.Tests.Readers
{
    public class OdtDocumentReaderTests
    {
        [Fact]
        public async Task ReadOdtParagraphsTest()
        {
            var odtPath = "Files\\Odt\\paragraphs.odt";
            using var fileStream = new FileStream(odtPath, FileMode.Open);
            var reference = await File.ReadAllTextAsync("Files\\References\\paragraphs.txt");

            IDocumentReader odtReader = new OdtDocumentReader();
            var pdfContent = odtReader.Read(fileStream);

            Assert.Equal(reference, pdfContent);
        }

        [Fact]
        public async Task ReadOdtTablesTest()
        {
            var odtPath = "Files\\Odt\\tables.odt";
            using var fileStream = new FileStream(odtPath, FileMode.Open);
            var reference = await File.ReadAllTextAsync("Files\\References\\docTables.txt");

            IDocumentReader odtReader = new OdtDocumentReader();
            var odtContent = odtReader.Read(fileStream);

            Assert.Equal(reference, odtContent);
        }

        [Fact]
        public async Task ReadOdtListsTest()
        {
            var odtPath = "Files\\Odt\\lists.odt";
            using var fileStream = new FileStream(odtPath, FileMode.Open);
            var reference = await File.ReadAllTextAsync("Files\\References\\lists.txt");

            IDocumentReader odtReader = new OdtDocumentReader();
            var odtContent = odtReader.Read(fileStream);

            Assert.Equal(reference, odtContent);
        }
    }
}
