using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace BinanceService.Extensions {
    internal static class HttpClientExtensions {
        public static async Task<T> GetJsonAsync<T>(this HttpClient client, string requestUri, params (string, string)[] parameters) {
            var url = requestUri;

            if (parameters != null) {
                url += parameters.BuildQueryString();
            }

            var response = await client.GetAsync(url);
            await HandleDeepErrors(response);

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(content);
        }

        public static async Task<T> PostJsonAsync<T>(this HttpClient client, string requestUri, params (string, string)[] parameters) {
            var url = requestUri;

            if (parameters != null) {
                url += parameters.BuildQueryString();
            }

            var response = await client.PostAsync(url, content: null /* no body required */);
            await HandleDeepErrors(response);

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(content);
        }

        public static async Task<T> DeleteJsonAsync<T>(this HttpClient client, string requestUri, params (string, string)[] parameters) {
            var url = requestUri;

            if (parameters != null) {
                url += parameters.BuildQueryString();
            }

            var response = await client.DeleteAsync(url);
            await HandleDeepErrors(response);

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(content);
        }

        private static async Task HandleDeepErrors(HttpResponseMessage response) {
            if (!response.IsSuccessStatusCode) {
                var errorContent = await response.Content.ReadAsStringAsync();
                var message = $"{response.StatusCode} {response.ReasonPhrase}\n{errorContent}";

                throw new HttpRequestException(message);
            }
        }
    }
}
