using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ct_api
{
    class Program
    {
        const string apiKey = "your_api_key_here";
        const string apiSecret = "your_secret_here";


        static async Task Main(string[] args)
        {
            //REAL DATA
            var api = new CoinTrackingAPI(apiKey, apiSecret);
            var response = await api.GetBalance();

            //MOCK DATA
            //var response = await File.ReadAllTextAsync("MockBalanceData.json");

            var model = JsonConvert.DeserializeObject<Root>(response);

            Console.WriteLine($"Total Account Value: {model.Summary.ProfitFiat} {model.AccountCurrency}");


            foreach (var item in model.Details)
            {
                if (double.TryParse(item.Value.ValueFiat, out var value) && value > 1)
                    Console.WriteLine($"{item.Value.Amount} {item.Key} \t\t => \t\t {item.Value.ValueFiat} {model.AccountCurrency}");
            }

            Console.WriteLine(response);
            Console.WriteLine(await api.GetTrades());
            Console.WriteLine(await api.GetHistoricalSummary());
            Console.WriteLine(await api.GetHistoricalCurrency("BTC"));
            Console.WriteLine(await api.GetGroupedBalance("type"));
            Console.WriteLine(await api.GetGains("best"));
        }
    }
}
