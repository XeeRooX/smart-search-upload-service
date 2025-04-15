using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadService.Domain.Common;
using UploadService.Domain.Readers;

namespace UploadService.Domain.Tests.Readers
{
    public class XlsDocumentReaderTests
    {
        [Fact]
        public async Task ReadXlsTablesOneTest()
        {
            var xlsPath = "Files\\Xls\\tables1.xls";
            using var fileStream = new FileStream(xlsPath, FileMode.Open);
            var reference = await File.ReadAllTextAsync("Files\\References\\tables1.txt");

            IDocumentReader xlsReader = new XlsDocumentReader();
            var xlsContent = xlsReader.Read(fileStream);

            Assert.Equal(reference, xlsContent);
        }
    }
}
