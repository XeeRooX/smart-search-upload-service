using b2xtranslator.OpenXmlLib.PresentationML;
using b2xtranslator.PptFileFormat;
using b2xtranslator.PresentationMLMapping;
using b2xtranslator.Shell;
using b2xtranslator.StructuredStorage.Reader;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploadService.Domain.Readers.Converters
{
    public class PptToPptx
    {
        public MemoryStream Convert(Stream stream)
        {
            stream.Close();
            //var reader = new StructuredStorageReader(stream);


            var pathToPath = "C:\\projects\\smart-search\\console-playground\\UploadService\\tests\\UploadService.Domain.Tests\\Files\\Ppt\\slides.ppt";
            var processingFile = new ProcessingFile(pathToPath);

            using var reader = new StructuredStorageReader(processingFile.File.FullName);
            var ppt = new PowerpointDocument(reader);
            var outType = Converter.DetectOutputType(ppt);
            var pptx = PresentationDocument.Create("pptx", outType);

            Converter.Convert(ppt, pptx);

            return new MemoryStream(pptx.CloseWithoutSavingFile());


            //var ppt = new PowerpointDocument(reader);
            //var pptx = PresentationDocument.Create("pptx", b2xtranslator.OpenXmlLib.OpenXmlPackage.DocumentType.Document);

            //Converter.Convert(ppt, pptx);

            //return new MemoryStream(pptx.CloseWithoutSavingFile());
        }
    }
}
