using UploadService.Domain.Common;
using UploadService.Domain.Readers;

namespace UploadService.Domain.Tests.Readers
{
    public class RtfDocumentReaderTests
    {
        [Fact]
        public async Task ReadRtfParagraphsTest()
        {
            var pdfPath = "Files\\Rtf\\paragraphs.rtf";
            using var fileStream = new FileStream(pdfPath, FileMode.Open);
            var reference = await File.ReadAllTextAsync("Files\\References\\paragraphs.txt");

            IDocumentReader rtfReader = new RtfDocumentReader();
            var rtfContent = rtfReader.Read(fileStream);

            Assert.Equal(reference, rtfContent);
        }

        [Fact]
        public async Task ReadRtfTablesTest()
        {
            var rtfPath = "Files\\Rtf\\tables.rtf";
            using var fileStream = new FileStream(rtfPath, FileMode.Open);
            var reference = await File.ReadAllTextAsync("Files\\References\\docTables.txt");

            IDocumentReader rtfReader = new RtfDocumentReader();
            var rtfContent = rtfReader.Read(fileStream);

            Assert.Equal(reference, rtfContent);
        }

        [Fact]
        public async Task ReadRtfListsTest()
        {
            var rtfPath = "Files\\Rtf\\lists.rtf";
            using var fileStream = new FileStream(rtfPath, FileMode.Open);
            var reference = await File.ReadAllTextAsync("Files\\References\\Rtf\\lists.txt");

            IDocumentReader rtfReader = new RtfDocumentReader();
            var rtfContent = rtfReader.Read(fileStream);

            Assert.Equal(reference, rtfContent);
        }
    }
}
