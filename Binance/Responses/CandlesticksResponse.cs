using System;

namespace Binance {
    public class CandlesticksResponse {
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public decimal Volume { get; set; }
        public DateTime CloseTime { get; set; }
        public decimal QuoteAssetVolume { get; set; }
        public decimal TradeCount { get; set; }
        public decimal TakerBuyBaseAssetVolume { get; set; }
        public decimal TakerBuyQuoteAssetVolume { get; set; }
        public decimal Ignore { get; set; }
    }
}
