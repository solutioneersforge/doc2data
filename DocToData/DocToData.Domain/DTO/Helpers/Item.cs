using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocToData.Domain.DTO.Helpers
{
    public class Item
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("qty")]
        public int Qty { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }
}
