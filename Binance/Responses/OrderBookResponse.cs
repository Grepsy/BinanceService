namespace Binance.Responses {
    public class OrderBookResponse {
        public long LastUpdateId { get; set; }
        // TODO: Fix model
        public dynamic Bids { get; set; }
        public dynamic Asks { get; set; }
    }
}
