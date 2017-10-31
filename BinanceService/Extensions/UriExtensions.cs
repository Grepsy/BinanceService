using System;
using System.Linq;
using System.Net;

namespace BinanceService.Extensions {
    internal static class UriExtensions {
        public static Uri AppendToQuery(this Uri uri, string key, string value) {
            var uriBuilder = new UriBuilder(uri);
            uriBuilder.Query += $"&{key}={value}";

            return uriBuilder.Uri;
        }

        public static string BuildQueryString(this (string, string)[] parameters) {
            var queryParams = parameters
                    .Where(p => p.Item2 != null)
                    .Select(p => string.Format("{0}={1}", WebUtility.UrlEncode(p.Item1), WebUtility.UrlEncode(p.Item2)));

            return "?" + string.Join("&", queryParams);
        }
    }
}
