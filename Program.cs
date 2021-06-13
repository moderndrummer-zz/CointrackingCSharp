using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ct_api
{
    public class Program
    {
        private static readonly DateTime purgeUntil = new DateTime(2018, 1, 29);
        static async Task Main(string[] args)
        {
            // use your own dev configuration file to import your Cointracking API Keys
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.development.json", true, true)
                .Build();

            var apiKey = config["apiKey"];
            var apiSecret = config["apiSecret"];

            //REAL DATA
            var api = new CoinTrackingAPI(apiKey, apiSecret);
            await OutputBalancesUntil(api, purgeUntil);
        }

        private static async Task OutputBalancesUntil(CoinTrackingAPI api, DateTime until)
        {
            var untilLong = until.ToUnixTimeSeconds();
            //var response = await api.GetHistoricalCurrency();
            var response = await File.ReadAllTextAsync("MockHistoricalCurrency.json");

            var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(response);
            var accountCurrency = json.ContainsKey("account_currency") ? json["account_currency"] : null;
            var values = json.Skip(3).Select(i => i.Value).ToList();
            var coinData = values.SelectMany(i =>
                JsonConvert.DeserializeObject<Dictionary<string, Dictionary<long, HistoricAmount>>>(i.ToString())
            ).ToList();

            Console.WriteLine($"Portfolio amounts until {until}");

            foreach (var coinEntry in coinData)
            {
                var coinName = coinEntry.Key;
                var timestampKeyValues = coinEntry.Value;

                var results = timestampKeyValues.Where(i => i.Key <= untilLong).ToList();
                if (!results.Any()) continue;

                var key = results.Max(i => i.Key);
                var amount = results.First(i => i.Key == key).Value.Amount;
                if (amount <= 0) continue;

                Console.WriteLine($"Token: {coinName}, Amount: {results.First(i => i.Key == key).Value.Amount}");
            }
        }


        private static async Task OutputTrades(CoinTrackingAPI api)
        {
            //var response = await api.GetTrades(tradePrices: true);
            var response = await File.ReadAllTextAsync("MockTrades.json");

            var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(response);
            var accountCurrency = json.ContainsKey("account_currency") ? json["account_currency"] : null;
            var tradeObjects = json.Skip(3).Select(i => i.Value).ToList();

            var trades = tradeObjects.Select(i => JsonConvert.DeserializeObject<Trade>(i.ToString())).ToList();

            var incomes = trades.Where(i =>
                i.Type.Equals(EntryType.Income.ToString(), StringComparison.InvariantCultureIgnoreCase) &&
                i.Timestamp < purgeUntil).ToList();

            var incomeCurrencies = incomes.Select(i => i.BuyCurrency).Distinct().ToList();

            foreach (string currency in incomeCurrencies)
            {
                Console.WriteLine();
                var income = incomes.Where(i => i.BuyCurrency.Equals(currency)).ToList();
                Console.WriteLine($"purging for {currency}, found {income.Count} entries");
                var lastDate = income.Max(i => i.Timestamp).ToString();
                Console.WriteLine($"Last income date on {lastDate}");
                var totalIncome = income.Sum(i => i.BuyAmount);
                var totalIncomeInAccountCurrency = income.Sum(i => i.BuyValueInCur);
                var totalIncomeInBTC = income.Sum(i => i.BuyValueInBtc);

                Console.WriteLine($"Total income {totalIncome} {currency}");
                Console.WriteLine($"Buy value in {accountCurrency} {totalIncomeInAccountCurrency}");
                Console.WriteLine($"Total income in BTC {totalIncomeInBTC}");

            }
        }

    }
}



