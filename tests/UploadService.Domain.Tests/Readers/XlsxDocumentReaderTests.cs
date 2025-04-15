using UploadService.Domain.Common;
using UploadService.Domain.Readers;

namespace UploadService.Domain.Tests.Readers
{
    public class XlsxDocumentReaderTests
    {
        [Fact]
        public async Task ReadXlsxTablesOneTest()
        {
            var xlsxPath = "Files\\Xlsx\\tables1.xlsx";
            using var fileStream = new FileStream(xlsxPath, FileMode.Open);
            var reference = await File.ReadAllTextAsync("Files\\References\\tables1.txt");

            IDocumentReader xlsxReader = new XlsxDocumentReader();
            var xlsxContent = xlsxReader.Read(fileStream);

            Assert.Equal(reference, xlsxContent);
        }
    }
}
