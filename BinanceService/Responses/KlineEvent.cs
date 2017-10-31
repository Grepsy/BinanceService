using BinanceService.Converters;
using Newtonsoft.Json;
using System;

namespace BinanceService.Responses {
    public class KlineEvent {
        [JsonProperty("e")]
        public string EventType { get; set; }

        [JsonProperty("s")]
        public string Symbol { get; set; }

        [JsonProperty("E")]
        [JsonConverter(typeof(UnixTimestampJsonConverter))]
        public DateTime EventTime { get; set; }

        [JsonProperty("k")]
        public KlineData Kline { get; set; }

        public class KlineData {
            [JsonProperty("t")]
            [JsonConverter(typeof(UnixTimestampJsonConverter))]
            public DateTime StartTime { get; set; }

            [JsonProperty("T")]
            [JsonConverter(typeof(UnixTimestampJsonConverter))]
            public DateTime EndTime { get; set; }

            [JsonProperty("s")]
            public string Symbol { get; set; }

            [JsonProperty("i")]
            public string Interval { get; set; }

            [JsonProperty("f")]
            public long FirstTradeId { get; set; }

            [JsonProperty("L")]
            public long LastTradeId { get; set; }

            [JsonProperty("o")]
            public decimal Open { get; set; }

            [JsonProperty("c")]
            public decimal Close { get; set; }

            [JsonProperty("h")]
            public decimal High { get; set; }

            [JsonProperty("l")]
            public decimal Low { get; set; }

            [JsonProperty("v")]
            public decimal Volume { get; set; }

            [JsonProperty("n")]
            public int TradeCount { get; set; }

            [JsonProperty("x")]
            public bool IsFinal { get; set; }

            [JsonProperty("q")]
            public decimal QuoteVolume { get; set; }

            [JsonProperty("V")]
            public decimal BuyVolume { get; set; }

            [JsonProperty("Q")]
            public decimal BuyQuoteVolume { get; set; }
        }
    }
}