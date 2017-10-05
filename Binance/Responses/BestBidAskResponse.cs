namespace Binance {
    public class BestBidAskResponse {
        public string Symbol { get; set; }
        public decimal BidPrice { get; set; }
        public decimal BidQty { get; set; }
        public decimal AskPrice { get; set; }
        public decimal AskQty { get; set; }
    }
}
