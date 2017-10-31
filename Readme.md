# BinanceService .NET API wrapper for the Binance Exchange

This project is a typed client-library for the Binance Exchange API.

[![NuGet](https://img.shields.io/nuget/v/binanceservice.svg)](https://www.nuget.org/packages/BinanceService)

## Getting Started

You should install [BinanceService with NuGet](https://www.nuget.org/packages/BinanceService):

    Install-Package BinanceService
    
Or via the .NET Core command line interface:

    dotnet add package BinanceService
    
See the following section for usage examples.

## Samples

The API is split up into three logical classes mirroring the endpoints exposed by the Binance API. 

The market data endpoint can be used anonymously.

```csharp
var binanceMarketService = new BinanceMarketService();
```

The account endpoint requires authentication using your API key and secret (can be found on the Binance API setting page).

```csharp
var binanceAccountService = new BinanceAccountService("KEY", "SECRET");
```

The available methods on the classes are pretty self explanatory and fully documented. Some examples:

```csharp
var orderbook = await binanceMarketService.GetOrderBook("NEOBTC");
Console.WriteLine("Orderbook: " + JsonConvert.SerializeObject(orderbook, Formatting.Indented));

var aggTrades = await binanceMarketService.GetAggregateTrades("NEOBTC");
Console.WriteLine("Agg trades: " + JsonConvert.SerializeObject(aggTrades, Formatting.Indented));

var priceStats = await binanceMarketService.GetPriceStats("NEOBTC");
Console.WriteLine("Pricestats: " + JsonConvert.SerializeObject(priceStats, Formatting.Indented));
```

For more guidance you can also have a look at the (official Binance documentation)[https://www.binance.com/restapipub.html].

## Authors

* **Robert Massa** - *Initial work* - [Grepsy](https://github.com/Grepsy)

## License

This project is licensed under the MIT License.