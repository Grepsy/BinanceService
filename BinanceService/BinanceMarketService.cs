using BinanceService.Extensions;
using BinanceService.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BinanceService {
    public class BinanceMarketService {
        protected HttpClient _client;

        /// <summary>
        /// Initializes a new instance of the BinanceMarketService.
        /// </summary>
        /// <param name="baseUrl">The webservice base URL.</param>
        public BinanceMarketService(string baseUrl = "https://www.binance.com/api/v1/")  {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(baseUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("User-Agent", ".NET Binance API Client");
        }

        /// <summary>
        /// Test connectivity to the Rest API.
        /// </summary>
        public async Task<bool> Ping() {
            var response = await _client.GetAsync("ping");
            response.EnsureSuccessStatusCode();

            return true;
        }

        /// <summary>
        /// Test connectivity to the Rest API and get the current server time.
        /// </summary>
        public async Task<DateTime> Time() {
            var response = await _client.GetJsonAsync<TimeResponse>("time");

            return response.ServerTime.ToDateTime();
        }

        /// <summary>
        /// Fetches the order book.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        /// <param name="limit">How much entries to request.</param>
        public async Task<OrderBookResponse> GetOrderBook(string symbol, int limit = 100) {
            return await _client.GetJsonAsync<OrderBookResponse>("depth", ("symbol", symbol), ("limit", limit.ToString()));
        }

        /// <summary>
        /// Get compressed, aggregate trades. Trades that fill at the time, from the same order, with the same price will have the quantity aggregated.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        /// <param name="fromId">The ID to get aggregate trades from (inclusive).</param>
        /// <param name="startTime">The start-time (inclusive).</param>
        /// <param name="endTime">The end-time (inclusive).</param>
        /// <param name="limit">How much entries to request.</param>
        public async Task<AggregateTradesResponse> GetAggregateTrades(string symbol, long? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int limit = 500) {
            var dto = await _client.GetJsonAsync<IEnumerable<AggregateTradeDto>>("aggTrades",
                ("symbol", symbol),
                ("fromId", fromId?.ToString()),
                ("startTime", startTime?.ToUnixTimestamp().ToString()),
                ("endTime", endTime?.ToUnixTimestamp().ToString()),
                ("limit", limit.ToString()));

            return new AggregateTradesResponse();
        }

        /// <summary>
        /// Get candlestick bars for a symbol. Candles are uniquely identified by their open time.
        /// </summary>
        /// <remarks>If startTime and endTime are not sent, the most recent klines are returned.</remarks>
        /// <param name="symbol">The symbol.</param>
        /// <param name="interval">The interval.</param>
        /// <param name="startTime">The start-time.</param>
        /// <param name="endTime">The end-time.</param>
        /// <param name="limit">How much entries to request.</param>
        public async Task<IEnumerable<CandlesticksResponse>> GetCandlesticks(string symbol, Interval interval, DateTime? startTime = null, DateTime? endTime = null, int limit = 500) {
            var klineData = await _client.GetJsonAsync<dynamic[]>("klines",
                ("symbol", symbol),
                ("interval", interval.ToString()),
                ("startTime", startTime?.ToUnixTimestamp().ToString()),
                ("endTime", endTime?.ToUnixTimestamp().ToString()),
                ("limit", limit.ToString()));

            return klineData.Select(x => new CandlesticksResponse {
                OpenTime = ((long)x[0]).ToDateTime(),
                Open = x[1],
                High = x[2],
                Low = x[3],
                Close = x[4],
                Volume = x[5],
                CloseTime = ((long)x[6]).ToDateTime(),
                QuoteAssetVolume = x[7],
                TradeCount = x[8],
                TakerBuyBaseAssetVolume = x[9],
                TakerBuyQuoteAssetVolume = x[10]
            });
        }

        /// <summary>
        /// Get 24 hour price change statistics.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        public async Task<PriceStatsResponse> GetPriceStats(string symbol) {
            return await _client.GetJsonAsync<PriceStatsResponse>("ticker/24hr", ("symbol", symbol));
        }

        /// <summary>
        /// Get latest price for all symbols.
        /// </summary>
        public async Task<IEnumerable<PriceResponse>> GetAllPrices() {
            return await _client.GetJsonAsync<IEnumerable<PriceResponse>>("ticker/allPrices");
        }

        /// <summary>
        /// Get the best price/qty on the order book for all symbols.
        /// </summary>
        public async Task<IEnumerable<BestBidAskResponse>> GetBestBidAsk() {
            return await _client.GetJsonAsync<IEnumerable<BestBidAskResponse>>("ticker/allBookTickers");
        }
    }
}
