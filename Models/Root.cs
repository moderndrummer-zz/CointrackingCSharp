namespace ct_api.Models
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

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
}
