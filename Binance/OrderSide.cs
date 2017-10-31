namespace Binance {
    public class OrderSide {
        private readonly string _value;

        public static readonly OrderSide Buy = new OrderSide("BUY");
        public static readonly OrderSide Sell = new OrderSide("SELL");

        private OrderSide(string value) {
            _value = value;
        }

        public override string ToString() {
            return _value;
        }
    }
}