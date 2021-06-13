namespace ct_api
{
    using System;
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

    public class Trade
    {
        [JsonProperty("buy_amount")]
        public double? BuyAmount { get; set; }

        [JsonProperty("buy_currency")]
        public string BuyCurrency { get; set; }

        [JsonProperty("sell_amount")]
        public double? SellAmount { get; set; }

        [JsonProperty("sell_currency")]
        public string SellCurrency { get; set; }

        [JsonProperty("fee_amount")]
        public string FeeAmount { get; set; }

        [JsonProperty("fee_currency")]
        public string FeeCurrency { get; set; }

        [JsonProperty("buy_value_in_cur")]
        public double? BuyValueInCur { get; set; }

        [JsonProperty("buy_value_in_btc")]
        public double? BuyValueInBtc { get; set; }

        [JsonProperty("sell_value_in_cur")]
        public string SellValueInCur { get; set; }

        [JsonProperty("sell_value_in_btc")]
        public string SellValueInBtc { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("exchange")]
        public string Exchange { get; set; }

        [JsonProperty("group")]
        public string Group { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("imported_from")]
        public string ImportedFrom { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        public DateTime Timestamp
        {
            get
            {
                var longtime = long.Parse(Time);
                return longtime.ToDateTime();
            }
        }

        [JsonProperty("imported_time")]
        public string ImportedTime { get; set; }

        [JsonProperty("trade_id")]
        public string TradeId { get; set; }


    }

    public class HistoricAmount
    {
        [JsonProperty("amount")]
        public double Amount { get; set; }
        [JsonProperty("fiat")]
        public double Fiat { get; set; }
        [JsonProperty("btc")]
        public double Btc { get; set; }
    }


    public enum EntryType
    {
        Income,
        Trade,
        Deposit,
        Withdrawal
    }
}
