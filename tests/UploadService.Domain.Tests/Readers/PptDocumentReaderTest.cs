using UploadService.Domain.Common;
using UploadService.Domain.Readers;

namespace UploadService.Domain.Tests.Readers
{
    public class PptDocumentReaderTest
    {
        [Fact]
        public async Task ReadPptSlidesTest()
        {
            var pptPath = "Files\\Ppt\\slides.ppt";
            var fileStream = new FileStream(pptPath, FileMode.Open);
            var reference = await File.ReadAllTextAsync("Files\\References\\slides.txt");

            IDocumentReader pptReader = new PptDocumentReader();
            var pptContent = pptReader.Read(fileStream);

            Assert.Equal(reference, pptContent);
        }
    }
}
