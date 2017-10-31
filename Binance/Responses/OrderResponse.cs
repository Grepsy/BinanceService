using System;

namespace Binance.Responses {
    public class OrderResponse {
        public string Symbol { get; set; }
        public string OrderId { get; set; }
        public string ClientOrderId { get; set; }
        public decimal Price { get; set; }
        public int OrigQty { get; set; }
        public int ExecutedQty { get; set; }
        public string Status { get; set; }
        public string TimeInForce { get; set; }
        public string Type { get; set; }
        public string Side { get; set; }
        public decimal StopPrice { get; set; }
        public decimal IcebergQty { get; set; }
        public DateTime Time { get; set; }
    }
}
