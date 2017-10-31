using BinanceService.Extensions;
using BinanceService.Handlers;
using BinanceService.Responses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BinanceService {
    public class BinanceAccountService {
        protected HttpClient _client;
        
        /// <summary>
        /// Initializes a new instance of the BinanceAccountService
        /// </summary>
        /// <param name="apiKey">The API key to use.</param>
        /// <param name="secretKey">The Secret Key to use.</param>
        /// <param name="baseUrl">The webservice base URL.</param>
        /// <param name="receiveWindow">The receive-window to use.</param>
        public BinanceAccountService(string apiKey, string secretKey, string baseUrl = "https://www.binance.com/api/v3/", int receiveWindow = 5000) {
            var authHandler = new BinanceAuthenticationHandler(apiKey, secretKey, receiveWindow);

            _client = new HttpClient(authHandler);
            _client.BaseAddress = new Uri(baseUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("User-Agent", ".NET Binance API Client");
        }

        /// <summary>
        /// Send in a new order
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        /// <param name="side">The order side.</param>
        /// <param name="type">The order type.</param>
        /// <param name="timeInForce">The time-in-force to apply.</param>
        /// <param name="quantity">The quantity.</param>
        /// <param name="price">The price.</param>
        /// <param name="clientOrderId">A unique id for the order. Automatically generated if not sent.</param>
        /// <param name="stopPrice">Used with stop orders.</param>
        /// <param name="icebergQuantity">Used with iceberg orders.</param>
        /// <param name="test">If true creates and validates a new order but does not send it into the matching engine.</param>
        public async Task<PostOrderResponse> PostOrder(
            string symbol,
            OrderSide side,
            OrderType type,
            TimeInForce timeInForce,
            decimal quantity,
            decimal price,
            string clientOrderId = null,
            decimal? stopPrice = null,
            decimal? icebergQuantity = null,
            bool test = false) {

            var urlPath = test ? "order/test" : "order";

            var parameters = new List<(string, string)> {
               ("symbol", symbol),
               ("side", side.ToString()),
               ("type", type.ToString()),
               ("quantity", quantity.ToString(CultureInfo.InvariantCulture)),
               ("clientOrderId", clientOrderId?.ToString()),
               ("icebergQty", icebergQuantity?.ToString(CultureInfo.InvariantCulture)), // TODO: Fix this
            };

            if (type == OrderType.Limit) {
                parameters.Add(("price", price.ToString(CultureInfo.InvariantCulture)));
                parameters.Add(("timeInForce", timeInForce.ToString()));
                parameters.Add(("stopPrice", stopPrice?.ToString(CultureInfo.InvariantCulture)));
            }

            // TODO: Test always return empty content
            return await _client.PostJsonAsync<PostOrderResponse>(urlPath, parameters.ToArray());
        }

        private async Task<OrderResponse> GetOrderImpl(string symbol, long? orderId = null, string origClientOrderId = null) {
            var response = await _client.GetJsonAsync<OrderResponse>("order",
                ("symbol", symbol),
                ("orderId", orderId.ToString()),
                ("origClientOrderId", origClientOrderId)
            );

            return response;
        }

        /// <summary>
        /// Check an order's status.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        /// <param name="orderId">The order ID.</param>
        public async Task<OrderResponse> GetOrder(string symbol, long orderId) {
            return await GetOrderImpl(symbol, orderId, null);
        }

        /// <summary>
        /// Check an order's status.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        /// <param name="origClientOrderId">The client order ID.</param>
        public async Task<OrderResponse> GetOrder(string symbol, string origClientOrderId) {
            return await GetOrderImpl(symbol, null, origClientOrderId);
        }

        /// <summary>
        /// Cancel an active order.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        /// <param name="orderId">The order ID.></param>
        /// <param name="origClientOrderId">The client order ID.</param>
        /// <param name="newClientOrderId">An ID used to uniquely identify this cancel. Automatically generated by default.</param>
        public async Task<OrderResponse> DeleteOrder(string symbol, string orderId = null, string origClientOrderId = null, string newClientOrderId = null) {
            var response = await _client.DeleteJsonAsync<OrderResponse>("order",
                ("symbol", symbol),
                ("orderId", orderId),
                ("origClientOrderId", origClientOrderId)
            );

            return response;
        }

        /// <summary>
        /// Get all open orders on a symbol.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        public async Task<IEnumerable<OrderResponse>> GetOpenOrders(string symbol) {
            return await _client.GetJsonAsync<IEnumerable<OrderResponse>>("openOrders", ("symbol", symbol));
        }

        /// <summary>
        /// Get all account orders; active, canceled, or filled.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        /// <param name="orderId">The order ID.</param>
        /// <param name="limit">How many items to request.</param>
        /// <returns></returns>
        public async Task<IEnumerable<OrderResponse>> GetOrders(string symbol, long? orderId = null, int limit = 500) {
            return await _client.GetJsonAsync<IEnumerable<OrderResponse>>("allOrders", 
                ("symbol", symbol),
                ("orderId", orderId?.ToString()),
                ("limit", limit.ToString())
            );
        }

        /// <summary>
        /// Get trades for a specific account and symbol.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        /// <param name="fromId">TradeId to fetch from. Default gets most recent trades.</param>
        /// <param name="limit">How many items to request.</param>
        /// <returns></returns>
        public async Task<IEnumerable<TradeResponse>> GetTrades(string symbol, long? fromId = null, int limit = 500) {
            return await _client.GetJsonAsync<IEnumerable<TradeResponse>>("myTrades",
                ("symbol", symbol),
                ("fromId", fromId?.ToString()),
                ("limit", limit.ToString())
            );
        }

        /// <summary>
        /// Get current account information.
        /// </summary>
        public async Task<AccountInfoResponse> GetAccountInfo() {
            return await _client.GetJsonAsync<AccountInfoResponse>("account");
        }
    }
}
