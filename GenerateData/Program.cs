using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;

namespace GenerateData
{
    class Program
    {
        static List<TableRow> rows = new List<TableRow>();
        static List<Row> rowsO = new List<Row>();
        class Row
        {
            public int EmployeeId { get; set; }
            public string WorkingTimeFrom { get; set; }
            public string WorkingTimeTo { get; set; }
            public string DinnerTimeFrom { get; set; }
            public string DinnerTimeTo { get; set; }
            public Row(int employeeId, string dinnerTimeFrom, string dinnerTimeTo, string workingTimeFrom, string workingTimeTo)
            {
                EmployeeId = employeeId;
                DinnerTimeFrom = dinnerTimeFrom;
                DinnerTimeTo = dinnerTimeTo;
                WorkingTimeFrom = workingTimeFrom;
                WorkingTimeTo = workingTimeTo;
            }
        }
        static void Main(string[] args)
        {

            //create configuration csv MS Excel format
            var cfg = new CsvConfiguration(CultureInfo.InvariantCulture);
            cfg.Delimiter = ";"; //ms excel format

            //write csv
            using (var sw = new StreamWriter($"test.csv"))
            using (var csv = new CsvWriter(sw, cfg))
            {
                csv.WriteRecords(rowsO);
            }


        }

        static void GenerateData()
        {

        }
    }
}
