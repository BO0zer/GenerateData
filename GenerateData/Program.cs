using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;

namespace GenerateData
{
    class Program
    {
        static List<TableRow> rows = new List<TableRow>();
        static List<Row> rowsO = new List<Row>();
        class Row
        {
            public int PersonId { get; set; }
            public string DataComing { get; set; }
            public string DataLeaving { get; set; }
            public Row(int personId, string dataComing, string dataLeaving)
            {
                PersonId = personId;
                DataComing = dataComing;
                DataLeaving = dataLeaving;
            }
        }
        static void Main(string[] args)
        {
            Random r = new Random();

            //(8-10) (16-18) (Время отсутствия в минутах: 5 - 60) Количество пар пришел ушел (2 - 6)

            for (int i = 0; i < 29; i++)
            {
                DateTime timeComing = new DateTime(2020, 3, 1 + i);
                if (timeComing.DayOfWeek == DayOfWeek.Saturday || timeComing.DayOfWeek == DayOfWeek.Sunday)
                    continue;
                for (int j = 0; j < 15; j++)
                {
                    if (r.Next(1, 8) == 2)
                        continue;

                    int personId = j;
                    timeComing = timeComing.AddHours(r.Next(8, 10)).AddMinutes(r.Next(0, 59));
                    int stayH = r.Next(1, 4);
                    int stayM = r.Next(0, 59);
                    DateTime timeLeaving = timeComing.AddHours(stayH).AddMinutes(stayM);
                    TableRow row = new TableRow(personId, timeComing.ToUniversalTime(), timeLeaving.ToUniversalTime());
                    Row rowO = new Row(personId, timeComing.ToUniversalTime().ToString("O"), timeLeaving.ToUniversalTime().ToString("O"));
                    rows.Add(row);
                    rowsO.Add(rowO);

                    GenerateData(timeLeaving, personId);
                    timeComing = new DateTime(2020, 3, 1 + i);
                }

            }

            foreach (var item in rows)
            {
                item.Output();
            }

            //create configuration csv MS Excel format
            var cfg = new CsvConfiguration(CultureInfo.InvariantCulture);
            cfg.Delimiter = ";"; //ms excel format

            //write csv
            using (var sw = new StreamWriter($"test.csv"))
            using (var csv = new CsvWriter(sw, cfg))
            {
                csv.WriteRecords(rowsO);
            }

            Console.ReadLine();
        }

        static void GenerateData(DateTime timeLeaving, int personId)
        {
            Random r = new Random();
            while (timeLeaving.Hour < 17)
            {
                int empty = r.Next(5, 60);
                DateTime timeComing = timeLeaving.AddMinutes(empty);

                int stayH = r.Next(1, 4);
                int stayM = r.Next(0, 59);
                timeLeaving = timeComing.AddHours(stayH).AddMinutes(stayM);
                TableRow row = new TableRow(personId, timeComing.ToUniversalTime(), timeLeaving.ToUniversalTime());
                Row rowO = new Row(personId, timeComing.ToUniversalTime().ToString("O"), timeLeaving.ToUniversalTime().ToString("O"));
                rows.Add(row);
                rowsO.Add(rowO);
            }
        }
    }
}
