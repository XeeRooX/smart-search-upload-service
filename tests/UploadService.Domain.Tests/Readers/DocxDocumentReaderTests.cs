using UploadService.Domain.Common;
using UploadService.Domain.Readers;

namespace UploadService.Domain.Tests.Readers
{
    public class DocxDocumentReaderTests
    {
        [Fact]
        public async Task ReadDocxParagraphsTest()
        {
            var docxPath = "Files\\Docx\\paragraphs.docx";
            using var fileStream = new FileStream(docxPath, FileMode.Open);
            var reference = await File.ReadAllTextAsync("Files\\References\\paragraphs.txt");

            IDocumentReader docxReader = new DocxDocumentReader();
            var docxContent = docxReader.Read(fileStream);

            Assert.Equal(reference, docxContent);
        }

        [Fact]
        public async Task ReadDocxTablesTest()
        {
            var docxPath = "Files\\Docx\\tables.docx";
            using var fileStream = new FileStream(docxPath, FileMode.Open);
            var reference = await File.ReadAllTextAsync("Files\\References\\docTables.txt");

            IDocumentReader docxReader = new DocxDocumentReader();
            var docxContent = docxReader.Read(fileStream);

            Assert.Equal(reference, docxContent);
        }

        [Fact]
        public async Task ReadDocxListsTest()
        {
            var docxPath = "Files\\Docx\\lists.docx";
            using var fileStream = new FileStream(docxPath, FileMode.Open);
            var reference = await File.ReadAllTextAsync("Files\\References\\lists.txt");

            IDocumentReader docxReader = new DocxDocumentReader();
            var docxContent = docxReader.Read(fileStream);

            Assert.Equal(reference, docxContent);
        }
    }
}
