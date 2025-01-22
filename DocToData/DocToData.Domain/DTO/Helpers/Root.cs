using Newtonsoft.Json;

namespace DocToData.Domain.DTO.Helpers
{
    public class Root
    {
        [JsonProperty("receipts")]
        public List<Receipt> Receipts { get; set; }
    }
}
