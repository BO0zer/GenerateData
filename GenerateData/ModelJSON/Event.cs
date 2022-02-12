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
        private static int _randomInc = 1;

        [JsonProperty("expected_time")]
        public DateTime ExpectedTime { get; set; }

        [JsonProperty("earlier")]
        public TimeDeviationProbability Earlier { get; set; }

        [JsonProperty("later")]
        public TimeDeviationProbability Later { get; set; }

        public static DateTime GetTime(Event someEvent)
        {
            DateTime expectedTime;
            if (new Random(_randomInc * _randomInc * _randomInc * 10 - 100 * _randomInc++).Next(0, 100) <= someEvent.Earlier.Probability)
            {
                expectedTime = someEvent.ExpectedTime.AddSeconds(-
                    new Random().Next(0, someEvent.Earlier.MaxTimeDeviation));
            }
            else if (new Random(_randomInc * _randomInc * _randomInc * 10 - 100 * _randomInc++).Next(0, 100) <= someEvent.Later.Probability)
            {

                expectedTime = someEvent.ExpectedTime.AddSeconds(
                    new Random(_randomInc * _randomInc * _randomInc * 10 - 100 * _randomInc++).Next(0, someEvent.Earlier.MaxTimeDeviation));
            }
            else
            {
                expectedTime = someEvent.ExpectedTime;
            }
            return expectedTime;
        }
    }
}
