using Newtonsoft.Json;

namespace BinanceService.Responses {
    public class BestBidAskResponse {
        public string Symbol { get; set; }

        public decimal BidPrice { get; set; }

        [JsonProperty("BidQty")]
        public decimal BidQuantity { get; set; }

        public decimal AskPrice { get; set; }

        [JsonProperty("AskQty")]
        public decimal AskQuantity { get; set; }
    }
}
