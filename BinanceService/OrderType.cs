namespace BinanceService {
    public class OrderType {
        private readonly string _value;

        public static readonly OrderType Limit = new OrderType("LIMIT");
        public static readonly OrderType Market = new OrderType("MARKET");
     
        private OrderType(string value) {
            _value = value;
        }

        public override string ToString() {
            return _value;
        }
    }
}