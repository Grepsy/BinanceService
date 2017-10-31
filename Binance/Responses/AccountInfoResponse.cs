namespace Binance.Responses {
    /*
     *  "makerCommission": 15,
  "takerCommission": 15,
  "buyerCommission": 0,
  "sellerCommission": 0,
  "canTrade": true,
  "canWithdraw": true,
  "canDeposit": true,
  "balances": [
    {
      "asset": "BTC",
      "free": "4723846.89208129",
      "locked": "0.00000000"
    },
    {
      "asset": "LTC",
      "free": "4763368.68006011",
      "locked": "0.00000000"
    }
  ]
     */
    public class AccountInfoResponse {
        public decimal MakerCommission { get; set; }
        public decimal TakerCommission { get; set; }
        public decimal BuyerCommission { get; set; }
        public decimal SellerCommission { get; set; }
        public bool CanTrade { get; set; }
        public bool CanWithdraw { get; set; }
        public bool CanDeposit { get; set; }
        public Balance[] Balances { get; set; }

        public class Balance {
            public string Asset { get; set; }
            public decimal Free { get; set; }
            public decimal Locked { get; set; }
        }
    }
}