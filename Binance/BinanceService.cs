using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Binance {
    public abstract class BinanceService {
        protected readonly HttpClient _client;

        protected BinanceService(string baseUrl) {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(baseUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("User-Agent", ".NET Binance API Client");
        }
    }
}
