using Newtonsoft.Json;

namespace BinanceService.Responses {
    public class PriceStatsResponse {
        public decimal PriceChange { get; set; }
        public decimal PriceChangePercent { get; set; }
        public decimal WeightedAvgPrice { get; set; }
        public decimal PrevClosePrice { get; set; }
        public decimal LastPrice { get; set; }
        public decimal BidPrice { get; set; }
        public decimal AskPrice { get; set; }
        public decimal OpenPrice { get; set; }
        public decimal HighPrice { get; set; }
        public decimal LowPrice { get; set; }
        public decimal Volume { get; set; }
        public decimal OpenTime { get; set; }
        public decimal CloseTime { get; set; }
        [JsonProperty("FristId")]
        public decimal FirstTradeId { get; set; }
        [JsonProperty("LastId")]
        public decimal LastTradeId { get; set; }
        public decimal Count { get; set; }
    }
}
