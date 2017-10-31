using Newtonsoft.Json;

namespace BinanceService.Responses {
    public class AggregateTradesResponse {
    }

    internal class AggregateTradeDto {
        [JsonProperty("a")]
        public long TradeId { get; set; }

        [JsonProperty("p")]
        public decimal Price { get; set; }

        [JsonProperty("q")]
        public decimal Quantity { get; set; }

        [JsonProperty("f")]
        public long FirstTradeId { get; set; }

        [JsonProperty("l")]
        public long LastTradeId { get; set; }

        [JsonProperty("T")]
        public long Timestamp { get; set; }

        [JsonProperty("m")]
        public bool IsMaker { get; set; }

        [JsonProperty("M")]
        public bool IsBestPrice { get; set; }
    }
}