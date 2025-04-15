using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UploadService.Domain.Common;

namespace UploadService.Domain.Readers
{
    public class DocxDocumentReader : IDocumentReader
    {
        public string? Read(Stream stream)
        {
            using var document = WordprocessingDocument.Open(stream, false);

            var stringBuilder = new StringBuilder();
            var body = document.MainDocumentPart?.Document.Body;

            if(body == null)
                return null;

            foreach (var element in body.ChildElements)
            {
                switch (element)
                {
                    case Paragraph paragraph:
                        stringBuilder.Append(paragraph.InnerText + " ");
                        continue;
                    case Table table:
                        foreach (var row in table.Elements<TableRow>())
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
