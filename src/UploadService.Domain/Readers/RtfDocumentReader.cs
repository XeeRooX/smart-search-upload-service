using HtmlAgilityPack;
using RtfPipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UploadService.Domain.Common;

namespace UploadService.Domain.Readers
{
    public class RtfDocumentReader : IDocumentReader
    {
        public string? Read(Stream stream)
        {
            stream.Position = 0;
            string rtf = string.Empty;
            using (StreamReader sr = new StreamReader(stream))
            {
                rtf = sr.ReadToEnd();
            }

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var html = Rtf.ToHtml(rtf);

            var result = ReadFromHtml(html);
            return result;
        }

        static string? ReadFromHtml(string html)
        {
            var document = new HtmlDocument();

            document.LoadHtml(html);
            var stringBuilder = new StringBuilder();

            foreach (var node in document.DocumentNode.SelectNodes("//text()"))
            {
                stringBuilder.Append(node.InnerText+ " ");
            }

            var result = stringBuilder.ToString().Trim();
            result = Regex.Replace(result, " +", " ");
            return result;
        }
    }
}
