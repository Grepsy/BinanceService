using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Binance {
    public static class Program {
        static void Main(string[] args) {
            Samples().Wait();
        }

        static async Task Samples() {
            // Add you API key and secret
            var apikey = "";
            var secret = "";

            var binance = new BinanceMarketService();
            Console.WriteLine("Ping: " + await binance.Ping());
            Console.WriteLine("Time: " + await binance.Time());

            var orderbook = await binance.GetOrderBook("NEOBTC");
            Console.WriteLine("Orderbook: " + JsonConvert.SerializeObject(orderbook, Formatting.Indented));

            var aggTrades = await binance.GetAggregateTrades("NEOBTC");
            Console.WriteLine("Agg trades: " + JsonConvert.SerializeObject(aggTrades, Formatting.Indented));

            var priceStats = await binance.GetPriceStats("NEOBTC");
            Console.WriteLine("Pricestats: " + JsonConvert.SerializeObject(priceStats, Formatting.Indented));

            var allPrices = await binance.GetAllPrices();
            Console.WriteLine("All prices: " + JsonConvert.SerializeObject(allPrices, Formatting.Indented));

            var bestBidAsk = await binance.GetBestBidAsk();
            Console.WriteLine("Best bid/ask prices: " + JsonConvert.SerializeObject(bestBidAsk, Formatting.Indented));

            var candleSticks = await binance.GetCandlesticks("NEOBTC", Interval.FiveMinutes);
            Console.WriteLine("Candlesticks: " + JsonConvert.SerializeObject(candleSticks, Formatting.Indented));

            var binanceAccount = new BinanceAccountService(apikey, secret);

            // WARNING: Enabling this section might execute trades
            if (false) {
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

            //var binanceStream = new BinanceStreamService();
            //await binanceStream.StreamTrades("NEOBTC");

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}