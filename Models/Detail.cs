namespace ct_api.Models
{

    using Newtonsoft.Json;

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
}
