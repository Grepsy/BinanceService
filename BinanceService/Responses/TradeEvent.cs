using BinanceService.Converters;
using Newtonsoft.Json;
using System;

namespace BinanceService.Responses {
    public class TradeEvent {
        [JsonProperty("e")]
        public string EventType { get; set; }

        [JsonProperty("a")]
        public long TradeId { get; set; }

        [JsonProperty("E")]
        [JsonConverter(typeof(UnixTimestampJsonConverter))]
        public DateTime EventTime { get; set; }

        [JsonProperty("T")]
        [JsonConverter(typeof(UnixTimestampJsonConverter))]
        public DateTime TradeTime { get; set; }

        [JsonProperty("p")]
        public decimal Price { get; set; }

        [JsonProperty("q")]
        public decimal Quantity { get; set; }

        [JsonProperty("f")]
        public long FirstTradeId { get; set; }

        [JsonProperty("l")]
        public long LastTradeId { get; set; }

        [JsonProperty("m")]
        public bool IsMaker { get; set; }

        [JsonProperty("s")]
        public string Symbol { get; set; }
    }
}