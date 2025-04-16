using UploadService.Domain.Common.Entities;
using UploadService.Domain.Common.Enums;

namespace UploadService.Dtos
{
    public class DocumentUploadIn
    {
        public Guid DocumentId { get; set; }
        public string DocumentName { get; set; } = null!;
        public string DocumentUrl { get; set; } = null!;
        public Guid ListId { get; set; }
        public string ListName { get; set; } = null!;
    }

    public static class DocumentUploadInMappers
    {
        public static DocumentInfo Map(this DocumentUploadIn from, DocumentInfo to)
        {
            to.Name = from.DocumentName;
            to.Url = from.DocumentUrl;
            to.Id = from.DocumentId;
            to.Type = GetTypeByFilename(from.DocumentName);

            return to;
        }

        public static DocumentListInfo Map(this DocumentUploadIn from, DocumentListInfo to)
        {
            to.Name = from.ListName;
            to.Id = from.ListId;

            return to;
        }

        static DocumentType GetTypeByFilename(string filename)
        {
            var extension = Path.GetExtension(filename).Trim().ToLower();

            return extension switch
            {
                "pdf" => DocumentType.Pdf,
                "rtf" => DocumentType.Rtf,
                "odt" => DocumentType.Odt,
                "doc" => DocumentType.Doc,
                "docx" => DocumentType.Docx,
                "ods" => DocumentType.Ods,
                "xls" => DocumentType.Xls,
                "xlsx" => DocumentType.Xlsx,
                _ => throw new ArgumentException()
            };

        }
    }
}
