using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateData
{
    public class TableRow
    {
        public int PersonId { get; set; }
        
        public DateTime TimeComing { get; set; }

        public DateTime TimeLeaving { get; set; }

        public TableRow(int personId, DateTime timeComing, DateTime timeLeaving)
        {
            PersonId = personId;
            TimeComing = timeComing;
            TimeLeaving = timeLeaving;
        }

        public void Output()
        {
            Console.WriteLine($"PersonId = {PersonId}; TimeComing = {TimeComing}; TimeLeaving = {TimeLeaving}");
        }
    }
}
