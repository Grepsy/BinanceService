using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Binance {
    public class BinanceMarketService : BinanceService {
        public BinanceMarketService(string baseUrl = "https://www.binance.com/api/v1/") : base(baseUrl) {
        }

        public async Task<bool> Ping() {
            var response = await _client.GetAsync("ping");
            response.EnsureSuccessStatusCode();

            return true;
        }

        public async Task<DateTime> Time() {
            var response = await _client.GetJsonAsync<TimeResponse>("time");

            return response.ServerTime.ToDateTime();
        }

        public async Task<OrderBookResponse> GetOrderBook(string symbol, int limit = 500) {
            return await _client.GetJsonAsync<OrderBookResponse>("depth", ("symbol", symbol), ("limit", limit.ToString()));
        }

        public async Task<AggregateTradesResponse> GetAggregateTrades(string symbol, long? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int limit = 500) {
            var dto = await _client.GetJsonAsync<IEnumerable<AggregateTradeDto>>("aggTrades",
                ("symbol", symbol),
                ("fromId", fromId?.ToString()),
                ("startTime", startTime?.ToTimestamp().ToString()),
                ("endTime", endTime?.ToTimestamp().ToString()),
                ("limit", limit.ToString()));

            return new AggregateTradesResponse();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>If startTime and endTime are not sent, the most recent klines are returned.</remarks>
        public async Task<CandlesticksResponse> GetCandlesticks(string symbol, Interval interval, DateTime? startTime = null, DateTime? endTime = null, int limit = 500) {
            return await _client.GetJsonAsync<CandlesticksResponse>("depth");
        }

        public async Task<PriceStatsResponse> GetPriceStats(string symbol) {
            return await _client.GetJsonAsync<PriceStatsResponse>("ticker/24hr");
        }

        public async Task<IEnumerable<PriceResponse>> GetAllPrices() {
            return await _client.GetJsonAsync<IEnumerable<PriceResponse>>("ticker/allPrices");
        }

        public async Task<IEnumerable<BestBidAskResponse>> GetBestBidAsk() {
            return await _client.GetJsonAsync<IEnumerable<BestBidAskResponse>>("allBookTickers");
        }
    }
}
