using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateData
{
    public class Employee
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("working_time")]
        public TimeAction WorkingTime { get; set; }

        [JsonProperty("dinner_time")]
        public TimeAction DinnerTime { get; set; }
    }
}
