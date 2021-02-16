using System.Collections.Generic;
using System.IO;

using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Excel
{
    internal static class Excel
    {
        static void Main()
        {
            using var package = new ExcelPackage();

            var sheet = package.Workbook.Worksheets.Add("Persons");
            CreateHeaders(sheet);

            var persons = new List<Person>
            {
                new Person("Дмитрий", "Иванов", 18, "8-913-888-11-55"),
                new Person("Сергей", "Петров", 33, "8-952-185-12-00"),
                new Person("Максим", "Сидоров", 58, "8-960-798-55-55"),
                new Person("Василий", "Жаров", 22, "8-903-841-39-14")
            };

            SetCellsValues(sheet, persons);
            SetTableStyle(sheet);
            package.SaveAs(new FileInfo("Persons.xlsx"));
        }

        private static void CreateHeaders(ExcelWorksheet sheet)
        {
            sheet.Cells["A1:D1"].Merge = true;
            sheet.Cells["A1"].Value = "Контактные лица";
            sheet.Cells["A2"].Value = "Имя";
            sheet.Cells["B2"].Value = "Фамилия";
            sheet.Cells["C2"].Value = "Возраст";
            sheet.Cells["D2"].Value = "Телефон";
        }

        private static void SetCellsValues(ExcelWorksheet sheet, IEnumerable<Person> persons)
        {
            foreach (var person in persons)
            {
                var lastRow = sheet.Dimension.Rows + 1;

                sheet.Cells[lastRow, 1, lastRow, 1].Value = person.FirstName;
                sheet.Cells[lastRow, 2, lastRow, 2].Value = person.LastName;
                sheet.Cells[lastRow, 3, lastRow, 3].Value = person.Age;
                sheet.Cells[lastRow, 4, lastRow, 4].Value = person.Phone;
            }
        }

        private static void SetTableStyle(ExcelWorksheet sheet)
        {
            var usedRange = sheet.Cells[sheet.Dimension.Address];

            usedRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            usedRange.AutoFitColumns();

            usedRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            usedRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            usedRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            usedRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
        }
    }
}