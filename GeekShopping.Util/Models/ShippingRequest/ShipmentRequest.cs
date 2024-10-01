using System.Text.Json.Serialization;

namespace GeekShopping.Util.Models.ShippingRequest
{
    public class ShipmentRequest
    {
        [JsonPropertyName("from")]
        public Zipcode From { get; set; }
        [JsonPropertyName("to")]
        public Zipcode To { get; set; }
        public Package Package { get; set; }
    }
}
