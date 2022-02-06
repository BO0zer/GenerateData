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
            GenerateData();
            ////create configuration csv MS Excel format
            //var cfg = new CsvConfiguration(CultureInfo.InvariantCulture);
            //cfg.Delimiter = ";"; //ms excel format

            ////write csv
            //using (var sw = new StreamWriter($"test.csv"))
            //using (var csv = new CsvWriter(sw, cfg))
            //{
            //    csv.WriteRecords(rowsO);
            //}
            Console.ReadLine();
        }

        static void GenerateData()
        {
            Config config = GetConfig();

            for (DateTime i = config.DateFrom; i < config.DateTo; i = i.AddDays(1))
            {
                foreach(var employee in config.Employees)
                {
                    Random r = new Random();
                    if(r.Next(0, 100) > employee.WorkingTime.AbsenseProbability)
                    {
                        if(r.Next(0, 100) <= employee.WorkingTime.StartEvent.Earlier.Probability)
                        {
                            r.Next(0, (int)employee.WorkingTime.StartEvent.Earlier.MaxTimeDeviation.TimeOfDay.TotalMinutes);
                        }
                    }
                }
            }

        }
        static Config GetConfig()
        {
            string fileName = @"C:\Users\Юриц\source\repos\GenerateData\GenerateData\Конфиг.json";
            if (File.Exists(fileName))
            {
                Config config =
                    JsonConvert.DeserializeObject<Config>
                        (File.ReadAllText(fileName));
                return config;
            }
            else
            {
                throw new Exception("Конфига не существует");
            }
        }

    }
}
