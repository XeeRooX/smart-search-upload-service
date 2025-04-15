using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using System.Text;
using System.Text.RegularExpressions;
using UploadService.Domain.Common;

namespace UploadService.Domain.Readers
{
    public class XlsDocumentReader : IDocumentReader
    {
        public string? Read(Stream stream)
        {
            using var workbook = new HSSFWorkbook(stream);
            var stringBuilder = new StringBuilder();

            for (int sheetIndex = 0; sheetIndex < workbook.NumberOfSheets; sheetIndex++)
            {
                var sheet = workbook.GetSheetAt(sheetIndex);
   
                for(int rowIndex = 0; rowIndex <= sheet.LastRowNum; rowIndex++)
                {
                    var row = sheet.GetRow(rowIndex);
                    if (row == null)
                        continue;

                    for (int cellIndex = 0; cellIndex < row.Cells.Count; cellIndex++)
                    {
                        var cell = row.GetCell(cellIndex);

                        if(cell != null)
                        {
                            switch (cell.CellType)
                            {
                                case NPOI.SS.UserModel.CellType.Numeric:
                                    stringBuilder.Append(cell.NumericCellValue.ToString() + " ");
                                    continue;
                                case NPOI.SS.UserModel.CellType.String:
                                    stringBuilder.Append(cell.StringCellValue + " ");
                                    continue;
                                case NPOI.SS.UserModel.CellType.Unknown:
                                    if(string.IsNullOrEmpty(cell.ToString()))
                                        stringBuilder.Append(cell.ToString() + " ");
                                    continue;
                                default:
                                    continue;
                            }
                        }
                    }
                }
            }

            var result = stringBuilder.ToString().Trim();
            result = Regex.Replace(result, " +", " ");
            return result;
        }
    }
}
