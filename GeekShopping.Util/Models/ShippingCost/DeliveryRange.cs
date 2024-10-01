using System.Text.Json.Serialization;

namespace GeekShopping.Util.Models.ShippingCost
{
    public class DeliveryRange
    {
        [JsonPropertyName("min")]
        public int Min { get; set; }

        [JsonPropertyName("max")]
        public int Max { get; set; }
    }
}
