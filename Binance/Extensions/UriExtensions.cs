using System;

namespace Binance.Extensions {
    internal static class UriExtensions {
        public static Uri AppendToQuery(this Uri uri, string key, string value) {
            var uriBuilder = new UriBuilder(uri);
            uriBuilder.Query += $"&{key}={value}";

            return uriBuilder.Uri;
        }
    }
}
