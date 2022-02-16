using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        public static DateTime GetTime(Event someEvent, Random r)
        {
            if (r.Next(0, 100) <= someEvent.Earlier.Probability)
                return someEvent.ExpectedTime.AddSeconds(-
                    r.Next(0, someEvent.Earlier.MaxTimeDeviation));

            if (r.Next(0, 100) <= someEvent.Later.Probability)
                return someEvent.ExpectedTime.AddSeconds(
                    r.Next(0, someEvent.Earlier.MaxTimeDeviation));

            return someEvent.ExpectedTime.AddSeconds(r.Next(-120, 120));
        }
    }
}
