# BinanceService .NET API wrapper for the Binance Exchange

This project is a typed client-library for the Binance Exchange API. It makes it easy to integrate Binance API functionality in your application.

[![NuGet](https://img.shields.io/nuget/v/binanceservice.svg)](https://www.nuget.org/packages/BinanceService)

## Getting Started

You should install [BinanceService with NuGet](https://www.nuget.org/packages/BinanceService):

    Install-Package BinanceService
    
Or via the .NET Core command line interface:

    dotnet add package BinanceService
    
See the following section for usage examples.

## Sample usage

The API is split up into three logical classes mirroring the endpoints exposed by the Binance API. 

The market data endpoint can be used anonymously.

```csharp
var binanceMarketService = new BinanceMarketService();
```

The account endpoint requires authentication using your API key and secret (can be found on the Binance API setting page).

```csharp
var binanceAccountService = new BinanceAccountService("KEY", "SECRET");
```

The steam-service exposes an easy way to consume the websockets streams in an asynchronous way.

```csharp
var binanceStream = new BinanceStreamService();
```

The available methods on the classes should be pretty self explanatory and are fully documented. Some examples:

### Examples for using the market endpoint

```csharp
var orderbook = await binanceMarketService.GetOrderBook("NEOBTC");
Console.WriteLine("Orderbook: " + JsonConvert.SerializeObject(orderbook, Formatting.Indented));

var aggTrades = await binanceMarketService.GetAggregateTrades("NEOBTC");
Console.WriteLine("Agg trades: " + JsonConvert.SerializeObject(aggTrades, Formatting.Indented));

var priceStats = await binanceMarketService.GetPriceStats("NEOBTC");
Console.WriteLine("Pricestats: " + JsonConvert.SerializeObject(priceStats, Formatting.Indented));
```

### Examples for using the account endpoint

```csharp
var order = await binanceAccount.PostOrder("NEOBTC", OrderSide.Buy, OrderType.Market, TimeInForce.ImmediateOrCancel, 0.10m, 1, test: true);
Console.WriteLine("Market order: " + JsonConvert.SerializeObject(order, Formatting.Indented));

var myTrades = await binanceAccount.GetTrades("NEOBTC");
Console.WriteLine("My trades: " + JsonConvert.SerializeObject(myTrades, Formatting.Indented));
```

### Example for streaming using websockets

```csharp
await binanceStream.StreamTrades("NEOBTC", (t) => Console.WriteLine(t.TradeId), cts.Token);
```

Please see the `BinanceService.Examples` project for more examples.

For more guidance you can also have a look at the [official Binance documentation](https://www.binance.com/restapipub.html).

## Authors

* **Robert Massa** - *Initial work* - [Grepsy](https://github.com/Grepsy)

## License

This project is licensed under the MIT License.