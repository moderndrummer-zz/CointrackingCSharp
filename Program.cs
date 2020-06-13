using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ct_api
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var apiKey = config["apiKey"];
            var apiSecret = config["apiSecret"];

            //REAL DATA
            var api = new CoinTrackingAPI(apiKey, apiSecret);
            var response = await api.GetBalance();

            //MOCK DATA
            //var response = await File.ReadAllTextAsync("MockBalanceData.json");

            var model = JsonConvert.DeserializeObject<Root>(response);
            if (model.Success == 0) {
                throw new Exception("Request failed, check your API keys");
            }

            Console.WriteLine($"Total Account Value: {model.Summary.ProfitFiat} {model.AccountCurrency}");

            foreach (var item in model.Details)
            {
                if (double.TryParse(item.Value.ValueFiat, out var value) && value > 1)
                    Console.WriteLine($"{item.Value.Amount} {item.Key} => {item.Value.ValueFiat} {model.AccountCurrency}");
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
