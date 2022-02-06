using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateData
{
    public class TableRow
    {
        public int EmployeeId { get; set; }
        
        public DateTime WorkingTimeFrom { get; set; }

        public DateTime WorkingTimeTo { get; set; }

        public DateTime DinnerTimeFrom { get; set; }

        public DateTime DinnerTimeTo { get; set; }

        public TableRow(int personId, DateTime workingTimeFrom, DateTime workingTimeTo
            , DateTime dinnerTimeFrom, DateTime dinnerTimeTo)
        {
            EmployeeId = personId;
            WorkingTimeFrom = workingTimeFrom;
            WorkingTimeTo = workingTimeTo;
            DinnerTimeFrom = dinnerTimeFrom;
            DinnerTimeTo = dinnerTimeTo;
        }

        public void Output()
        {
            Console.WriteLine($"PersonId = {EmployeeId}; WorkingTimeFrom = {WorkingTimeFrom}; " +
                $"WorkingTimeTo = {WorkingTimeTo}; DinnerTimeFrom = {DinnerTimeFrom}; DinnerTimeTo = {DinnerTimeTo}" );
        }
    }
}
