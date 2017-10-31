using System;

namespace BinanceService.Responses {
    public class CandlesticksResponse {
        public DateTime OpenTime { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public decimal Volume { get; set; }
        public DateTime CloseTime { get; set; }
        public decimal QuoteAssetVolume { get; set; }
        public int TradeCount { get; set; }
        public decimal TakerBuyBaseAssetVolume { get; set; }
        public decimal TakerBuyQuoteAssetVolume { get; set; }
    }
}
