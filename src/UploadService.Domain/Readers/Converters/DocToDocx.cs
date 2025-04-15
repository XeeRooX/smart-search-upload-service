using b2xtranslator.DocFileFormat;
using b2xtranslator.OpenXmlLib.WordprocessingML;
using b2xtranslator.StructuredStorage.Reader;
using b2xtranslator.WordprocessingMLMapping;
using static b2xtranslator.OpenXmlLib.OpenXmlPackage;
using System.IO;


namespace UploadService.Domain.Readers.Converters
{
    public class DocToDocx
    {
        public MemoryStream Convert(Stream stream)
        {
            var reader = new StructuredStorageReader(stream);
            var doc = new WordDocument(reader);
            var docx = WordprocessingDocument.Create("docx", b2xtranslator.OpenXmlLib.OpenXmlPackage.DocumentType.Document);
            Converter.Convert(doc, docx);
            //docx.Close();

            return new MemoryStream(docx.CloseWithoutSavingFile());
        }
    }
}
