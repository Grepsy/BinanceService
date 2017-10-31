using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BinanceService {
    public static class Program {
        static void Main(string[] args) {
            // Add you API key and secret
            var apikey = "";
            var secret = "";

            MarketSamples().Wait();
            
            //AccountSample(apikey, secret).Wait(); // WARNING: Enabling this section might execute trades

            StreamSamples().Wait();

            Console.ReadLine();
        }

        static async Task MarketSamples() {
            var binanceMarketService = new BinanceMarketService();

            Console.WriteLine("Ping: " + await binanceMarketService.Ping());
            Console.WriteLine("Time: " + await binanceMarketService.Time());

            var orderbook = await binanceMarketService.GetOrderBook("NEOBTC");
            Console.WriteLine("Orderbook: " + JsonConvert.SerializeObject(orderbook, Formatting.Indented));

            var aggTrades = await binanceMarketService.GetAggregateTrades("NEOBTC");
            Console.WriteLine("Agg trades: " + JsonConvert.SerializeObject(aggTrades, Formatting.Indented));

            var priceStats = await binanceMarketService.GetPriceStats("NEOBTC");
            Console.WriteLine("Pricestats: " + JsonConvert.SerializeObject(priceStats, Formatting.Indented));

            var allPrices = await binanceMarketService.GetAllPrices();
            Console.WriteLine("All prices: " + JsonConvert.SerializeObject(allPrices, Formatting.Indented));

            var bestBidAsk = await binanceMarketService.GetBestBidAsk();
            Console.WriteLine("Best bid/ask prices: " + JsonConvert.SerializeObject(bestBidAsk, Formatting.Indented));

            var candleSticks = await binanceMarketService.GetCandlesticks("NEOBTC", Interval.FiveMinutes);
            Console.WriteLine("Candlesticks: " + JsonConvert.SerializeObject(candleSticks, Formatting.Indented));
        }

        private static async Task AccountSample(string apikey, string secret) {
            var binanceAccount = new BinanceAccountService(apikey, secret);

            var orderResp = await binanceAccount.PostOrder("NEOBTC", OrderSide.Buy, OrderType.Market, TimeInForce.ImmediateOrCancel, 0.10m, 1, test: true);
            Console.WriteLine("Market order: " + JsonConvert.SerializeObject(orderResp, Formatting.Indented));

            var orderResp2 = await binanceAccount.PostOrder("OMGETH", OrderSide.Buy, OrderType.Limit, TimeInForce.ImmediateOrCancel, 0.10m, 1, test: true);
            Console.WriteLine("Limit order: " + JsonConvert.SerializeObject(orderResp2, Formatting.Indented));

            var order = await binanceAccount.GetOrder("NEOBTC", 0);
            Console.WriteLine("Order resp: " + JsonConvert.SerializeObject(order, Formatting.Indented));

            var clientOrder = await binanceAccount.GetOrder("NEOBTC", "myOrder");
            Console.WriteLine("Order resp: " + JsonConvert.SerializeObject(clientOrder, Formatting.Indented));

            var openOrders = await binanceAccount.GetOpenOrders("NEOBTC");
            Console.WriteLine("Order resp: " + JsonConvert.SerializeObject(openOrders, Formatting.Indented));

            var orders = await binanceAccount.GetOrders("NEOBTC");
            Console.WriteLine("Order resp: " + JsonConvert.SerializeObject(orders, Formatting.Indented));

            var myTrades = await binanceAccount.GetTrades("NEOBTC");
            Console.WriteLine("My trades: " + JsonConvert.SerializeObject(myTrades, Formatting.Indented));

            var accountInfo = await binanceAccount.GetAccountInfo();
            Console.WriteLine("Order resp: " + JsonConvert.SerializeObject(accountInfo, Formatting.Indented));
        }

        private static async Task StreamSamples() {
            var binanceStream = new BinanceStreamService();
            var cts = new CancellationTokenSource();

            Console.WriteLine("Steaming depth...");
            await binanceStream.StreamDepth("NEOBTC", (ev) => Console.WriteLine(ev.EventTime), cts.Token);

            Console.WriteLine("Steaming trades...");
            await binanceStream.StreamTrades("NEOBTC", (trade) => Console.WriteLine(trade.Price), cts.Token);

            Console.WriteLine("Steaming klines...");
            await binanceStream.StreamKlines("NEOBTC", Interval.OneMinute, (ev) => Console.WriteLine(ev.EventTime), cts.Token);
        }
    }
}