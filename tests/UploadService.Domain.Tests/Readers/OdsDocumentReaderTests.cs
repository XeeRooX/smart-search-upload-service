using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadService.Domain.Common;
using UploadService.Domain.Readers;

namespace UploadService.Domain.Tests.Readers
{
    public class OdsDocumentReaderTests
    {
        [Fact]
        public async Task ReadOdsTablesOneTest()
        {
            var odsPath = "Files\\Ods\\tables1.ods";
            using var fileStream = new FileStream(odsPath, FileMode.Open);
            var reference = await File.ReadAllTextAsync("Files\\References\\tables1.txt");

            IDocumentReader odsReader = new OdsDocumentReader();
            var odsContent = odsReader.Read(fileStream);

            Assert.Equal(reference, odsContent);
        }
    }
}
