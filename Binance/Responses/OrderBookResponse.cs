namespace Binance {
    public class OrderBookResponse {
        public long LastUpdateId { get; set; }
        public dynamic Bids { get; set; }
        public dynamic Asks { get; set; }
    }
}
