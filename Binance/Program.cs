using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Binance {
    public static class Program {
        static void Main(string[] args) {
            Test().Wait();
        }

        static async Task Test() {
            var binance = new BinanceMarketService();
            //Console.WriteLine("Ping: " + await binance.Ping());
            //Console.WriteLine("Time: " + await binance.Time());

            //var orderbook = await binance.GetOrderBook("NEOBTC");
            //Console.WriteLine("Orderbook: " + JsonConvert.SerializeObject(orderbook, Formatting.Indented));

            //var aggTrades = await binance.GetAggregateTrades("NEOBTC");
            //Console.WriteLine("Agg trades: " + JsonConvert.SerializeObject(aggTrades, Formatting.Indented));

            var candles = await binance.GetCandlesticks("NEOBTC", Interval.OneDay);
            Console.WriteLine("Candles: " + JsonConvert.SerializeObject(candles, Formatting.Indented));


            Console.ReadLine();
        }
    }
}