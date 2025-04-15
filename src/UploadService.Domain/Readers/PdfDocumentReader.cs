using System.Text;
using System.Text.RegularExpressions;
using UglyToad.PdfPig;
using UploadService.Domain.Common;

namespace UploadService.Domain.Readers
{
    public class PdfDocumentReader : IDocumentReader
    {
        public string? Read(Stream stream)
        {
            using var document = PdfDocument.Open(stream);
            var stringBuilder = new StringBuilder();

            foreach (var page in document.GetPages())
            {
                stringBuilder.AppendFormat(page.Text);
            }

            var result = stringBuilder.ToString().Trim();
            result = Regex.Replace(result, " +", " ");
            return result;
        }
    }
}
