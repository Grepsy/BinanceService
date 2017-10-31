using System;

namespace Binance.Extensions {
    internal static class DateTimeExtensions {
        public static DateTime ToDateTime(this long timestamp) {
            var dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(timestamp);
            var dateTime = dateTimeOffset.UtcDateTime;

            return dateTime;
        }

        public static long ToUnixTimestamp(this DateTime dateTime) {
            if (dateTime == DateTime.MinValue) {
                return 0;
            }

            return ((DateTimeOffset)dateTime).ToUnixTimeMilliseconds();
        }
    }
}
