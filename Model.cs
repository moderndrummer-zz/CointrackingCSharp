namespace ct_api
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Root
    {
        [JsonProperty("success")]
        public long Success { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("account_currency")]
        public string AccountCurrency { get; set; }

        [JsonProperty("details")]
        public Dictionary<string, Detail> Details { get; set; }

        [JsonProperty("summary")]
        public Summary Summary { get; set; }
    }

    public partial class Detail
    {
        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("coin")]
        public string Coin { get; set; }

        [JsonProperty("value_fiat")]
        public string ValueFiat { get; set; }

        [JsonProperty("value_btc")]
        public string ValueBtc { get; set; }

        [JsonProperty("price_fiat")]
        public string PriceFiat { get; set; }

        [JsonProperty("price_btc")]
        public string PriceBtc { get; set; }

        [JsonProperty("change1h")]
        public string Change1H { get; set; }

        [JsonProperty("change24h")]
        public string Change24H { get; set; }

        [JsonProperty("change7d")]
        public string Change7D { get; set; }

        [JsonProperty("change30d")]
        public string Change30D { get; set; }
    }

    public partial class Summary
    {
        [JsonProperty("value_fiat")]
        public string ValueFiat { get; set; }

        [JsonProperty("value_btc")]
        public string ValueBtc { get; set; }

        [JsonProperty("profit_fiat")]
        public string ProfitFiat { get; set; }

        [JsonProperty("profit_btc")]
        public string ProfitBtc { get; set; }
    }
}
