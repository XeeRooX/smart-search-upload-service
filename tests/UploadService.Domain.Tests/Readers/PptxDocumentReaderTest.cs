using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadService.Domain.Common;
using UploadService.Domain.Readers;

namespace UploadService.Domain.Tests.Readers
{
    public class PptxDocumentReaderTest
    {
        [Fact]
        public async Task ReadPptxSlidesTest()
        {
            var pptxPath = "Files\\Pptx\\slides.pptx";
            using var fileStream = new FileStream(pptxPath, FileMode.Open);
            var reference = await File.ReadAllTextAsync("Files\\References\\slides.txt");

            IDocumentReader pptxReader = new PptxDocumentReader();
            var pptxContent = pptxReader.Read(fileStream);

            Assert.Equal(reference, pptxContent);
        }
    }
}
