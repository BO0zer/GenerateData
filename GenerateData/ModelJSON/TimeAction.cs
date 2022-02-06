using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateData
{
    public class TimeAction
    {
        [JsonProperty("absense_probability")]
        public int AbsenseProbability { get; set; }

        [JsonProperty("start_event")]
        public Event StartEvent { get; set; }

        [JsonProperty("finish_event")]
        public Event FinishEvent { get; set; }


    }
}
