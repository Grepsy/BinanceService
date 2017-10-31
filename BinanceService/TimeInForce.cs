namespace BinanceService {
    public class TimeInForce {
        private readonly string _value;

        public static readonly TimeInForce GoodTillCancelled = new TimeInForce("GTC");
        public static readonly TimeInForce ImmediateOrCancel = new TimeInForce("IOC");
        
        private TimeInForce(string value) {
            _value = value;
        }

        public override string ToString() {
            return _value;
        }
    }
}