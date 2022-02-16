using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;

namespace GenerateData
{
    class Program
    {
        static List<Row> rowsO = new List<Row>();
        class Row
        {
            public string EmployeeId { get; set; }
            public string WorkingTimeFrom { get; set; }
            public string WorkingTimeTo { get; set; }
            public string DinnerTimeFrom { get; set; }
            public string DinnerTimeTo { get; set; }
            public Row(string employeeId,  string workingTimeFrom, string workingTimeTo, string dinnerTimeFrom, string dinnerTimeTo)
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

            //create configuration csv MS Excel format
            var cfg = new CsvConfiguration(CultureInfo.InvariantCulture);
            cfg.Delimiter = ";"; //ms excel format

            string csvpath = @"test2.csv";
            StringBuilder csvContent = new StringBuilder();
            csvContent.AppendLine("EmployeeId; WorkingTimeFrom; WorkingTimeTo; DinnerTimeFrom; DinnerTimeTo");
            foreach (var row in rowsO)
            {
                csvContent.AppendLine($"{row.EmployeeId}; {row.WorkingTimeFrom}; {row.WorkingTimeTo}; {row.DinnerTimeFrom}; {row.DinnerTimeTo}");
            }

            File.AppendAllText(csvpath, csvContent.ToString());

            Console.ReadLine();
        }

        static void GenerateData()
        {
            Random r = new Random();
            Config config = GetConfig();
            DateTime i = config.DateFrom;
            for (; i < config.DateTo; i = i.AddDays(1))
            {
                if (i.DayOfWeek == DayOfWeek.Sunday || i.DayOfWeek == DayOfWeek.Saturday)
                    continue;
                for(int j = 0; j < config.Employees.Length; j++)
                {
                    Employee employee = config.Employees[j];
                    employee = CorrectFormat(employee, i);

                    DateTime wExpectedStartTime = DateTime.MinValue;
                    DateTime wExpectedFinishTime = DateTime.MinValue;
                    DateTime dExpectedStartTime = DateTime.MinValue;
                    DateTime dExpectedFinishTime = DateTime.MinValue;

                    if (r.Next(0, 100)  > employee.WorkingTime.AbsenseProbability)
                    {
                        wExpectedStartTime = Event.GetTime(employee.WorkingTime.StartEvent, r);
                        wExpectedFinishTime = Event.GetTime(employee.WorkingTime.FinishEvent, r);

                        if (r.Next(0, 100) > employee.DinnerTime.AbsenseProbability)
                        {
                            dExpectedStartTime = Event.GetTime(employee.DinnerTime.StartEvent, r);
                            dExpectedFinishTime = Event.GetTime(employee.DinnerTime.FinishEvent, r);
                        }
                    }

                    if (!(wExpectedStartTime == DateTime.MinValue || wExpectedFinishTime == DateTime.MinValue))
                    {
                        string employeeId = employee.Id;
                        string workingTimeFrom = wExpectedStartTime.ToString("O");
                        string workingTimeTo = wExpectedFinishTime.ToString("O");
                        string dinnerTimeFrom = "";
                        string dinnerTimeTo = "";

                        if (!(dExpectedStartTime == DateTime.MinValue || dExpectedFinishTime == DateTime.MinValue))
                        {
                            dinnerTimeFrom = dExpectedStartTime.ToString("O");
                            dinnerTimeTo = dExpectedFinishTime.ToString("O");
                        }
                        rowsO.Add(new Row(employeeId, workingTimeFrom, workingTimeTo, dinnerTimeFrom, dinnerTimeTo));
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
        public static Employee CorrectFormat(Employee employee, DateTime dateTime)
        {
            employee.DinnerTime.StartEvent.ExpectedTime = CorrectDateTime(employee.DinnerTime.StartEvent, dateTime);
            employee.DinnerTime.FinishEvent.ExpectedTime = CorrectDateTime(employee.DinnerTime.FinishEvent, dateTime);
            employee.WorkingTime.StartEvent.ExpectedTime = CorrectDateTime(employee.WorkingTime.StartEvent, dateTime);
            employee.WorkingTime.FinishEvent.ExpectedTime = CorrectDateTime(employee.WorkingTime.FinishEvent, dateTime);

            DateTime CorrectDateTime(Event someEvent, DateTime correctDateTime)
            {
                correctDateTime = correctDateTime.AddHours(someEvent.ExpectedTime.Hour);
                correctDateTime = correctDateTime.AddMinutes(someEvent.ExpectedTime.Minute);
                correctDateTime = correctDateTime.AddSeconds(someEvent.ExpectedTime.Second);

                return correctDateTime;
            }
            return employee;
        }
        
    }
}
