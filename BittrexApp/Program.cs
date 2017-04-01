using Bittrex;
using Bittrex.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BittrexApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var ex = new Exchange();
            ex.Initialise(new ExchangeContext()
            {
                ApiKey = "",
                Secret = "",
                Simulate = false,
                QuoteCurrency = "BTC"
            });

            String market = "ETC";
            string path = @"D:\temp\GetMarketHistory.txt";


            var openOrders = ex.GetOpenOrders(market);
            bool isNoActionDefined = true;
            if (openOrders.Any(r => r.OrderType == OpenOrderType.Limit_Buy))
            {
                isNoActionDefined = false;
            }
            if (openOrders.Any(r => r.OrderType == OpenOrderType.Limit_Sell))
            {
                isNoActionDefined = false;
            }

            if (isNoActionDefined)
            {
                var balance = ex.GetBalance(market);
                path = @"D:\temp\GetBalance.txt";
          

            }


            path = @"D:\temp\orderBooks.txt";
            var orderBooks = ex.GetOrderBook(market, OrderBookType.Buy, 20);
            DataTableHelper.WriteFile(orderBooks.buy, path);



            var ticker = ex.GetTicker(market);

            //BuyDigitalCurrency(ex, market, 0.05001342m, 10);



            var marketHistory = ex.GetMarketHistory(market);
    

      

            path = @"D:\temp\getopenorders.txt";
            var getopenorders = ex.GetOpenOrders(market);
            DataTableHelper.WriteFile(getopenorders, path);

            Console.WriteLine("Type anything to close it.");
            Console.Read();

        }

        private static void PrintBalance(Exchange ex)
        {
            var balances = ex.GetBalances();
            var dt = DataTableHelper.ToDataTable(balances);

            File.WriteAllText(@"D:\temp\balances.txt", DataTableHelper.ToPrintConsole(dt));
        }

        private static OrderResponse BuyDigitalCurrency(Exchange ex, string market, decimal btc, int percantageDiff)
        {
            var marketSummary = ex.GetMarketSummary(market);
            decimal price = marketSummary.Bid - (marketSummary.Bid * ((decimal)percantageDiff / 100));
            decimal quantity = Math.Round(btc / price, 2);
            Console.WriteLine(marketSummary.Bid + "   " + price);
            var accountBalance = ex.PlaceBuyOrder(market, quantity, price);
            return accountBalance;
        }
    }
}
