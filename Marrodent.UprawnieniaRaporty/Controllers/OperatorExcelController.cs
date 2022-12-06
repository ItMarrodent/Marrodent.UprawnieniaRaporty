using System.Collections.Generic;
using System.IO;
using System.Linq;
using Marrodent.UprawnieniaRaporty.Interfaces;
using Marrodent.UprawnieniaRaporty.Models;
using OfficeOpenXml;

namespace Marrodent.UprawnieniaRaporty.Controllers
{
    public sealed class OperatorExcelController : IOperatorExcelController
    {
        //Private
        private readonly ICollection<OperatorExcelRow> _data;

        //CTOR
        public OperatorExcelController(ICollection<OperatorExcelRow> data)
        {
            _data = data;
        }

        //Public
        public void GenerateFile(string path)
        {
            using (Stream stream = File.Create(path))
            {
                using (ExcelPackage package = new ExcelPackage(stream))
                {
                    ExcelWorksheet excelWorksheet = package.Workbook.Worksheets.Add("Uprawnienia");
                    PrepareColumns(ref excelWorksheet);
                    AddData(ref excelWorksheet, _data);
                    AutoFit(ref excelWorksheet);
                    package.Save();
                    package.Dispose();
                }

                stream.Flush();
                stream.Close();
            }
        }

        //Private
        private void PrepareColumns(ref ExcelWorksheet worksheet)
        {
            worksheet.Cells[1, 1].Value = "Kod operatora";
            worksheet.Cells[1, 2].Value = "Nazwa operatora";

            for (int x = 3; x < _data.Max(y => y.Permissions.Count) + 3; x++)
            {
                worksheet.Cells[1, x].Value = $"Uprawnienie {x - 2}";
            }
        }

        private void AddData(ref ExcelWorksheet worksheet, ICollection<OperatorExcelRow> data)
        {
            int row = 2;

            foreach (var line in data)
            {
                worksheet.Cells[row, 1].Value = line.Name;
                worksheet.Cells[row, 2].Value = line.FullName;

                for (int x = 3; x < line.Permissions.Count + 3; x++)
                {
                    worksheet.Cells[row, x].Value = line.Permissions[x - 3];
                }

                row++;
            }
        }

        private void AutoFit(ref ExcelWorksheet worksheet)
        {
            for (int x = 1; x <= _data.Max(y => y.Permissions.Count) + 2; x++)
            {
                worksheet.Column(x).AutoFit();
            }
        }
    }
}
