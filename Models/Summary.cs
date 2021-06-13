namespace ct_api.Models
{

    using Newtonsoft.Json;

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
