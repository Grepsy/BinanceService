using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<AggregateTradeResponse>> GetAggregateTrades(string symbol, long? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int limit = 500) {
            var response = await _client.GetJsonAsync<IEnumerable<AggregateTradeResponse>>("aggTrades",
                ("symbol", symbol),
                ("fromId", fromId?.ToString()),
                ("startTime", startTime?.ToTimestamp().ToString()),
                ("endTime", endTime?.ToTimestamp().ToString()),
                ("limit", limit.ToString()));

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>If startTime and endTime are not sent, the most recent klines are returned.</remarks>
        public async Task<IEnumerable<CandlesticksResponse>> GetCandlesticks(string symbol, Interval interval, DateTime? startTime = null, DateTime? endTime = null, int limit = 500) {
            var response = await _client.GetJsonAsync<dynamic[]>("klines",
                ("symbol", symbol),
                ("interval", interval.ToString()),
                ("startTime", startTime?.ToTimestamp().ToString()),
                ("endTime", endTime?.ToTimestamp().ToString()),
                ("limit", limit.ToString()));

            return response.Select(x => new CandlesticksResponse {
                Open = x[0],
                High = x[1],
                Low = x[2],
                Close = x[3],
                Volume = x[4],
                CloseTime = ((long)x[5]).ToDateTime(),
                QuoteAssetVolume = x[6],
                TradeCount = x[7],
                TakerBuyBaseAssetVolume = x[8],
                TakerBuyQuoteAssetVolume = x[9]
            });
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
