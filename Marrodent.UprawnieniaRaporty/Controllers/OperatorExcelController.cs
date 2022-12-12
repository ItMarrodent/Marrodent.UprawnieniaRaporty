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
                    ExcelWorksheet excelWorksheetPermissions = package.Workbook.Worksheets.Add("Uprawnienia");
                    PrepareColumnsPermissions(ref excelWorksheetPermissions);
                    AddDataPermissions(ref excelWorksheetPermissions, _data);
                    AutoFit(ref excelWorksheetPermissions);

                    ExcelWorksheet excelWorksheetLastLogin = package.Workbook.Worksheets.Add("Ostatnie logowanie");
                    PrepareColumnsLastLogin(ref excelWorksheetLastLogin);
                    AddDataLastLogin(ref excelWorksheetLastLogin, _data);
                    AutoFit(ref excelWorksheetLastLogin);

                    package.Save();
                    package.Dispose();
                }

                stream.Flush();
                stream.Close();
            }
        }

        //Private
        private void PrepareColumnsPermissions(ref ExcelWorksheet worksheet)
        {
            worksheet.Cells[1, 1].Value = "Kod operatora";
            worksheet.Cells[1, 2].Value = "Nazwa operatora";

            for (int x = 3; x < _data.Max(y => y.Permissions.Count) + 3; x++)
            {
                worksheet.Cells[1, x].Value = $"Uprawnienie {x - 2}";
            }
        }
        private void PrepareColumnsLastLogin(ref ExcelWorksheet worksheet)
        {
            worksheet.Cells[1, 1].Value = "Kod operatora";
            worksheet.Cells[1, 2].Value = "Nazwa operatora";
            worksheet.Cells[1, 3].Value = "Ostatnie logowanie";
        }

        private void AddDataPermissions(ref ExcelWorksheet worksheet, ICollection<OperatorExcelRow> data)
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

        private void AddDataLastLogin(ref ExcelWorksheet worksheet, ICollection<OperatorExcelRow> data)
        {
            int row = 2;

            foreach (var line in data)
            {
                worksheet.Cells[row, 1].Value = line.Name;
                worksheet.Cells[row, 2].Value = line.FullName;
                worksheet.Cells[row, 3].Value = line.LastLogin;

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
