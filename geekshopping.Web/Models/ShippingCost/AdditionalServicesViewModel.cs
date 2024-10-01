using System.Text.Json.Serialization;

namespace GeekShopping.Util.Models.ShippingCost
{
    public class AdditionalServicesViewModel
    {
        [JsonPropertyName("receipt")]
        public bool Receipt { get; set; }

        [JsonPropertyName("own_hand")]
        public bool OwnHand { get; set; }

        [JsonPropertyName("collect")]
        public bool Collect { get; set; }

    }
}
