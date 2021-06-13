namespace ct_api.Models
{

    using Newtonsoft.Json;

    public class HistoricAmount
    {
        [JsonProperty("amount")]
        public double Amount { get; set; }
        [JsonProperty("fiat")]
        public double Fiat { get; set; }
        [JsonProperty("btc")]
        public double Btc { get; set; }
    }
}
