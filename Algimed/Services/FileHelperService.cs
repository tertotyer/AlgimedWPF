using Algimed.Data;
using Algimed.Models;
using LumenWorks.Framework.IO.Csv;
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
        public static List<Models.Result> ReadCSV(string pathCsvFile)
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
    }
}
