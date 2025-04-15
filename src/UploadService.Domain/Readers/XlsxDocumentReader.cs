using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using UploadService.Domain.Common;

namespace UploadService.Domain.Readers
{
    public class XlsxDocumentReader : IDocumentReader
    {
        public string? Read(Stream stream)
        {
            using var document = SpreadsheetDocument.Open(stream, false);
            var workbookPart = document.WorkbookPart;

            var numberFormatInfo = new NumberFormatInfo();
            numberFormatInfo.NumberDecimalSeparator = ",";

            if (workbookPart == null)
                return null;

            var sheets = workbookPart.Workbook.Sheets?.Elements<Sheet>();
            if (sheets == null)
                return null;

            var sharedStrings = GetSharedStrings(workbookPart.SharedStringTablePart);
            int currentRow = 0;
            var stringBuilder = new StringBuilder();

            foreach (var sheet in sheets)
            {
                var worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id!);


                using (var reader = OpenXmlReader.Create(worksheetPart))
                {
                    while (reader.Read())
                    {
                        if (reader.ElementType == typeof(Row) && reader.IsStartElement)
                            currentRow = Convert.ToInt32(reader.Attributes[0].Value);
                        else if (reader.ElementType == typeof(Cell))
                        {
                            var cellValue = (Cell?)reader.LoadCurrentElement();

                            if (cellValue == null)
                                continue;

                            if (cellValue.DataType != null && cellValue.DataType == CellValues.SharedString)
                            {
                                var sharedStringIdx = cellValue?.CellValue?.InnerText;
                                if (!string.IsNullOrEmpty(sharedStringIdx))
                                    stringBuilder.Append(sharedStrings.ElementAt(Convert.ToInt32(sharedStringIdx)) + " ");
                            }
                            else if (cellValue.DataType != null && (cellValue.DataType == CellValues.String || cellValue.DataType == CellValues.InlineString))
                            {
                                var text = cellValue?.InnerText;
                                if (!string.IsNullOrEmpty(text))
                                    stringBuilder.Append(text + " ");
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(cellValue?.InnerText))
                                    continue;

                                if (double.TryParse(cellValue?.InnerText, CultureInfo.InvariantCulture, out double number))
                                {
                                    var text = number.ToString(numberFormatInfo);
                                    stringBuilder.Append(text + " ");
                                    continue;
                                }

                                if (!string.IsNullOrEmpty(cellValue?.InnerText))
                                    stringBuilder.Append(cellValue?.InnerText + " ");
                            }
                        }
                    }
                }
            }

            var result = stringBuilder.ToString().Trim();
            result = Regex.Replace(result, " +", " ");
            return result;
        }

        static IEnumerable<string> GetSharedStrings(SharedStringTablePart? sharedStringTablePart)
        {
            if (sharedStringTablePart == null)
                yield break;

            foreach (var item in sharedStringTablePart.SharedStringTable.Elements<SharedStringItem>())
                yield return item.InnerText;
        }
    }
}
