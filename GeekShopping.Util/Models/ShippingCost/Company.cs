using System.Text.Json.Serialization;

namespace GeekShopping.Util.Models.ShippingCost
{
    public class Company
    {

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("picture")]
        public string Picture { get; set; }
    }
}
