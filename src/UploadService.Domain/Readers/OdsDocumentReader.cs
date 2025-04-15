using System.IO.Compression;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using UploadService.Domain.Common;

namespace UploadService.Domain.Readers
{
    public class OdsDocumentReader : IDocumentReader
    {
        public string? Read(Stream stream)
        {
            var content = Unzip(stream);
            using var memoryStream = new MemoryStream(content);

            var settings = new XmlWriterSettings() { ConformanceLevel = ConformanceLevel.Auto };
            var stringBuilder = new StringBuilder();

            var xmlReader = XmlReader.Create(memoryStream);
            SkipToSpreadsheet(xmlReader);
            while (xmlReader.Read())
            {
                if (xmlReader.NodeType == XmlNodeType.Text)
                {
                    stringBuilder.Append(xmlReader.Value + " ");
                }
            }

            var result = stringBuilder.ToString().Trim();
            result = Regex.Replace(result, " +", " ");
            return result;
        }

        void SkipToSpreadsheet(XmlReader reader)
        {
            reader.ReadToFollowing("office:spreadsheet");
        }

        public byte[] Unzip(Stream stream)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ZipArchive archive = new ZipArchive(stream);
                var unzippedEntryStream = archive.GetEntry("content.xml")!.Open();
                unzippedEntryStream.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
