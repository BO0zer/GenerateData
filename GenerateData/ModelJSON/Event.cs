using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateData
{
    [Serializable]
    public class Event
    {
        [JsonProperty("expected_time")]
        public DateTime ExpectedTime { get; set; }

        [JsonProperty("earlier")]
        public TimeDeviationProbability Earlier { get; set; }

        [JsonProperty("later")]
        public TimeDeviationProbability Later { get; set; }
    }
}
