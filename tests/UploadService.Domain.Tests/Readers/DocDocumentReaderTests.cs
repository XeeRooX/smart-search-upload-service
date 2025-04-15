using UploadService.Domain.Common;
using UploadService.Domain.Readers;

namespace UploadService.Domain.Tests.Readers
{
    public class DocDocumentReaderTests
    {
        [Fact]
        public async Task ReadDocParagraphsTest()
        {
            var docPath = "Files\\Doc\\paragraphs.doc";
            using var fileStream = new FileStream(docPath, FileMode.Open);
            var reference = await File.ReadAllTextAsync("Files\\References\\paragraphs.txt");

            IDocumentReader docReader = new DocDocumentReader();
            var docContent = docReader.Read(fileStream);

            Assert.Equal(reference, docContent);
        }

        [Fact]
        public async Task ReadDocxTablesTest()
        {
            var docPath = "Files\\Doc\\tables.doc";
            using var fileStream = new FileStream(docPath, FileMode.Open);
            var reference = await File.ReadAllTextAsync("Files\\References\\docTables.txt");

            IDocumentReader docReader = new DocDocumentReader();
            var docContent = docReader.Read(fileStream);

            Assert.Equal(reference, docContent);
        }

        [Fact]
        public async Task ReadDocListsTest()
        {
            var docPath = "Files\\Doc\\lists.doc";
            using var fileStream = new FileStream(docPath, FileMode.Open);
            var reference = await File.ReadAllTextAsync("Files\\References\\lists.txt");

            IDocumentReader docReader = new DocDocumentReader();
            var docContent = docReader.Read(fileStream);

            Assert.Equal(reference, docContent);
        }
    }
}
