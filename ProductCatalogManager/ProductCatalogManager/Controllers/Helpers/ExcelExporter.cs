using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using DataTable = System.Data.DataTable;

namespace ProductCatalogManager.Controllers.Helpers
{
    public class ExcelExporter
    {
        public static byte[] ExportDataToExcel<T>(List<T> data, string entityType)
        {
            var table = ConvertToDataTable<T>(data);

            var filePath = Path.Combine(Path.GetTempPath(), entityType + DateTime.Now.Ticks + ".xlsx");

            var excelApp = new Application();

            excelApp.Visible = false;
            excelApp.DisplayAlerts = false;

            var excelworkBook = excelApp.Workbooks.Add(Type.Missing);

            var excelSheet = (Worksheet)excelworkBook.ActiveSheet;
            excelSheet.Name = entityType + " List";

            for (var i = 0; i < table.Columns.Count; i++)
            {
                excelSheet.Cells[1, i + 1] = table.Columns[i].ColumnName;
            }

            for (var i = 0; i < table.Rows.Count; i++)
            {
                for (var j = 0; j < table.Columns.Count; j++)
                {
                    excelSheet.Cells[i + 2, j + 1] = table.Rows[i][j];
                }
            }
            excelSheet.SaveAs(filePath);
            excelworkBook.Close();
            excelApp.Quit();

            return File.ReadAllBytes(filePath);
        }

        private static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));

            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (T item in data)
            {
                DataRow row = table.NewRow();

                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;

                table.Rows.Add(row);
            }

            return table;
        }
    }
}