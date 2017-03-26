using Bittrex;
using Bittrex.Helpers;
using System;
using System.Collections.Generic;
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
            ex.Initialise(new ExchangeContext() {
                ApiKey = "",
                Secret = "",
                Simulate = false,
                QuoteCurrency = "BTC"
            });

            var history = ex.GetMarketHistory("ETC",50);
            var dt=  DataTableHelper.ToDataTable(history);
            
            DataTableHelper.ToPrintConsole(dt);
            Console.WriteLine("Type anything to close it.");
            Console.Read();
        }
    }
}
