using System;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;

namespace ct_api
{
    public class CoinTrackingAPI
    {
        private const string url = "https://cointracking.info/api/v1/";
        private readonly string apiKey;
        private readonly string apiSecret;

        private static readonly HttpClient client = new HttpClient();

        public CoinTrackingAPI(string key, string secret)
        {
            apiKey = key;
            apiSecret = secret;
        }

        private FormUrlEncodedContent PrepareRequestData(string method, List<KeyValuePair<string, string>> data)
        {
            return new FormUrlEncodedContent(
                Enumerable.Concat(
                  data,
                  new[] {
                    new KeyValuePair<string,string>("method", method),
                    new KeyValuePair<string,string>("nonce", new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds().ToString())
                  }
                )
            );
        }

        private async Task SignMessage(FormUrlEncodedContent formData)
        {
            var hmac = new HMACSHA512(Encoding.ASCII.GetBytes(apiSecret));
            var sign = hmac.ComputeHash(await formData.ReadAsByteArrayAsync());
            formData.Headers.Add("Key", apiKey);
            formData.Headers.Add("Sign", BitConverter.ToString(sign).Replace("-", string.Empty).ToLower());
        }

        public async Task<string> GetTrades(int limit = 0, string order = "ASC", int start = 0, int end = 0, bool tradePrices = false)
        {
            var optionalParams = new List<KeyValuePair<string, string>>();

            if (limit > 0)
            {
                optionalParams.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
            }

            optionalParams.Add(new KeyValuePair<string, string>("order", order));

            if (start > 0)
            {
                optionalParams.Add(new KeyValuePair<string, string>("start", start.ToString()));
            }

            if (end > 0)
            {
                optionalParams.Add(new KeyValuePair<string, string>("end", end.ToString()));
            }

            if (tradePrices)
            {
                optionalParams.Add(new KeyValuePair<string, string>("trade_prices", "1"));
            }

            var response = await GetSignedResponse("getTrades", optionalParams);
            return await response.Content.ReadAsStringAsync();
        }

        private async Task<HttpResponseMessage> GetSignedResponse(string methodName, List<KeyValuePair<string, string>> optionalParams)
        {
            var formData = PrepareRequestData(methodName, optionalParams);
            await SignMessage(formData);

            var response = await client.PostAsync(url, formData);
            response.EnsureSuccessStatusCode();

            return response;
        }

        public async Task<string> GetBalance()
        {
            var response = await GetSignedResponse("getBalance", new List<KeyValuePair<string, string>>());
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetHistoricalSummary(bool btc = false, int start = 0, int end = 0)
        {
            var optionalParams = new List<KeyValuePair<string, string>>();

            if (btc)
            {
                optionalParams.Add(new KeyValuePair<string, string>("btc", "1"));
            }

            if (start > 0)
            {
                optionalParams.Add(new KeyValuePair<string, string>("start", start.ToString()));
            }

            if (end > 0)
            {
                optionalParams.Add(new KeyValuePair<string, string>("end", end.ToString()));
            }

            var response = await GetSignedResponse("getHistoricalSummary", optionalParams);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetHistoricalCurrency(string currency = null, int start = 0, int end = 0)
        {
            var optionalParams = new List<KeyValuePair<string, string>>();

            if (!string.IsNullOrEmpty(currency))
            {
                optionalParams.Add(new KeyValuePair<string, string>("currency", currency));
            }

            if (start > 0)
            {
                optionalParams.Add(new KeyValuePair<string, string>("start", start.ToString()));
            }

            if (end > 0)
            {
                optionalParams.Add(new KeyValuePair<string, string>("end", end.ToString()));
            }

            var response = await GetSignedResponse("getHistoricalCurrency", optionalParams);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetGroupedBalance(string group = "exchange", bool excludeDepWith = false, string type = null)
        {
            var optionalParams = new List<KeyValuePair<string, string>>();

            if (!string.IsNullOrEmpty(group))
            {
                optionalParams.Add(new KeyValuePair<string, string>("group", group));
            }

            if (!string.IsNullOrEmpty(type))
            {
                optionalParams.Add(new KeyValuePair<string, string>("type", type));
            }

            if (excludeDepWith)
            {
                optionalParams.Add(new KeyValuePair<string, string>("exclude_dep_with", "1"));
            }

            var response = await GetSignedResponse("getGroupedBalance", optionalParams);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetGains(string price = null, bool excludeDepWith = false, string costbasis = null, bool btc = false)
        {
            var optionalParams = new List<KeyValuePair<string, string>>();

            if (!string.IsNullOrEmpty(price))
            {
                optionalParams.Add(new KeyValuePair<string, string>("price", price));
            }

            if (!string.IsNullOrEmpty(costbasis))
            {
                optionalParams.Add(new KeyValuePair<string, string>("costbasis", costbasis));
            }

            if (excludeDepWith)
            {
                optionalParams.Add(new KeyValuePair<string, string>("exclude_dep_with", "1"));
            }

            if (btc)
            {
                optionalParams.Add(new KeyValuePair<string, string>("btc", "1"));
            }

            var response = await GetSignedResponse("getGains", optionalParams);
            return await response.Content.ReadAsStringAsync();
        }
    }
}