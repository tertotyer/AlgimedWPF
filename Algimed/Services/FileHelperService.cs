using Algimed.Data;
using Algimed.Models;
using LumenWorks.Framework.IO.Csv;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algimed.Services
{
    public class FileHelperService
    {
        public static List<Models.Result> ReadFile(string pathFile)
        {
            Dictionary<string, double?> cols = new Dictionary<string, double?>(3)
            {
                {"C01", null },
                {"D01", null },
                {"F01", null },
            };
            Dictionary<string, double?> rows = new Dictionary<string, double?>(3)
            {
                {"E01", null },
                {"G01", null },
                {"H01", null },
            };

            if (pathFile.EndsWith(".csv"))
            {
                ReadCSV(pathFile, cols, rows);
            }
            else
            {
                ReadXLSX(pathFile, cols, rows);
            }

            var list = new List<Models.Result>();
            using (var db = new ApplicationDbContext())
            {
                foreach (var col in cols)
                {
                    foreach (var row in rows)
                    {
                        Result result = new Result()
                        {
                            Cells = $"{col.Key}_{row.Key}"
                        };
                        if (col.Value != null && row.Value != null)
                        {
                            result.Value = Math.Round(Math.Pow((double)col.Value - (double)row.Value, 2), 2);
                        }

                        db.Results.AddOrUpdate(result);
                        list.Add(result);
                    }
                }
                db.SaveChanges();
            }

            return list;
        }

        private static void ReadCSV(string pathCsvFile, Dictionary<string, double?> cols, Dictionary<string, double?> rows)
        {
            using (CsvReader csv = new CsvReader(new StreamReader(pathCsvFile), true))
            {
                var headers = csv.GetFieldHeaders();

                if (headers[0] == "C01" || headers[0] == "D01" || headers[0] == "F01")
                {
                    cols[headers[0]] = double.Parse(headers[5], CultureInfo.InvariantCulture);
                }

                if (headers[0] == "E01" || headers[0] == "G01" || headers[0] == "H01")
                {
                    rows[headers[0]] = double.Parse(headers[5], CultureInfo.InvariantCulture);
                }

                while (csv.ReadNextRecord())
                {
                    if (csv[0] == "C01" || csv[0] == "D01" || csv[0] == "F01")
                    {
                        cols[csv[0]] = double.Parse(csv[5], CultureInfo.InvariantCulture);
                    }

                    if (csv[0] == "E01" || csv[0] == "G01" || csv[0] == "H01")
                    {
                        rows[csv[0]] = double.Parse(csv[5], CultureInfo.InvariantCulture);
                    }
                }
            }
        }

        private static void ReadXLSX(string pathile, Dictionary<string, double?> cols, Dictionary<string, double?> rows)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(new FileInfo(pathile)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                for (int i = 1; i < int.MaxValue; i++)
                {
                    if (worksheet.Cells[i, 1].Value == null) break;

                    var cell = worksheet.Cells[i, 1].Value.ToString();

                    if (cell == "C01" || cell == "D01" || cell == "F01")
                    {
                        cols[cell] = double.Parse(worksheet.Cells[i, 6].Value.ToString(), CultureInfo.InvariantCulture); ;
                    }

                    if (cell == "E01" || cell == "G01" || cell == "H01")
                    {
                        rows[cell] = double.Parse(worksheet.Cells[i, 6].Value.ToString(), CultureInfo.InvariantCulture); ;
                    }
                }
            }
        }
    }
}
