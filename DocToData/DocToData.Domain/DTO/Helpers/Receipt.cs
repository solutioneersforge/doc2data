using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocToData.Domain.DTO.Helpers
{
    public class Receipt
    {
        [JsonProperty("merchant_name")]
        public string MerchantName { get; set; }

        [JsonProperty("merchant_address")]
        public string MerchantAddress { get; set; }

        [JsonProperty("merchant_phone")]
        public string MerchantPhone { get; set; }

        [JsonProperty("receipt_no")]
        public string ReceiptNo { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("items")]
        public List<Item> Items { get; set; }

        [JsonProperty("subtotal")]
        public decimal Subtotal { get; set; }

        [JsonProperty("total")]
        public decimal Total { get; set; }
    }
}
