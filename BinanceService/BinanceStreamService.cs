using BinanceService.Responses;
using Newtonsoft.Json;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BinanceService {
    public class BinanceStreamService {
        private const int MAX_MESSAGE_SIZE = 1024;
        private readonly ClientWebSocket _client;
        private readonly string _baseUrl;

        public BinanceStreamService(string baseUrl = "wss://stream.binance.com:9443/ws/") {
            _baseUrl = baseUrl;
            _client = new ClientWebSocket();
        }

        public async Task StreamTrades(string symbol, Action<StreamTrade> onTrade, CancellationToken cancellationToken) {
            var url = new Uri(_baseUrl + symbol.ToLower() + "@aggTrade");

            await _client.ConnectAsync(url, cancellationToken);
            
            while (_client.State == WebSocketState.Open) {
                var buffer = new ArraySegment<byte>(new byte[MAX_MESSAGE_SIZE]);
                var result = await _client.ReceiveAsync(buffer, cancellationToken);
                var payload = Encoding.UTF8.GetString(buffer.Array, 0, result.Count);

                var trade = JsonConvert.DeserializeObject<StreamTrade>(payload);

                onTrade(trade);
            }
        }
    }
}
