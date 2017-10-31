using BinanceService.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BinanceService.Responses {
    public class DepthEvent {
        [JsonProperty("e")]
        public string EventType { get; set; }

        [JsonProperty("s")]
        public string Symbol { get; set; }

        [JsonProperty("E")]
        [JsonConverter(typeof(UnixTimestampJsonConverter))]
        public DateTime EventTime { get; set; }

        [JsonProperty("u")]
        public long UpdateId{ get; set; }

        [JsonProperty("b")]
        public dynamic[] BidsDynamic { get; set; }
        public IEnumerable<(decimal Price, decimal Quantity)> Bids => BidsDynamic.Select(x => ((decimal)x[0], (decimal)x[1]));

        [JsonProperty("a")]
        public dynamic[] AsksDynamic { get; set; }
        public IEnumerable<(decimal Price, decimal Quantity)> Asks => AsksDynamic.Select(x => ((decimal)x[0], (decimal)x[1]));
    }
}