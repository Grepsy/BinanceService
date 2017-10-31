using BinanceService.Converters;
using Newtonsoft.Json;
using System;

namespace BinanceService.Responses {
    public class TradeResponse {
        public long Id { get; set; }
        public decimal Price { get; set; }
        public decimal Qty { get; set; }
        public decimal Commission { get; set; }
        public string CommissionAsset { get; set; }
        [JsonConverter(typeof(UnixTimestampJsonConverter))]
        public DateTime Time { get; set; }
        public bool IsBuyer { get; set; }
        public bool IsMaker { get; set; }
        public bool IsBestMatch { get; set; }
    }
}