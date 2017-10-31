using BinanceService.Responses;
using Newtonsoft.Json;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BinanceService {
    public class BinanceStreamService {
        private const int MAX_MESSAGE_SIZE = 1024 * 8;
        private readonly string _baseUrl;

        public BinanceStreamService(string baseUrl = "wss://stream.binance.com:9443/ws/") {
            _baseUrl = baseUrl;
        }

        public async Task StreamDepth(string symbol, Action<DepthEvent> onUpdate, CancellationToken cancellationToken) {
            var url = new Uri(_baseUrl + symbol.ToLower() + "@depth");

            await Stream(onUpdate, url, cancellationToken);
        }

        public async Task StreamTrades(string symbol, Action<TradeEvent> onTrade, CancellationToken cancellationToken) {
            var url = new Uri(_baseUrl + symbol.ToLower() + "@aggTrade");

            await Stream(onTrade, url, cancellationToken);
        }

        public async Task StreamKlines(string symbol, Interval interval, Action<KlineEvent> onUpdate, CancellationToken cancellationToken) {
            var url = new Uri(_baseUrl + symbol.ToLower() + "@kline_" + interval);

            await Stream(onUpdate, url, cancellationToken);
        }

        private static async Task Stream<T>(Action<T> onUpdate, Uri url, CancellationToken cancellationToken) {
            var client = new ClientWebSocket();
            await client.ConnectAsync(url, cancellationToken);

            while (client.State == WebSocketState.Open && !cancellationToken.IsCancellationRequested) {
                var buffer = new ArraySegment<byte>(new byte[MAX_MESSAGE_SIZE]);
                var result = await client.ReceiveAsync(buffer, cancellationToken);
                var payload = Encoding.UTF8.GetString(buffer.Array, 0, result.Count);
                var ev = JsonConvert.DeserializeObject<T>(payload);

                onUpdate(ev);
            }
        }
    }
}
