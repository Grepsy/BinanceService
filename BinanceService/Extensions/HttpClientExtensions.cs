using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace BinanceService.Extensions {
    internal static class HttpClientExtensions {
        public static async Task<T> GetJsonAsync<T>(this HttpClient client, string requestUri, params (string, string)[] parameters) {
            var url = requestUri;

            if (parameters != null) {
                var queryParams = parameters
                    .Where(p => p.Item2 != null)
                    .Select(p => string.Format("{0}={1}", WebUtility.UrlEncode(p.Item1), WebUtility.UrlEncode(p.Item2)));

                url += "?" + string.Join("&", queryParams);
            }

            var response = await client.GetAsync(url);
            
            if (!response.IsSuccessStatusCode) {
                var errorContent = await response.Content.ReadAsStringAsync();
                var message = $"{response.StatusCode} {response.ReasonPhrase}\n{errorContent}";

                throw new HttpRequestException(message);
            }

            var content = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<T>(content);

            return responseObject;
        }

        public static async Task<T> PostJsonAsync<T>(this HttpClient client, string requestUri, params (string, string)[] parameters) {
            var url = requestUri;

            if (parameters != null) {
                var queryParams = parameters
                    .Where(p => p.Item2 != null)
                    .Select(p => string.Format("{0}={1}", WebUtility.UrlEncode(p.Item1), WebUtility.UrlEncode(p.Item2)));

                url += "?" + string.Join("&", queryParams);
            }

            var response = await client.PostAsync(url, content: null /* no body required */);

            if (!response.IsSuccessStatusCode) {
                var errorContent = await response.Content.ReadAsStringAsync();
                var message = $"{response.StatusCode} {response.ReasonPhrase}\n{errorContent}";

                throw new HttpRequestException(message);
            }

            var content = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<T>(content);

            return responseObject;
        }

        public static async Task<T> DeleteJsonAsync<T>(this HttpClient client, string requestUri, params (string, string)[] parameters) {
            var url = requestUri;

            if (parameters != null) {
                var queryParams = parameters
                    .Where(p => p.Item2 != null)
                    .Select(p => string.Format("{0}={1}", WebUtility.UrlEncode(p.Item1), WebUtility.UrlEncode(p.Item2)));

                url += "?" + string.Join("&", queryParams);
            }

            var response = await client.DeleteAsync(url);

            if (!response.IsSuccessStatusCode) {
                var errorContent = await response.Content.ReadAsStringAsync();
                var message = $"{response.StatusCode} {response.ReasonPhrase}\n{errorContent}";

                throw new HttpRequestException(message);
            }

            var content = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<T>(content);

            return responseObject;
        }
    }
}
