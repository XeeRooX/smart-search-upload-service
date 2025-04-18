using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;
using System.Text;
using System.Text.RegularExpressions;
using UploadService.Domain.Common;

namespace UploadService.Domain.Readers
{
    public class PptxDocumentReader : IDocumentReader
    {
        public string? Read(Stream stream)
        {
            using var presentationDocument = PresentationDocument.Open(stream, false);
            int numberOfSlides = CountSlides(presentationDocument);

            var stringBuilder = new StringBuilder();

            for (int i = 0; i < numberOfSlides; i++)
            {
                stringBuilder.Append(GetSlideIdAndText(presentationDocument, i));
            }

            var result = stringBuilder.ToString().Trim();
            result = Regex.Replace(result, " +", " ");

            if (string.IsNullOrEmpty(result))
                return null;

            return result;
        }

        int CountSlides(PresentationDocument presentationDocument)
        {
            if (presentationDocument is null)
            {
                throw new ArgumentNullException("presentationDocument");
            }

            int slidesCount = 0;

            PresentationPart? presentationPart = presentationDocument.PresentationPart;
            if (presentationPart is not null)
            {
                slidesCount = presentationPart.SlideParts.Count();
            }

            return slidesCount;
        }

        string? GetSlideIdAndText(PresentationDocument presentationDocument, int index)
        {
            PresentationPart? part = presentationDocument.PresentationPart;
            OpenXmlElementList slideIds = part?.Presentation?.SlideIdList?.ChildElements ?? default;

            if (part is null || slideIds.Count == 0)
            {
                return "";
            }

            string? relId = ((SlideId)slideIds[index]).RelationshipId;

            if (relId is null)
            {
                return "";
            }
 
            SlidePart slide = (SlidePart)part.GetPartById(relId);
            StringBuilder paragraphText = new StringBuilder();

            IEnumerable<DocumentFormat.OpenXml.Drawing.Text> texts = slide.Slide.Descendants<DocumentFormat.OpenXml.Drawing.Text> ();
            IEnumerable<char> marks = [',', '.', '!', '?', ';', ':'];

            var textsCount = texts.Count();
            for (int textIndex = 0; textIndex < textsCount; textIndex++)
            {
                if(textIndex != textsCount - 1 && marks.Any(t => t == texts.ElementAt(textIndex + 1).Text[0]))
                    paragraphText.Append(texts.ElementAt(textIndex).Text);
                else
                    paragraphText.Append(texts.ElementAt(textIndex).Text + " ");
            }
           
            return paragraphText.ToString();
        }
    }
}
