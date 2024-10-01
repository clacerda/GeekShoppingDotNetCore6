using System.Text.Json.Serialization;

namespace GeekShopping.Util.Models.ShippingCost
{
    public class Dimensions
    {
        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("length")]
        public int Length { get; set; }
    }
}
