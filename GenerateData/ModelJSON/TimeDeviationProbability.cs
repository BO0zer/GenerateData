using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateData
{
    public class TimeDeviationProbability
    {
        [JsonProperty("probability")]
        public int Probability { get; set; }

        [JsonProperty("max_time_deviation")]
        public DateTime MaxTimeDeviation { get; set; }
    }
}
