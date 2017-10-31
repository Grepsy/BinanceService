using BinanceService.Converters;
using Newtonsoft.Json;
using System;

namespace BinanceService.Responses {
    public class PostOrderResponse {
        public string Symbol { get; set; }
        public long OrderId { get; set; }
        public string ClientOrderId { get; set; }

        [JsonProperty("transactTime")]
        [JsonConverter(typeof(UnixTimestampJsonConverter))]
        public DateTime TransactionTime { get; set; }
    }
}