namespace Binance {
    public class Interval {
        private readonly string _value;

        public static readonly Interval OneMinute = new Interval("1m");
        public static readonly Interval ThreeMinutes = new Interval("3m");
        public static readonly Interval FiveMinutes = new Interval("5m");
        public static readonly Interval FifteenMinutes = new Interval("15m");
        public static readonly Interval ThirtyMinutes = new Interval("30m");
        public static readonly Interval OneHour = new Interval("1h");
        public static readonly Interval TwoHours = new Interval("2h");
        public static readonly Interval FourHours = new Interval("4h");
        public static readonly Interval SixHours = new Interval("6h");
        public static readonly Interval EightHours = new Interval("8h");
        public static readonly Interval TwelveHours = new Interval("12h");
        public static readonly Interval OneDay = new Interval("1d");
        public static readonly Interval ThreeDays = new Interval("3d");
        public static readonly Interval OneWeek = new Interval("1w");
        public static readonly Interval OneMonth = new Interval("1M");

        private Interval(string value) {
            _value = value;
        }

        public override string ToString() {
            return _value;
        }
    }
}