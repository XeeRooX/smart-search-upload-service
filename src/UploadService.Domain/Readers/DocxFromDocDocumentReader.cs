using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Text;
using System.Text.RegularExpressions;
using UploadService.Domain.Common;

namespace UploadService.Domain.Readers
{
    public class DocxFromDocDocumentReader : IDocumentReader
    {
        public string? Read(Stream stream)
        {
            using var document = WordprocessingDocument.Open(stream, false);

            var stringBuilder = new StringBuilder();
            var body = document.MainDocumentPart?.Document.Body;

            if (body == null)
                return null;

            foreach (var element in body.ChildElements)
            {
                switch (element.GetType().ToString())
                {
                    case "DocumentFormat.OpenXml.Wordprocessing.Paragraph":
                    case "w:p":
                        stringBuilder.Append(element.InnerText + " ");
                        continue;
                    case "DocumentFormat.OpenXml.Wordprocessing.Table":
                    case "w:tbl":
                        foreach (var row in ((Table)element).Elements<TableRow>())
                        {
                            foreach (var cell in row.Elements<TableCell>())
                            {
                                stringBuilder.Append(cell.InnerText + " ");
                            }
                        }
                        continue;
                    default:
                        continue;
                }
            }

            var result = stringBuilder.ToString().Trim();
            result = Regex.Replace(result, " +", " ");
            return result;
        }
    }
}
